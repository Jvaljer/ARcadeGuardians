using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigation : MonoBehaviour{
    public GameObject settings;
    public GameObject ingame;

    public void GoToGame(){
        settings.SetActive(false);
        ingame.SetActive(true);
    }
}
