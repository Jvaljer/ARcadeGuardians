using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWayPoints : MonoBehaviour{
    public List<GameObject> way_points;
    public float speed = 2;
    public int index = 0;

    void Start() {
        
    }

    void Update(){
        Vector3 dst = way_points[index].transform.position;

        Vector3 new_pos = Vector3.MoveTowards(transform.position, dst, speed*Time.deltaTime);
        transform.position = new_pos;

        float dist = Vector3.Distance(transform.position, dst);

        if(dist<=0.05){
            if(index < way_points.Count) index++;
        }
    }
}
