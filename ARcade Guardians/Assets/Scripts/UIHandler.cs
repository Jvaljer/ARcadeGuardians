using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour{
    //components
    public GameObject game;
    public Text setting_label;
    //variables
    private string difficulty = "___";
    private string mode = "___";

    public void SetIndicator(){
        setting_label.text = difficulty+" & "+mode;
    }
    public void SetDifficultyEasy(){
        difficulty = "easy";
        SetIndicator();
    }
    public void SetDifficultyMedium(){
        difficulty = "medium";
        SetIndicator();
    }
    public void SetDifficultyHard(){
        difficulty = "hard";
        SetIndicator();
    }

    public void SetModeTest(){
        mode = "test";
        SetIndicator();
    }
    public void SetModeAR(){
        mode = "ar";
        SetIndicator();
    }

    public void Launch(){
        game.GetComponent<Game_>().Initialize(mode);
    }
}
