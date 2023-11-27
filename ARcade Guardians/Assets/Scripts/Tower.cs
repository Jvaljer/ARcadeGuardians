using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{
    public GameObject range_area;
    //predicates
    private bool setup;

    private void Start(){
        setup = false;
    }

    private void Update(){
        if(setup){

        }
    }

    public void ValidateSetup(){
        setup = true;
        range_area.GetComponent<TowerRange>().init = true;
    }
}
