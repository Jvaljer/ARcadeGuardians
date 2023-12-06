using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone_ : MonoBehaviour{
    public Game_ game;

    public void OnTriggerEnter(Collider other){
        int op_damage;
        switch (other.tag){
            case "goblin":
                op_damage = 3;
                break;
            case "wolf":
                op_damage = 5;
                break;
            case "troll":
                op_damage = 10;
                break;
            default:
                op_damage = 0;
                break;
        }

        game.EnnemyReached(op_damage);
    }
}
