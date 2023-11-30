using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
    public float life = 5f;
    private bool reached = false;
    void Awake(){
        Destroy(gameObject, life);
    }
    public bool ReachedTarget(){
        return reached;
    }
}
