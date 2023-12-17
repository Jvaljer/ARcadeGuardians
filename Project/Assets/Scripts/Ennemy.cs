using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour{
    public List<Transform> way_points;
    private float speed;
    private bool run = false;
    private float health_point;
    private string typ = "";

    public void Travel(){
        StartCoroutine(MoveAll());
    }
    private IEnumerator MoveAll(){
        for(int i=0; i<way_points.Count; i++){
            Vector3 dest = way_points[i].position;
            Vector3 pos = transform.position;
            
            float x_dif = (dest.x-pos.x)/30f;
            float y_dif = (dest.y-pos.y)/30f;
            float z_dif = (dest.z-pos.z)/30f;

            float dist = Vector3.Distance(pos, dest);
            float x = pos.x;
            float y = pos.y;
            float z = pos.z;

            for(int j=0; j<30; j++){
                x += x_dif;
                y += y_dif;
                z += z_dif;
                Vector3 new_pos = new Vector3(x,y,z);
                transform.position = new_pos;
                dist = Vector3.Distance(transform.position, dest);

                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void SetEntity(string category){
        switch(category){
            case "goblin":
                health_point = 30f;
                speed = 3f;
                typ = "goblin";
                break;
            case "wolf":
                health_point = 18f;
                speed = 4.5f;
                typ = "wolf";
                break;
            default:
                break;
        }
    }
    public void Launch(){
        run = true;
    }
    public void SetWayPoints(Transform way){
        way_points = new List<Transform>();
        foreach (Transform point in way){
            way_points.Add(point);
        }
    }
    public void TakeDamage(float d){
        health_point -= d;
        if(health_point<=0){
            Destroy(gameObject);
        }
    }
    
    public void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("level-end")){
            Destroy(gameObject);
        }
    }
}

