using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GDS7.Group1.Project3.Assets
{
    public class ResetDollyCart : MonoBehaviour
    {
        void Start()
        {
            GetComponent<CinemachineDollyCart>().m_Position = 0;
        }
    }
}