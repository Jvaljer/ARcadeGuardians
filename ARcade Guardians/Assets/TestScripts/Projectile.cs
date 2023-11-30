using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
    public float life = 5f;
    private bool reached = false;
    private float speed = 1f;

    void Awake(){
        Destroy(gameObject, life);
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag=="goblin"){
            reached = true;
            Destroy(other.transform.parent.parent.gameObject); //wanna replace that with damages instead
            Destroy(gameObject);
        }
    }

    public bool ReachedTarget(){
        return reached;
    }
    public float Speed(){
        return speed;
    }
}
