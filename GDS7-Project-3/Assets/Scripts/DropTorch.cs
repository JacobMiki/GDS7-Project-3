using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets
{
    public class DropTorch : MonoBehaviour
    {
        public void DropPlayerTorch()
        {
            var player = GameObject.Find("Player");
            var animator = player.GetComponentInChildren<Animator>();
            animator.SetTrigger("Drop Torch");
            //_torch.transform.parent = transform;
            //Destroy(_torch.transform.Find("Torch").gameObject);
            //Destroy(_torch.GetComponent<Collider>(), 5f);
            //Destroy(_torch, 8f);
        }
    }
}