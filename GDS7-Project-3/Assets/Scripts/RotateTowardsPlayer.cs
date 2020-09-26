using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = _player.transform.position;
        var lookDir = new Vector3(playerPos.x, transform.position.y, playerPos.z) - transform.position;
        var lookRot = Quaternion.FromToRotation(transform.forward, lookDir);

        transform.Rotate(transform.up, lookRot.eulerAngles.y);
    }
}
