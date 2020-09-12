using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;
using UnityEngine.Events;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public enum BrazierPuzzlePieceInfluenceType
    {
        ON,
        OFF,
        TOGGLE
    }

    [System.Serializable]
    public class BrazierPuzzlePiece
    {
        [SerializeReference] public SwitchableLight Brasier;
        public BrazierPuzzlePieceInfluence[] Influences;
    }

    [System.Serializable]
    public class BrazierPuzzlePieceInfluence
    {
        [SerializeReference] public SwitchableLight Other;
        public BrazierPuzzlePieceInfluenceType Type;
    }

    public class BrasierPuzzle : MonoBehaviour
    {
        [SerializeField] private BrazierPuzzlePiece[] _pieces;
        [SerializeField] private UnityEvent _onPuzzleSolve;
        [SerializeField] private UnityEvent _afterPuzzleUpdate;

        public bool Solved { get; set; }

        public void UpdatePuzzle(SwitchableLight brasier)
        {
            if (Solved)
            {
                return;
            }

            var triggeringPiece = _pieces.First(piece => piece.Brasier == brasier);

            foreach (var influence in triggeringPiece.Influences)
            {
                if (influence.Other)
                {
                    switch (influence.Type)
                    {
                        case BrazierPuzzlePieceInfluenceType.ON:
                            influence.Other.Switch(true, emitEvent: false);
                            break;
                        case BrazierPuzzlePieceInfluenceType.OFF:
                            influence.Other.Switch(false, emitEvent: false);
                            break;
                        case BrazierPuzzlePieceInfluenceType.TOGGLE:
                            influence.Other.Toggle(emitEvent: false);
                            break;
                    }
                }
            }

            if (_pieces.All(piece => piece.Brasier.IsLightOn))
            {
                foreach (var piece in _pieces)
                {
                    piece.Brasier.SwitchingDisabled = true;
                }
                _onPuzzleSolve?.Invoke();
            }

            _afterPuzzleUpdate.Invoke();
        }

        public IEnumerable<BrazierPuzzlePiece> GetUnlitPieces()
        {
            return _pieces.Where(piece => !piece.Brasier.IsLightOn);
        }
    }
}
