using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{
    public GameObject range_area;
    public string t_type; 
    private bool setup;

    //projectiles handling
    public Transform launch_point;
    public GameObject projectile_prefab;
    public float launch_v = 10f;
    private int base_dmg;
    private float reload_time;

    //upgrade handling
    private int atk_bonus = 0;
    private float rng_bonus = 0f;
    private int held_upgrades = 0;
    private int max_upgrades = 3;


    public void ValidateSetup(){
        setup = true;
        range_area.GetComponent<TowerRange>().Init();
        switch (t_type){
            case "bomber":
                base_dmg = 15;
                reload_time = 1.5f;
                break;
            case "archer":
                base_dmg = 8;
                reload_time = 0.8f;
                break;
            case "knight":
                //this might be a different case
                break;
            default:
                base_dmg = 1;
                reload_time = 0.5f;
                break;
        }
    }

    public void FireProjectileAt(Collider target){
        var projectile = Instantiate(projectile_prefab, launch_point.position, launch_point.rotation);
        projectile.SetDamage(base_dmg);
        //now we wanna make it go to the given target
        StartCoroutine(Shoot(projectile, target.gameObject));
        StartCoroutine(Reload(range_area));
    }

    private IEnumerator Reload(GameObject range){
        Debug.Log("just shot now reloading...");
        yield return new WaitForSeconds(reload_time);
        Debug.Log("Finished reloading");
        range.GetComponent<TowerRange>().Reload();
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

    //upgrades method
    public void AddAtkUpgrade(){
        Debug.Log("Actually adding damage to this tower");
        //must implement
        held_upgrades++;
    }
    public void AddRngUpgrade(){
        //must implement
        held_upgrades++;
    }
    public void AddCustomUpgrade(string str){
        //must implement
    }
    
    public void RemoveAtkUpgrade(){
        Debug.Log("Actually adding damage to this tower");
        //must implement
        held_upgrades--;
    }
    public void RemoveRngUpgrade(){
        //must implement
        held_upgrades--;
    }
    public void RemoveCustomUpgrade(string str){
        //must implement
    }

    public bool HoldMaxUpgrade(){
        return (held_upgrades==max_upgrades);
    }
}
