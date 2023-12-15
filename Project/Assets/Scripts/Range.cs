using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour{
    public Tower tower;

    //predicates
    private bool already_shot = false;

    public void OnTriggerEnter(Collider other){
        if(tower.Valid()){
            Debug.Log("ENTITY ENTERING");
            if(!already_shot){
                if(other.tag=="goblin" || other.tag=="wolf"){
                    Debug.Log("     -> It's an ENNEMY "+other.tag);
                    tower.FireProjectileAt(other);
                    already_shot = true;
                }
            }
        }
    }

    public void OnTriggerStay(Collider other){
        if(tower.Valid()){
            if(!already_shot){
                if(other.tag=="goblin" || other.tag=="wolf"){
                    tower.FireProjectileAt(other);
                    already_shot = true;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other){
        if(tower.Valid()){
            //must implement
        }
    }
    public void Reload(){
        already_shot = false;
    }
}
