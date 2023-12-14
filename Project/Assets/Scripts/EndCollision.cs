using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollision : MonoBehaviour{
    public Game game;
    private float diff_mult;

    public void OnTriggerEnter(Collider other){
        int op_damage;
        switch (other.tag){
            case "goblin":
                op_damage = 3;
                break;
            case "wolf":
                op_damage = 5;
                break;
            default:
                op_damage = 0;
                break;
        }
        game.EnnemyReachedEnd(op_damage*diff_mult);
    }

    public void SetMultiplicator(string diff){
        switch(diff){
            case "easy":
                diff_mult = 1f;
                break;
            case "medium":
                diff_mult = 1.5f;
                break;
            case "hard":
                diff_mult = 2.5f;
                break;
            default:
                diff_mult = 0f;
                break;
        }
    }
}