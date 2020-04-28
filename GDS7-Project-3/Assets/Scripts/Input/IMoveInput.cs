using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input
{
    interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}
