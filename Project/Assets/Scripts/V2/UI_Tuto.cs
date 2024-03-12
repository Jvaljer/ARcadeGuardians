using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tuto : MonoBehaviour{
    private string current = "fst";

    //UI components
    public GameObject first;
    public GameObject page1;
    public GameObject page2;
    public GameObject last;
    
    //Buttons
    public GameObject next;
    public GameObject previous;
    public GameObject skip;

    public void ClickNext(){
        switch(current){
            case "fst":
                first.SetActive(false);
                previous.SetActive(true);
                page1.SetActive(true);
                break;
            case "p1":
                page1.SetActive(false);
                page2.SetActive(true);
                break;
            case "p2":
                page2.SetActive(false);
                //page3.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void ClickPrevious(){
        switch(current){
            case "p1":
                page1.SetActive(false);
                first.SetActive(true);
                previous.SetActive(false);
                break;
            case "p2":
                page2.SetActive(false);
                page1.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void ClickSkip(){
        switch(current){
            case "fst":
                first.SetActive(false);
                break;
            case "p1":
                page1.SetActive(false);
                break;
            case "p2":
                page2.SetActive(false);
                break;
            default:
                break;
        }
        last.SetActive(true);
        next.SetActive(false);
    }
}
