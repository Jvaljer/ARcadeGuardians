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

    public void Setup(Transform tr){
        gameObject.transform.position = tr.position;
        gameObject.transform.rotation = tr.rotation;

        gameObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
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

        Vector3 scale = new Vector3(0.005f, 0.005f, 0.005f);
        projectile.transform.localScale = scale;

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
            Vector3 dir = (target.transform.position - projectile.transform.position).normalized;
            projectile.transform.position += dir * script.Speed() * Time.deltaTime;
            yield return new WaitForSeconds(0.15f);
        }
        Destroy(projectile);
    }

    //Some Getters
    public bool Valid(){ return validated; }
}
