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
    private GameObject level_marker;
    private GameObject marker_go;
    private GameObject current_tower;
    private string tower_type;
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
    public void DetectLevel(GameObject marker){
        if(!level_set){
            level_marker = marker;
        }
        infopane.SetActive(true);
        level_popup.SetActive(true);
    }
    public void DetectTower(GameObject marker, string typ){
        current_tower = marker;
        tower_type = typ;
        infopane.SetActive(true);
        tower_popup.SetActive(true);
    }

    //Ignoring Handling
    public void IgnoreLevel(){
        infopane.SetActive(false);
        level_popup.SetActive(false);
        level_marker = null; //wanna destroy or set to null ???
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
        marker_go = level_marker;
        game.ValidateLevel(marker_go);
    }
    public void ValidateTower(){
        infopane.SetActive(false);
        tower_popup.SetActive(false);
        game.ValidateTower(current_tower, tower_type);
    }

    public void Launch(){
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
