using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2 : MonoBehaviour{
    private string difficulty = "none";
    public UI2 ui;

    //Level's Tracking & DIsplay
    private GameObject level;
    private GameObject lvl_mark;

    //Levels Prefab
    public GameObject lvl0;
    public GameObject lvl1;


    void Update(){
        //must implement
    }

    public void Init(){
        //must implement
    }

    //Attributes Setting
    public void SetDifficulty(string diff){
        difficulty = diff;
    }
    public void SetLevel(int i){
        //must implement
        switch(i){
            case 0:
                lvl0.SetActive(true);
                break;
            case 1:
                lvl1.SetActive(true);
                break;
            default:
                break;
        }
    }

    //Images Target Detection
    public void DetectLevel(GameObject marker){
        Debug.Log("Game Detected Level");
        lvl_mark = marker;
        ui.TrackLevel(true);
    }
    public void LooseLevel(){
        lvl_mark = null;
        ui.TrackLevel(false);
    }

}
