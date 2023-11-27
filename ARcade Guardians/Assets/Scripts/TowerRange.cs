using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour{
    public Tower tower;
    public bool init = false;

    public void OnTriggerEnter(Collider other){
        if(init){
            Debug.Log("something entered the area -> ATTACK if ennemy");
        }
    }
    public void OnTriggerExit(Collider other){
        if(init){
            Debug.Log("something exited the area -> SWITCH focus if still ennemies");
        }
    }
}
