using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{
    private Game_ game;
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
    private float spd_bonus = 0f;
    private int held_upgrades = 0;
    private int max_upgrades = 3;
    private int atkup_cost;
    private int spdup_cost;


    public void Start(){
        game = GameObject.FindGameObjectsWithTag("script-holder")[0].GetComponent<Game_>();
        setup = true;
        range_area.GetComponent<TowerRange>().Init();
        switch (t_type){
            case "bomber":
                base_dmg = 15;
                reload_time = 1.5f;
                atkup_cost = 150;
                spdup_cost = 200;
                break;
            case "archer":
                base_dmg = 8;
                reload_time = 0.8f;
                atkup_cost = 200;
                spdup_cost = 150;
                break;
            case "knight":
                //this might be a different case
                break;
            default:
                base_dmg = 1;
                reload_time = 0.5f;
                atkup_cost = 100;
                spdup_cost = 100;
                break;
        }
    }

    public void FireProjectileAt(Collider target){
        var projectile = Instantiate(projectile_prefab, launch_point.position, launch_point.rotation);
        projectile.GetComponent<Projectile>().SetDamage(base_dmg+atk_bonus);
        //now we wanna make it go to the given target
        StartCoroutine(Shoot(projectile, target.gameObject));
        StartCoroutine(Reload(range_area));
    }

    private IEnumerator Reload(GameObject range){
        Debug.Log("just shot now reloading...");
        yield return new WaitForSeconds(reload_time-spd_bonus);
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
    public void TryAddAtkUp(){
        //here we only wanna test if the player got enough golds
        int player_gold = game.PlayerGold();
        if(player_gold>=atkup_cost){
            AddAtkUpgrade();
            game.PlayerPay(atkup_cost);
        }
    }
    public void TryAddSpdUp(){
        int player_gold = game.PlayerGold();
        if(player_gold>=spdup_cost){
            AddSpdUpgrade();
            game.PlayerPay(spdup_cost);
        }
    }
    private void AddAtkUpgrade(){
        atk_bonus += 3;
        held_upgrades++;
    }
    private void AddSpdUpgrade(){
        spd_bonus += 0.5f;
        held_upgrades++;
    }
    
    public void RemoveAtkUpgrade(){
        atk_bonus -= 3;
        held_upgrades--;
        game.PlayerRefund(atkup_cost);
    }
    public void RemoveSpdUpgrade(){
        spd_bonus -= 0.5f;
        held_upgrades--;
        game.PlayerRefund(spdup_cost);
    }

    public bool HoldMaxUpgrade(){
        return (held_upgrades==max_upgrades);
    }
}
