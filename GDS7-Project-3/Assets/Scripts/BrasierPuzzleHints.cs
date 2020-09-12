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
    [SerializeField] private float _timeToTriggerHintBeforeCount;
    [SerializeField] private BrasierPuzzle _brasierPuzzle;
    [SerializeField] private GameObject _hintPrefab;

    private BrazierPuzzlePiece _hintingAt;
    private GameObject _hint;
    private Coroutine _waitToSpawnHintCoroutine;
    private Coroutine _tryWaitToTriggerCoroutine;

    public void OnPuzzleUpdate()
    {
        if (_tryWaitToTriggerCoroutine != null)
        {
            StopCoroutine(_tryWaitToTriggerCoroutine);
        }

        var remaining = _brasierPuzzle.GetUnlitPieces();
        var remainingCount = remaining.Count();

        if (_hint)
        {
            _hint.GetComponent<PuzzleHint>().MarkToDestroy();
        }

        if (remainingCount > _brazierCountToHintAt)
        {
            _tryWaitToTriggerCoroutine = StartCoroutine(TryWaitToTrigger(remaining));
        }
        else
        {
            HandleSpawn(remaining);
        }
    }

    private IEnumerator TryWaitToTrigger(IEnumerable<BrazierPuzzlePiece> remaining)
    {
        yield return new WaitForSeconds(_timeToTriggerHintBeforeCount);

        HandleSpawn(remaining);
        _tryWaitToTriggerCoroutine = null;
    }

    private void HandleSpawn(IEnumerable<BrazierPuzzlePiece> remaining)
    {


        if (!remaining.Any())
        {
            return;
        }

        _waitToSpawnHintCoroutine = StartCoroutine(WaitToSpawnHint(remaining));
    }

    private IEnumerator WaitToSpawnHint(IEnumerable<BrazierPuzzlePiece> remaining)
    {
        while(_hint)
        {
            yield return null;
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

            }
            else
            {
                return Vector3.Distance(player.transform.position, b.Brasier.transform.position);
            }
        }).First();
        _hint = Instantiate(_hintPrefab, _hintingAt.Brasier.transform);
        _waitToSpawnHintCoroutine = null;
    }
}
