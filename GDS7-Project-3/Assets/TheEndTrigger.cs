using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using UnityEngine;

public class TheEndTrigger : MonoBehaviour
{
    [SerializeField] private string _endScreenName;
    public void Trigger()
    {
        GameObject.FindWithTag("Screens").transform.Find(_endScreenName).gameObject.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<CharacterInput>().InputsEnabled = false;
    }
}
