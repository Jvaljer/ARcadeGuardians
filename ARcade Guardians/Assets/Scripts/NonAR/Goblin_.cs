using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_ : MonoBehaviour{
    public List<Transform> way_points;
    public float speed;
    private int index;
    private bool run = false;

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
    }
    public void SetWayPoints(Transform way){
        way_points = new List<Transform>();
        foreach (Transform point in way){
            way_points.Add(point);
        }
    }
}
