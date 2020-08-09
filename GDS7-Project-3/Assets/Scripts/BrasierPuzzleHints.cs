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
        Debug.Log("OnPuzzleUpdate", this);
        var remaining = _brasierPuzzle.GetUnlitPieces();
        var remainingCount = remaining.Count();

        Debug.Log(remainingCount, this);
        if (remainingCount > _brazierCountToHintAt)
        {
            return;
        }

        if (_hint)
        {
            Debug.Log("Destroy hint", this);
            Destroy(_hint);
            _hint = null;
        }

        if (!remaining.Any())
        {
            Debug.Log("No remaining", this);
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
                Debug.Log("NavMeshPath", b.Brasier);
                return EnemySpawner.GetPathLength(path);

            } else
            {
                Debug.Log("Vector3.Distance", b.Brasier);
                return Vector3.Distance(player.transform.position, b.Brasier.transform.position);
            }
        }).First();
        Debug.Log("Hinting at", _hintingAt.Brasier);
        _hint = Instantiate(_hintPrefab, _hintingAt.Brasier.transform);
        Debug.Log("Spawned hint", _hint);
    }
}
