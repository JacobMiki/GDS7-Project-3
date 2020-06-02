using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Combat
{
    enum CombatDirectorState
    {
        PLAYER_OUTSIDE,
        PLAYER_SAFE,
        WAITING_FOR_SPAWN,
        ENCOUNTER,

    }
    public class CombatDirector : MonoBehaviour
    {
        [SerializeField] private GameObject _combatEncounter;
        [SerializeField] private GameObject _player;
        [SerializeField] private float _timeBetweenEncounters;

        [SerializeField] private CombatDirectorState _state = CombatDirectorState.PLAYER_OUTSIDE;

        private GameObject _activeEncounter = null;
        private Coroutine _spawningCoroutine = null;
        private ISafeState _playerSafeState;


        private void Awake()
        {
            _playerSafeState = _player.GetComponent<ISafeState>();
        }

        private void Update()
        {
            switch (_state)
            {
                case CombatDirectorState.PLAYER_OUTSIDE:
                    if (_spawningCoroutine != null)
                    {
                        StopCoroutine(_spawningCoroutine);
                        _spawningCoroutine = null;
                    }
                    break;
                case CombatDirectorState.PLAYER_SAFE:
                    if (_spawningCoroutine != null)
                    {
                        StopCoroutine(_spawningCoroutine);
                        _spawningCoroutine = null;
                    }
                    if (!_playerSafeState.IsSafe)
                    {
                        _state = CombatDirectorState.WAITING_FOR_SPAWN;
                    }
                    break;
                case CombatDirectorState.WAITING_FOR_SPAWN:
                    if (_playerSafeState.IsSafe)
                    {
                        _state = CombatDirectorState.PLAYER_SAFE;
                    }
                    if (_spawningCoroutine == null)
                    {
                        _spawningCoroutine = StartCoroutine(WaitAndSpawn());
                    }
                    break;
                case CombatDirectorState.ENCOUNTER:
                    if (_activeEncounter == null)
                    {
                        _state = CombatDirectorState.WAITING_FOR_SPAWN;
                    }
                    break;
            }
        }

        private IEnumerator WaitAndSpawn()
        {
            yield return new WaitForSeconds(_timeBetweenEncounters);
            _activeEncounter = Instantiate(_combatEncounter, _player.transform.position, _player.transform.rotation, transform);
            _state = CombatDirectorState.ENCOUNTER;

            _spawningCoroutine = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _state = CombatDirectorState.WAITING_FOR_SPAWN;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _state = CombatDirectorState.PLAYER_OUTSIDE;
            }
        }

    }
}