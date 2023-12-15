using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{
    //linkings
    private Game game;
    private bool validated = false;

    //components
    public GameObject range;
    public Transform launch_point;
    public GameObject projectile_prefab;

    //attributes
    public string typ;
    private float projectile_s;
    private float base_dmg;
    private float reload_time;

    public void Setup(){
        validated = true;
        switch(typ){
            case "archer":
                projectile_s = 1.5f;
                base_dmg = 8f;
                reload_time = 0.85f;
                break;
            case "bomber":
                break;
            default:
                break;
        }
    }

    //Shoot Handling
    public void FireProjectileAt(Collider target){
        var projectile = Instantiate(projectile_prefab, launch_point.position, launch_point.rotation);
        projectile.GetComponent<Projectile>().SetDamage(base_dmg);
        projectile.GetComponent<Projectile>().SetSpeed(projectile_s);
        //now we wanna make it go to the given target
        StartCoroutine(Shoot(projectile, target.gameObject));
        StartCoroutine(Reload(range));
    }

    private IEnumerator Reload(GameObject range){
        yield return new WaitForSeconds(reload_time);
        range.GetComponent<Range>().Reload();
    }
    private IEnumerator Shoot(GameObject projectile, GameObject target){
        Projectile script = projectile.GetComponent<Projectile>();
        while(!script.ReachedTarget()){
            // Calculate the direction vector from the projectile to the target
            Vector3 dir = (target.transform.position - projectile.transform.position).normalized;
            // Update the projectile's position based on the direction
            projectile.transform.position += dir * script.Speed() * Time.deltaTime;
            yield return null; //new WaitForSeconds(0.1f)
        }
    }

    //Some Getters
    public bool Valid(){ return validated; }
}
