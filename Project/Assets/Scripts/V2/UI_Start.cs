using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Start : MonoBehaviour{
    //game component
    public Game2 game;
    private string difficulty_;

    //parent
    public UI2 ui;

    //UI components
    public GameObject difficulty;
    public Text info;
    public GameObject selection;
    public GameObject on;
    public GameObject off;
    public GameObject display;
    public GameObject scale;
    public Slider slider;

    //other attributes
    private int level = 0;
    private int lvl_cnt = 1;
    public GameObject lvl0;
    public GameObject lvl1;


    void Start(){
        //must implement
    }

    public void ClickDiff(string str){
        difficulty_ = str;
        Display(str);
    }

    public void Display(string str){
        switch(str){
            case "beginner":
                info.text = "This is the easiest way to play this game, it allows you to take your time and discover the game, its features, but also the possible techniques andstrategies to adopt.";
                break;
            case "intermediate":
                info.text = "This is a nice way to challenge yourself as a confirmed player, ennemies are gonna be faster and hit harder, and your towers' cost will be increased significantly";
                break;
            case "hardcore":
                info.text = "Here you wanna prove your true capacities to the world, and take the ultimate challenge of finishing the final difficulty of this game. None knows if that is even possible...";
                break;
            default:
                break;
        }
    }
    public void DisplayLevel(){
        Debug.Log("we have level: "+level);
        switch(level){
            case 0:
                lvl0.SetActive(true);
                lvl1.SetActive(false);
                break;
            case 1:
                lvl0.SetActive(false);
                lvl1.SetActive(true);
                break;
            default:
                lvl0.SetActive(false);
                lvl1.SetActive(false);
                break;
        }
    }

    public void ClickDiffOK(){
        difficulty.SetActive(false);
        game.SetDifficulty(difficulty_);
        selection.SetActive(true);
    }
    public void ClickSelectOK(){
        selection.SetActive(false);
        game.SetLevel(level);
        scale.SetActive(true);
        slider.onValueChanged.AddListener(delegate { SliderValueChange(); });
    }
    public void ClickLaunch(){
        scale.SetActive(false);
        game.Init();
        ui.Disable(0);
        ui.Enable(1);
    }

    public void TrackLevel(bool b){
        if(b){
            on.SetActive(true);
            off.SetActive(false);
            display.SetActive(true);
            return;
        }
        on.SetActive(false);
        off.SetActive(true);
        //display.SetActive(false);
    }

    public void Next(){
        level++;
        if(level>lvl_cnt){
            level = 0;
        }
        DisplayLevel();
    }
    public void Previous(){
        level--;
        if(level<0){
            level = lvl_cnt;
        }
        DisplayLevel();
    }

    public void SliderValueChange(){
        float value = slider.value;
        game.ScaleLevel(value);
    }
}
