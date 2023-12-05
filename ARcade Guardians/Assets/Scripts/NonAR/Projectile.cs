using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
    public float life = 5f;
    private bool reached = false;
    private float speed = 1f;
    private int damage;

    void Awake(){
        Destroy(gameObject, life);
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag=="goblin"){
            reached = true;
            other.transform.parent.parent.gameObject.GetComponent<Goblin_>().TakeDamage(damage);
            //Destroy(gameObject);
        }
    }

    public bool ReachedTarget(){
        return reached;
    }
    public float Speed(){
        return speed;
    }

    public void SetDamage(int d){
        damage = d;
    }
}
