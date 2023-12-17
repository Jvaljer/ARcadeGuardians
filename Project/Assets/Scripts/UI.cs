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
    public GameObject detectpane;
    public GameObject level_popup;
    public GameObject tower_popup;
    public GameObject waveend;
    public GameObject infopane;
    public GameObject choice;
    public GameObject upgradechoosed;

    //variables
    private string marker = "";
    private string difficulty = "___";

    //predicates
    private bool level_set = false;

    //game attributes
    private GameObject level_marker;
    private GameObject marker_go;
    private GameObject current_tower;
    private GameObject tmp_marker;
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
        detectpane.SetActive(true);
        level_popup.SetActive(true);
    }
    public void DetectTower(GameObject marker, string typ){
        current_tower = marker;
        tower_type = typ;
        detectpane.SetActive(true);
        tower_popup.SetActive(true);
    }
    public void DetectFireball(GameObject f_marker){
        choice.SetActive(true);
        marker = "fireball";
        tmp_marker = f_marker;
    }
    public void DetectArrows(){
        choice.SetActive(true);
        marker = "arrows";
    }

    //Ignoring Handling
    public void IgnoreLevel(){
        detectpane.SetActive(false);
        level_popup.SetActive(false);
        level_marker = null; //wanna destroy or set to null ???
    }
    public void IgnoreTower(){
        detectpane.SetActive(false);
        tower_popup.SetActive(false);
        current_tower = null; //wanna destroy or set to null ???
    }
    public void IgnoreUpgrade(){
        //must implement
    }

    //Validating Methods
    public void ValidateLevel(){
        level_set = true;
        detectpane.SetActive(false);
        level_popup.SetActive(false);
        marker_go = level_marker;
        game.ValidateLevel(marker_go);
    }
    public void ValidateTower(){
        detectpane.SetActive(false);
        tower_popup.SetActive(false);
        game.ValidateTower(current_tower, tower_type);
    }
    public void ValidateUpgrade(){
        switch(marker){
            case "fireball":
                game.ApplyFireUpgrade(tmp_marker);
                break;
            case "arrows":
                break;
            default:
                break;
        }
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
    public void ChooseUpgrade(){
        switch(marker){ //this will serve for visual displaying
            case "fireball":
                break;
            case "arrows":
                break;
            default:
                break;
        }
        choice.SetActive(false);
        detectpane.SetActive(true);
        upgradechoosed.SetActive(true);
    }
    public void ChooseSpell(){
        switch(marker){
            case "fireball":
                break;
            case "arrows":
                break;
            default:
                break;
        }
        //must implement
    }
    public void EndWave(){
        wave_running = false;
        infopane.SetActive(true);
        waveend.SetActive(true);
    }

    public void NotEnoughGolds(){
        //must implement
    }
    public void WaveOk(){
        waveend.SetActive(false);
        infopane.SetActive(false);
    }

    //Some Setters
    public void SetGolds(int g){
        golds_txt.text = g.ToString();
    }
    public void SetWaves(int w){
        waves_txt.text = w.ToString();
    }
}
