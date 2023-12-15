using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour{
    //components
    public Game game;
    public Text setting_label;
    public Text golds_txt;
    public Text waves_txt;
    public GameObject settings;
    public GameObject ingame;
    public GameObject infopane;
    public GameObject level_popup;
    public GameObject tower_popup;

    //variables
    private string difficulty = "___";

    //predicates
    private bool level_set = false;

    //game attributes
    private GameObject tmp_level;
    private GameObject final_level;
    private GameObject current_tower;
    private bool wave_running = false;

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

    //Detecting methods
    public void DetectLevel(GameObject level){
        if(!level_set){
            tmp_level = level;
        }
        infopane.SetActive(true);
        level_popup.SetActive(true);
    }
    public void DetectTower(GameObject tower){
        current_tower = tower;
        infopane.SetActive(true);
        tower_popup.SetActive(true);
    }

    //Ignoring Handling
    public void IgnoreLevel(){
        infopane.SetActive(false);
        level_popup.SetActive(false);
        tmp_level = null; //wanna destroy or set to null ???
    }
    public void IgnoreTower(){
        infopane.SetActive(false);
        tower_popup.SetActive(false);
        current_tower = null; //wanna destroy or set to null ???
    }

    //Validating Methods
    public void ValidateLevel(){
        level_set = true;
        infopane.SetActive(false);
        level_popup.SetActive(false);
        final_level = tmp_level;
        game.ValidateLevel(final_level);
    }
    public void ValidateTower(){
        infopane.SetActive(false);
        tower_popup.SetActive(false);
        game.ValidateTower(current_tower);
    }

    public void Launch(){
        Debug.Log("[----------UI.LAUNCH----------]");
        game.Initialize(difficulty);
        settings.SetActive(false);
        ingame.SetActive(true);
        game.SetIngameUI(true);
    }
    public void LaunchNextWave(){
        if(wave_running){
            return;
        }
        game.LaunchWave();
        wave_running = true;
    }
    public void EndWave(){
        wave_running = false;
    }

    //Some Setters
    public void SetGolds(int g){
        golds_txt.text = g.ToString();
    }
    public void SetWaves(int w){
        waves_txt.text = w.ToString();
    }
}
