using System.Collections;
using System.Xml.Serialization;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;
using UnityEngine.AI;

namespace GDS7.Group1.Project3.Assets.Scripts.Enemy
{
    enum EnemySpawnerState
    {
        WAITING,
        COUNTDOWN_TO_SPAWN,
        SPAWNING,
        SPAWNED
    }
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _distanceFromPlayer;
        [SerializeField] private float _navMeshTolerance;
        [SerializeField] private float _coneAngle;
        [SerializeField] private float _timeToSpawn;

        private GameObject _enemyInstance;
        private GameObject _player;
        private ISafeState _playerSafeState;
        private ITorchState _playerTorchState;
        private float _countdown;
        private bool _abortSpawning = false;
        [SerializeField] private EnemySpawnerState _state;


        private void OnEnable()
        {
            _player = GameManager.Instance.Player;
            _playerSafeState = _player.GetComponent<ISafeState>();
            _playerTorchState = _player.GetComponent<ITorchState>();
            _enemyInstance = null;
            _state = EnemySpawnerState.WAITING;

        }

        private void Update()
        {
            if (_enemyInstance)
            {
                _state = EnemySpawnerState.SPAWNED;
            }

            if (!_playerTorchState.HasTorch)
            {
                _state = EnemySpawnerState.WAITING;
            }

            switch (_state)
            {
                case EnemySpawnerState.WAITING:
                    {
                        if (!_playerSafeState.IsSafe && _playerTorchState.HasTorch)
                        {
                            _state = EnemySpawnerState.COUNTDOWN_TO_SPAWN;
                            _countdown = _timeToSpawn;
                        }
                    }
                    return;
                case EnemySpawnerState.COUNTDOWN_TO_SPAWN:
                    {
                        _countdown -= Time.deltaTime;

                        if (_playerSafeState.IsSafe)
                        {
                            _state = EnemySpawnerState.WAITING;
                            return;
                        }

                        if (_countdown <= 0)
                        {
                            _state = EnemySpawnerState.SPAWNING;
                            SpawnEnemy();
                        }
                    }
                    return;
                case EnemySpawnerState.SPAWNING:
                    {
                        if (_playerSafeState.IsSafe)
                        {
                            _state = EnemySpawnerState.WAITING;
                            _abortSpawning = true;
                            if (!_enemyInstance)
                            {
                                Destroy(_enemyInstance);
                                _enemyInstance = null;
                            }
                            return;
                        }
                    }
                    return;
                case EnemySpawnerState.SPAWNED:
                    {
                        if (_playerSafeState.IsSafe)
                        {
                            _state = EnemySpawnerState.WAITING;
                            Destroy(_enemyInstance);
                            _enemyInstance = null;
                            return;
                        }
                        if (!_enemyInstance)
                        {
                            _state = EnemySpawnerState.COUNTDOWN_TO_SPAWN;
                            _countdown = _timeToSpawn;
                        }
                    }
                    return;
            }
        }

        private void OnDisable()
        {
            Destroy(_enemyInstance);
            _enemyInstance = null;
            _player = null;
        }

        public void SpawnEnemy()
        {
            StartCoroutine(TrySpawnEnemy());
        }

        private IEnumerator TrySpawnEnemy()
        {

            if (_player)
            {
                do
                {
                    if (_abortSpawning)
                    {
                        _abortSpawning = false;
                        yield break;
                    }

                    var randomRotation = Quaternion.AngleAxis(Random.Range(-_coneAngle, _coneAngle), Vector3.up);

                    var pos = _player.transform.position + randomRotation * _player.transform.forward * _distanceFromPlayer;
                    Debug.DrawLine(_player.transform.position, pos, Color.red, 1f);
                    if (NavMesh.SamplePosition(pos, out var navMeshHit, _navMeshTolerance, NavMesh.AllAreas))
                    {
                        Debug.DrawLine(pos, navMeshHit.position, Color.green, 1f);
                        NavMeshPath path = new NavMeshPath();
                        if (NavMesh.CalculatePath(navMeshHit.position, _player.transform.position, NavMesh.AllAreas, path) && path.status == NavMeshPathStatus.PathComplete && GetPathLength(path) < _distanceFromPlayer + _navMeshTolerance)
                        {
                            var length = GetPathLength(path);
                            if (length < _distanceFromPlayer + _navMeshTolerance && length > _distanceFromPlayer - _navMeshTolerance)
                            {
                                _enemyInstance = Instantiate(_enemyPrefab);
                                _enemyInstance.transform.position = navMeshHit.position;
                            }
                        }
                    }

                    if (!_enemyInstance)
                    {
                        yield return null;
                    }

                } while (!_enemyInstance);
            }
        }

        public static float GetPathLength(NavMeshPath path)
        {
            float lng = 0.0f;

            for (int i = 1; i < path.corners.Length; ++i)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }

            return lng;
        }
    }
}