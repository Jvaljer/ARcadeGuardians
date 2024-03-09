using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2 : MonoBehaviour{
    private string difficulty = "none";
    public UI2 ui;

    void Start(){
        //must implement
    }

    void Update(){
        //must implement
    }

    public void SetDifficulty(string diff){
        difficulty = diff;
    }

    //Images Target Detection
    public void DetectLevel(){
        Debug.Log("Game Detected Level");
        ui.TrackLevel(true);
    }
    public void LooseLevel(){
        ui.TrackLevel(false);
    }

}
