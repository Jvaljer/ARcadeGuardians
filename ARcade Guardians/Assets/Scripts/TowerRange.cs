using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour{
    public Tower tower;
    private bool init = true;
    private bool already_shot = false;
    private bool hold_max = false;

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
            if(!hold_max){
                if(other.tag=="atk-upgrade"){
                    tower.AddAtkUpgrade();
                    hold_max = tower.HoldMaxUpgrade();
                } else if(other.tag=="rng-upgrade"){
                    tower.AddRngUpgrade();
                    hold_max = tower.HoldMaxUpgrade();
                } else if(other.tag=="custom-upgrade"){
                    tower.AddCustomUpgrade(other.transform.parent.gameObject.GetComponent<Upgrade>().Type());
                    hold_max = tower.HoldMaxUpgrade();
                }
            }
        }
    }

    public void OnTriggerStay(Collider other){
        if(init){
            if(!already_shot){
                if(other.tag=="goblin"){
                    tower.FireProjectileAt(other);
                    already_shot = true;
                }
            }
            if(!hold_max){
                if(other.tag=="atk-upgrade"){
                    tower.AddAtkUpgrade();
                } else if(other.tag=="rng-upgrade"){
                    tower.AddRngUpgrade();
                } else if(other.tag=="custom-upgrade"){
                    tower.AddCustomUpgrade(other.transform.parent.gameObject.GetComponent<Upgrade>().Type());
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
