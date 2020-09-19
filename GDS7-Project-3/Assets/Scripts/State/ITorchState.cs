using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.State
{
    public interface ITorchState
    {
        bool HasTorch { get; set; }

        GameObject DropTorch();
    }
}
