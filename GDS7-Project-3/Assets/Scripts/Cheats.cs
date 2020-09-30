using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using GDS7.Group1.Project3.Assets.Scripts.Enemy;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;
using UnityEngine.AI;

public class Cheats : MonoBehaviour
{
    [SerializeField] private Transform _largeRoomTarget;
    [SerializeField] private Transform _staircaseTarget;
    [SerializeField] private Transform _labirynthTarget;
    [SerializeField] private Transform _exitTarget;

    public void Awake()
    {
#if !DEBUG
        gameObject.SetActive(false);
#endif
    }

    public void MovePlayerToLargeRoom()
    {
        GameManager.Instance.Player.transform.position = _largeRoomTarget.position;
    }

    public void MovePlayerToStaircase()
    {
        GameManager.Instance.Player.transform.position = _staircaseTarget.position;
    }

    public void MovePlayerToLabirynth()
    {
        GameManager.Instance.Player.transform.position = _labirynthTarget.position;
    }
    
    public void MovePlayerToExit()
    {
        GameManager.Instance.Player.transform.position = _exitTarget.position;
    }

    public void TogglePlayerTorch()
    {
        var torchState = GameManager.Instance.Player.GetComponent<ITorchState>();
        torchState.HasTorch = !torchState.HasTorch;
    }

    public void SolveLabyrinth()
    {
        GameObject.Find("Labyrinth Brasier Puzzle").GetComponent<BrasierPuzzle>().ForceSolve();
    }

    public void FindAllCoins()
    {
        foreach(var coin in CollectableManager.Instance.collectableInteracts)
        {
            coin.collected = true;
            Destroy(coin.GetComponentInChildren<MeshRenderer>().gameObject);
        }
        CollectableManager.Instance.UpdateCollectables();
    }


}
