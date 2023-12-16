using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour{
    //linkings
    private Transform way_points;
    private Transform spawn_point;
    private Transform areas; //this is the level's entity that encapsulate all the areas
    public GameObject ui;
    private GameObject level;
    private bool ingame_ui = false;

    //prefabs
    public GameObject level_prefab;

    //game variables
    private bool set = false;
    private string difficulty = "";
    private List<GameObject> towers;
    private int archer_cost = 200;

    //vuforia handling
    private bool lock_towers = false;

    //player stuff
    private float base_hp = -1f;
    private int golds = 0;

    //wave handling
    private Wave wave;
    private int wave_cnt = 0;
    private bool wave_just_ended = false;

    //Update Method -> Testing the crucial predicates on each frames
    private void Update(){
        if(set){
            if(base_hp<=0){
                //loosing condition
            }
            if(ingame_ui){
                //refresh the ui texts of golds & waves
                if(wave_just_ended){
                    ui.GetComponent<UI>().SetWaves(wave_cnt);
                    wave_just_ended = false;
                }
            }
            if(wave_cnt>10){
                //winning condition
            }
            //must implement
        }
    }

    //Game Logic Handling
    public void EnnemyReachedEnd(float damage){
        base_hp -= damage;
    }
    public void LaunchWave(){
        //we wanna start a coroutine for the wave's progression
        lock_towers = true;
        wave.Set(wave_cnt, difficulty, way_points, spawn_point, this);
        wave.Begin();
    }
    public void EndWave(){
        wave_just_ended = true;
        wave.Reset();
        wave_cnt++;
    }

    //Settings Handling
    public void Initialize(string diff){
        difficulty = diff;
        towers = new List<GameObject>();
        golds = 500;

        switch(difficulty){
            case "easy":
                base_hp = 50f;
                break;
            case "medium":
                base_hp = 30f;
                break;
            case "hard":
                base_hp = 15.5f;
                break;
            default:
                Debug.Log("%%%%%  ERROR PARSING DIFFICULTY  %%%%%");
                break;
        }

        ui.GetComponent<UI>().SetGolds(golds);
        ui.GetComponent<UI>().SetWaves(0);
    }


    //Marker Detection Handling
    public void DetectLevel(GameObject level){
        ui.GetComponent<UI>().DetectLevel(level);
    }
    public void DetectTower(GameObject tower){
        if(lock_towers){
            tower.SetActive(false);
            return;
        }
        switch(tower.GetComponent<Tower>().typ){
            case "archer":
                if(golds<=archer_cost){
                    tower.SetActive(false);
                    return;
                }
                break;
            case "bomber":
                break;
            default:
                break;
        }
        ui.GetComponent<UI>().DetectTower(tower);
    }

    //UI Validation Handling
    public void ValidateLevel(GameObject marker){
        Vector3 pos = marker.transform.position;
        Quaternion rota = marker.transform.rotation;
/*
        level = Instantiate(level_prefab, pos, rota);
        level.transform.localScale = scale;
*/
        level = marker.transform.GetChild(0).gameObject;

        GameObject.FindGameObjectWithTag("level-end").GetComponent<EndCollision>().SetMultiplicator(difficulty);
        GameObject.FindGameObjectWithTag("level-end").GetComponent<EndCollision>().SetGame(this);
        way_points = GameObject.FindGameObjectWithTag("path").transform;
        spawn_point = GameObject.FindGameObjectWithTag("spawn").transform;
        areas = GameObject.FindGameObjectWithTag("areas").transform;
        wave = level.GetComponent<Wave>();
    }
    public void ValidateTower(GameObject go_tower){
        //here we wanna add the tower to the very next area 
        towers.Add(go_tower);
        float min = 100f;
        Transform area = null;
        for(int i=0; i<areas.childCount; i++){
            Vector3 area_pos = areas.GetChild(i).position;
            float dist = Vector3.Distance(go_tower.transform.position, area_pos);
            if(dist <= min){
                area = areas.GetChild(i);
                min = dist;
            }
        }
        towers[towers.Count-1].GetComponent<Tower>().Setup(area);
    }

    //Some Setters
    public void SetIngameUI(bool b){ ingame_ui = b; }
}
