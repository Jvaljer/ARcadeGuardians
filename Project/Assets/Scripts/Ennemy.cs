using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour{
    public List<Transform> way_points;
    private float move_refresh;
    private float health_point;
    private bool on_fire = false;
    private bool arrows_on = false;
    private string cat = "";

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

                yield return new WaitForSeconds(move_refresh);
            }
        }
    }

    public void SetEntity(string category){
        switch(category){
            case "goblin":
                health_point = 45f;
                move_refresh = 0.05f;
                cat = "GOBLIN";
                break;
            case "wolf":
                health_point = 28f;
                move_refresh = 0.025f;
                cat = "WOLF";
                break;
            default:
                break;
        }
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

    public void FireAffected(float dmg){
        Debug.Log("entered fire");
        on_fire = true;
        StartCoroutine(FireDamage(dmg));
    }
    private IEnumerator FireDamage(float d){
        float time = 0f;
        while(on_fire){
            TakeDamage(d);
            yield return new WaitForSeconds(0.5f);
            time += 0.5f;
            if(time>=8f) on_fire = false;
        }
    }
    public void FireHealed(){
        Debug.Log("exited fire");
        on_fire = false;
    }

    public void ArrowsRain(float dmg){
        arrows_on = true;
        StartCoroutine(ArrowsDamage(dmg));
    }
    private IEnumerator ArrowsDamage(float d){
        float time = 0f;
        while(arrows_on){
            TakeDamage(d);
            yield return new WaitForSeconds(0.75f);
            time += 0.75f;
            if(time>=8f) arrows_on = false;
        }
    }
    public void ArrowsStop(){
        arrows_on = false;
    }
}

