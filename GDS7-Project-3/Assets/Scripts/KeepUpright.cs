using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class KeepUpright : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
    }
}