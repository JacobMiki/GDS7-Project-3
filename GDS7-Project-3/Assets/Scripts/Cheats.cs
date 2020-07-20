using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Enemy;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;
using UnityEngine.AI;

public class Cheats : MonoBehaviour
{
    [SerializeField] private Vector3 _labirynthTarget;
    [SerializeField] private Vector3 _labirynth2Target;

    public void MovePlayerToLabirynth()
    {
        GameObject.Find("Player").transform.position = _labirynthTarget;
    }
    
    public void MovePlayerToLabirynth2()
    {
        GameObject.Find("Player").transform.position = _labirynth2Target;
    }

    public void TogglePlayerTorch()
    {
        var torchState = GameObject.Find("Player").GetComponent<ITorchState>();
        torchState.HasTorch = !torchState.HasTorch;
    }

    public void SpawnEnemy()
    {
        FindObjectOfType<EnemySpawner>()?.SpawnEnemy();
    }




}
