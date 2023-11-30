using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour{
    public Tower tower;
    private bool init = true;
    private bool already_shot = false;

    public void OnTriggerEnter(Collider other){
        if(init){
            Debug.Log("SOMETHING ENTERING");
            if(!already_shot){
                if(other.tag=="goblin"){
                    Debug.Log("     -> It's a GOBLIN");
                    tower.FireProjectileAt(other);
                    already_shot = true;
                }
            }
        }
    }
    public void OnTriggerExit(Collider other){
        if(init){
            Debug.Log("SOMETHING EXITED");
        }
    }

    public void Init(){
        init = true;
    }
    public void Stop(){
        init = false;
    }
    public void Reload(){
        already_shot = false;
    }
}
