using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDS7.Group1.Project3.Assets.Scripts;
using GDS7.Group1.Project3.Assets.Scripts.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class BrasierPuzzleHints : MonoBehaviour
{
    [SerializeField] private int _brazierCountToHintAt;
    [SerializeField] private BrasierPuzzle _brasierPuzzle;
    [SerializeField] private GameObject _hintPrefab;

    private BrazierPuzzlePiece _hintingAt;
    private GameObject _hint;

    public void OnPuzzleUpdate()
    {
        var remaining = _brasierPuzzle.GetUnlitPieces();
        var remainingCount = remaining.Count();

        if (remainingCount > _brazierCountToHintAt)
        {
            return;
        }

        if (_hint)
        {
            Destroy(_hint);
            _hint = null;
        }

        if (!remaining.Any())
        {
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        _hintingAt = remaining.OrderBy(b =>
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.SamplePosition(b.Brasier.transform.position, out var bHit, 1f, NavMesh.AllAreas)
                && NavMesh.SamplePosition(player.transform.position, out var playerHit, 1f, NavMesh.AllAreas) 
                && NavMesh.CalculatePath(playerHit.position, bHit.position, NavMesh.AllAreas, path))
            {
                return EnemySpawner.GetPathLength(path);

            } else
            {
                return Vector3.Distance(player.transform.position, b.Brasier.transform.position);
            }
        }).First();
        _hint = Instantiate(_hintPrefab, _hintingAt.Brasier.transform);
    }
}
