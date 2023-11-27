using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
        Debug.Log("trigger enter");
    }
}
