using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public event Action onFinish;

    public void Finish()
    {
        gameObject.SetActive(false);
        onFinish?.Invoke();
    }
}
