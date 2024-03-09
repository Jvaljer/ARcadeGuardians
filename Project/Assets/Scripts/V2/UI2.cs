using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI2 : MonoBehaviour{
    public GameObject Start_Pane; // 0
    public GameObject Tuto_Pane; // 1
    public GameObject Game_Pane; // 2
    public GameObject Infos; // 3

    public void Disable(int i){
        switch (i){
            case 0:
                Start_Pane.SetActive(false);
                break;
            case 1:
                Tuto_Pane.SetActive(false);
                break;
            case 2:
                Game_Pane.SetActive(false);
                break;
            case 3:
                Infos.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void Enable(int i){
        switch (i){
            case 0:
                Start_Pane.SetActive(true);
                break;
            case 1:
                Tuto_Pane.SetActive(true);
                break;
            case 2:
                Game_Pane.SetActive(true);
                break;
            case 3:
                Infos.SetActive(true);
                break;
            default:
                break;
        }
    }
}
