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

    public void ValidateSetup(){
        setup = true;
        range_area.GetComponent<TowerRange>().Init();
    }

    public void FireProjectileAt(Collider target){
        var projectile = Instantiate(projectile_prefab, launch_point.position, launch_point.rotation);
        //now we wanna make it go to the given target
        StartCoroutine(Shoot(projectile, target.gameObject));
        StartCoroutine(Reload(range_area));
    }

    private IEnumerator Reload(GameObject range){
        Debug.Log("just shot now reloading...");
        yield return new WaitForSeconds(2f);
        Debug.Log("Finished reloading");
        range.GetComponent<TowerRange>().Reload();
    }
    private IEnumerator Shoot(GameObject projectile, GameObject target){
        while(!projectile.GetComponent<Projectile>().ReachedTarget()){ //yet this returns error tho
            //must implement 
            // here we wanna make the target go in the direction of the target (just a bit)
            yield return new WaitForSeconds(0.1f);
        }
    }
}
