using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour{
    public List<Transform> way_points;
    private float speed;
    private int index;
    private bool run = false;
    private int health_point;

    void Update(){
        if(run){
            Vector3 dst = way_points[index].position;

            Vector3 new_pos = Vector3.MoveTowards(transform.position, dst, speed*Time.deltaTime);
            transform.position = new_pos;

            float dist = Vector3.Distance(transform.position, dst);

            if(dist<=0.05){
                if(index < way_points.Count) index++;
            }
        }
    }

    public void SetEntity(string category){
        index = 0;
        switch(category){
            case "goblin":
                health_point = 30;
                speed = 3f;
                break;
            case "wolf":
                health_point = 18;
                speed = 4.5f;
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
    public void TakeDamage(int d){
        health_point -= d;
        if(health_point<=0){
            Destroy(gameObject);
        }
    }
}

