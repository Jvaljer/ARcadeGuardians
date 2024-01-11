using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour{
    public string stype;

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="goblin"){
            if(stype=="fire"){
                other.gameObject.GetComponent<Ennemy>().FireAffected(1f);
            } else if(stype=="arrows"){
                other.gameObject.GetComponent<Ennemy>().ArrowsRain(2.5f);
            }
        } else if(other.gameObject.tag=="wolf"){
            if(stype=="fire"){
                Debug.Log("object is "+other.gameObject);
                other.gameObject.GetComponent<Ennemy>().FireAffected(1f);
            } else if(stype=="arrows"){
                other.gameObject.GetComponent<Ennemy>().ArrowsRain(2.5f);
            }
        }
    }
    public void OnTriggerExit(Collider other){
        if(other.gameObject.tag=="goblin"){
            if(stype=="fire"){
                other.gameObject.GetComponent<Ennemy>().FireHealed();
            } else if(stype=="arrows"){
                other.gameObject.GetComponent<Ennemy>().ArrowsStop();
            }
        } else if(other.gameObject.tag=="wolf"){
            if(stype=="fire"){
                other.gameObject.GetComponent<Ennemy>().FireHealed();
            } else if(stype=="arrows"){
                other.gameObject.GetComponent<Ennemy>().ArrowsStop();
            }
        }
            
    }
}
