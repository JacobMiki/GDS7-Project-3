using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class Door : MonoBehaviour
    {
        public void Open()
        {
            Destroy(gameObject);
        }
    }
}