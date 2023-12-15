using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour{
    //linkings
    public GameObject ui;
    private GameObject level;

    //game variables
    private bool set = false;
    private string difficulty = "";
    private List<GameObject> towers;
    private int archer_cost = 200;

    //player stuff
    private float base_hp = -1f;
    private int golds = 0;

    //Update Method -> Testing the crucial predicates on each frames
    private void Update(){
        if(set){
            if(base_hp<=0){
                //must implement
            }

            //must implement
        }
    }

    //Game Logic Handling
    public void EnnemyReachedEnd(float damage){
        base_hp -= damage;
    }


    //Settings Handling
    public void Initialize(string diff){
        difficulty = diff;
        towers = new List<GameObject>();
        golds = 500;

        switch(difficulty){
            case "easy":
                base_hp = 50f;
                break;
            case "medium":
                base_hp = 30f;
                break;
            case "hard":
                base_hp = 15.5f;
                break;
            default:
                Debug.Log("%%%%%  ERROR PARSING DIFFICULTY  %%%%%");
                break;
        }
    }


    //Marker Detection Handling
    public void DetectLevel(GameObject level){
        ui.GetComponent<UI>().DetectLevel(level);
    }
    public void DetectTower(GameObject tower){
        switch(tower.GetComponent<Tower>().typ){
            case "archer":
                if(golds<=archer_cost){
                    tower.SetActive(false);
                    return;
                }
                break;
            case "bomber":
                break;
            default:
                break;
        }
        ui.GetComponent<UI>().DetectTower(tower);
    }

    //UI Validation Handling
    public void ValidateLevel(GameObject go_level){
        level = go_level;
        GameObject.FindGameObjectWithTag("level-end").GetComponent<EndCollision>().SetMultiplicator(difficulty);
    }
    public void ValidateTower(GameObject go_tower){
        towers.Add(go_tower);
        towers[towers.Count-1].GetComponent<Tower>().Setup();
    }
}
