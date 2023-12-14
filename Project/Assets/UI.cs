using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour{
    //components
    public Game game;
    public Text setting_label;
    public GameObject level_popup;

    //variables
    private string difficulty = "___";

    //predicates
    private bool level_set = false;

    //game attributes
    private GameObject final_level;

    //all methods
    private void SetIndicator(){
        setting_label.text = difficulty;
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

    public void DetectLevel(GameObject level){
        if(!level_set){
            final_level = level;
        }
        level_popup.SetActive(true);
    }
    public void ValidateLevel(){
        level_set = true;
        level_popup.SetActive(false);
        game.ValidateLevel(final_level);
    }

    public void Launch(){
        game.Initialize();
    }
}
