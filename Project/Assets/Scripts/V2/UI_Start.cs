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
    public GameObject scale;

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

    public void ClickDiffOK(){
        difficulty.SetActive(false);
        game.SetDifficulty(difficulty_);
        selection.SetActive(true);
    }

    public void TrackLevel(bool b){
        Debug.Log("Inner UI Script got level: "+b);
        if(b){
            on.SetActive(true);
            off.SetActive(false);
        }
        on.SetActive(false);
        off.SetActive(true);
    }
}
