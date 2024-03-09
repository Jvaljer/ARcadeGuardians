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
    public GameObject scale;

    void Start(){
        //here we might wanna reset to the first state ?
    }

    public void ClickDiff(string str){
        Debug.Log("clicked on difficulty: "+str);
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
}
