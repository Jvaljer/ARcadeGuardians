using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour{
    //linkings
    public GameObject ui;
    private GameObject level;

    //Game Logic Handling
    public void EnnemyReachedEnd(int damage){
        //must implement
    }


    //Settings Handling
    public void Initialize(){
        //Must implement
    }


    //Marker Detection Handling
    public void DetectLevel(GameObject level){
        ui.GetComponent<UI>().DetectLevel(level);
    }
    public void ValidateLevel(GameObject go_level){
        level = go_level;
    }
}
