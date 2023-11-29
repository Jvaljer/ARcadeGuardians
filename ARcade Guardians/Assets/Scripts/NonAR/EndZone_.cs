using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone_ : MonoBehaviour{
    public void OnTriggerEnter(Collider other){
        if(other.tag=="goblin"){
            Debug.Log(other.transform.parent.gameObject);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
