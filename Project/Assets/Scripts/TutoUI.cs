using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoUI : MonoBehaviour{
    public GameObject infopane;
    public GameObject tuto_ui;
    public GameObject welcome_txt;
    public GameObject level_txt;
    public GameObject tower_txt;
    public GameObject gold_txt;
    public GameObject ready_txt;
    public GameObject ok_btn;
    public GameObject next_btn;
    public GameObject skip_btn;

    private string current = "welcome";

    public void Next(){
        switch(current){
            case "welcome":
                welcome_txt.SetActive(false);
                current = "level";
                level_txt.SetActive(true);
                break;

            case "level":
                level_txt.SetActive(false);
                current = "tower";
                tower_txt.SetActive(true);
                break;

            case "tower":
                tower_txt.SetActive(false);
                current = "gold";
                gold_txt.SetActive(true);
                break;

            case "gold":
                gold_txt.SetActive(false);
                current = "ready";
                ready_txt.SetActive(true);
                next_btn.SetActive(false);
                ok_btn.SetActive(true);
                skip_btn.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void Skip(){
        switch(current){
            case "welcome":
                welcome_txt.SetActive(false);
                break;

            case "level":
                level_txt.SetActive(false);
                break;

            case "tower":
                tower_txt.SetActive(false);
                break;

            case "gold":
                gold_txt.SetActive(false);
                break;

            default:
                break;
        }
        ready_txt.SetActive(true);
        next_btn.SetActive(false);
        ok_btn.SetActive(true);
        skip_btn.SetActive(false);
    }

    public void Ok(){
        infopane.SetActive(false);
        tuto_ui.SetActive(false);
    }
}
