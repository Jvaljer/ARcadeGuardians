using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour{
    //Attributes
    private int ennemies_in;
    private int ennemies_out;
    private Dictionary<string, int> enemies = new Dictionary<string, int>();
        
    //Constructor
    public Wave(int cpt, string diff){
        switch(diff){
            case "easy":
                break;
            case "medium":
                break;
            case "hard":
                break;
            default:
                break;
        }
    }

    //Methods
    public void Start(){
        StartCoroutine(WaveRun());
    }

    //Coroutines
    private IEnumerator WaveRun(){
            //must implement
        yield return new WaitForSeconds(1f);
    }
}