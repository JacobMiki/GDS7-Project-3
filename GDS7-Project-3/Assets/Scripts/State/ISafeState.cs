﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.State
{
    public interface ISafeState
    {
        bool IsSafe { get; set; }
        Vector3 LastSafePosition { get; set; }
        Quaternion LastSafeRotation { get; set; }
    }
}
