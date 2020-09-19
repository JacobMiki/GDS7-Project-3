using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using GDS7.Group1.Project3.Assets.Scripts.Enemy;
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
        GameObject.Find("Player").transform.position = _largeRoomTarget.position;
    }

    public void MovePlayerToStaircase()
    {
        GameObject.Find("Player").transform.position = _staircaseTarget.position;
    }

    public void MovePlayerToLabirynth()
    {
        GameObject.Find("Player").transform.position = _labirynthTarget.position;
    }
    
    public void MovePlayerToExit()
    {
        GameObject.Find("Player").transform.position = _exitTarget.position;
    }

    public void TogglePlayerTorch()
    {
        var torchState = GameObject.Find("Player").GetComponent<ITorchState>();
        torchState.HasTorch = !torchState.HasTorch;
    }

    public void SolveLabyrinth()
    {
        GameObject.Find("Labyrinth Brasier Puzzle").GetComponent<BrasierPuzzle>().ForceSolve();
    }




}
