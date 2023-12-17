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
    public int level = 0;
    private int up_dmg = 0;
    private float up_spd = 0f;

    public void Setup(Transform tr){
        gameObject.transform.position = tr.position;
        gameObject.transform.rotation = tr.rotation;

        gameObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        validated = true;
        switch(typ){
            case "archer":
                projectile_s = 0.25f;
                base_dmg = 8f;
                reload_time = 0.85f;
                break;
            case "bomber":
                projectile_s = 0.125f;
                base_dmg = 13f;
                reload_time = 1.75f;
                break;
            default:
                break;
        }
    }

    public void Upgrade(){
        Debug.Log("this is the "+(level+1)+"th level for this tower");
        level++;
        switch(typ){
            case "archer":
                up_dmg += 4;
                up_spd += 0.25f;
                break;
            case "bomber":
                up_dmg += 6;
                up_spd += 0.125f;
                break;
            default:
                break;
        }
    }

    //Shoot Handling
    public void FireProjectileAt(Collider target){
        var projectile = Instantiate(projectile_prefab, launch_point.position, launch_point.rotation);
        projectile.GetComponent<Projectile>().SetDamage(base_dmg + up_dmg);
        projectile.GetComponent<Projectile>().SetSpeed(projectile_s - up_spd);

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
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(projectile);
    }

    //Some Getters
    public bool Valid(){ return validated; }
}
