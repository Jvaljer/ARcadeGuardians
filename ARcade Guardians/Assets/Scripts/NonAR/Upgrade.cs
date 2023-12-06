using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour{
    public string type = "upgrade";
    
    public string Type(){
        return type;
    }
    public float speed = 1f;

    private void Update(){
        //testing without AR
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        if(Input.GetKey(KeyCode.UpArrow)){
            transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
        }
    }
}
