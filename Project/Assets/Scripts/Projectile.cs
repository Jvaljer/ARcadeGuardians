using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
    public float life = 5f;
    private bool reached = false;
    private float speed;
    private float damage;

    void Awake(){
        Destroy(gameObject, life);
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag=="goblin"){
            reached = true;
            other.transform.parent.parent.gameObject.GetComponent<Ennemy>().TakeDamage(damage);
        }
    }

    public bool ReachedTarget(){
        return reached;
    }
    public float Speed(){
        return speed;
    }

    public void SetDamage(float d){
        damage = d;
    }
    public void SetSpeed(float s){
        speed = s;
    }
}
