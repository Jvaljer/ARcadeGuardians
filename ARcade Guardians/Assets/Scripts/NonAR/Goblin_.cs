using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_ : MonoBehaviour{
    public List<Transform> way_points;
    public float speed;
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

    public void Launch(){
        run = true;
        index = 0;
        health_point = 30;
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
