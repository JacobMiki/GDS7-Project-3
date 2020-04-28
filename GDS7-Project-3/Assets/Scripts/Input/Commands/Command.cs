using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    abstract class Command : MonoBehaviour
    {
        public virtual void Execute() { }
        public virtual void Complete() { }
    }
}
