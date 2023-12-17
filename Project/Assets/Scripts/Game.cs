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
    public GameObject archer_prefab;
    public GameObject bomber_prefab;
    public GameObject indicator_prefab;

    //game variables
    private bool set = false;
    private string difficulty = "";
    private List<GameObject> towers;
    private int archer_cost = 200;
    private int bomber_cost = 275;
    private List<bool> occupied;

    //vuforia handling
    private bool wave_running = false;
    private bool not_placed = true;

    //player stuff
    private float base_hp = -1f;
    private int golds = 0;

    //wave handling
    private Wave wave;
    private int wave_cnt = 0;
    private bool wave_just_ended = false;
    private int reached = -1;

    //Update Method -> Testing the crucial predicates on each frames
    private void Update(){
        if(set){
            if(base_hp<=0){
                //loosing condition
            }
            if(ingame_ui){
                if(wave_running){
                    if(GameObject.Find("Goblin(Clone)")==null && GameObject.Find("Wolf(Clone)")==null){
                        EndWave();
                    }
                }
            }
            if(wave_cnt>10){
                //winning condition
            }
            //must implement
            ui.GetComponent<UI>().SetGolds(golds);
        }
    }

    //Game Logic Handling
    public void EnnemyReachedEnd(float damage){
        base_hp -= damage;
        reached++;
    }
    public void LaunchWave(){
        //we wanna start a coroutine for the wave's progression
        wave_running = true;
        wave.Set(wave_cnt, difficulty, way_points, spawn_point, this);
        wave.Begin();
        reached = 0;
    }
    public void EndWave(){
        wave_running = false;
        wave.Reset();
        ui.GetComponent<UI>().EndWave();
        wave_cnt++;
        ui.GetComponent<UI>().SetWaves(wave_cnt);
        golds += 100*wave_cnt - (10*reached);
    }

    //Settings Handling
    public void Initialize(string diff){
        difficulty = diff;
        towers = new List<GameObject>();
        golds = 500;
        occupied = new List<bool>();

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
        set = true;
    }


    //Marker Detection Handling
    public void DetectLevel(GameObject level){
        ui.GetComponent<UI>().DetectLevel(level);
    }
    public void DetectTower(GameObject marker){
        marker.transform.GetChild(0).gameObject.SetActive(true);
        if(wave_running){
            marker.SetActive(false);
            return;
        }
        ui.GetComponent<UI>().DetectTower(marker, marker.tag);

        not_placed = true;
        float min = 100f;

        Transform area = null;
        List<int> available = new List<int>();
        for(int i=0; i<occupied.Count; i++){
            if(occupied[i]==false){
                available.Add(i);
            }
        }
    }

    //UI Validation Handling
    public void ValidateLevel(GameObject marker){
        Vector3 pos = marker.transform.position;
        Quaternion rota = marker.transform.rotation;

        Destroy(marker);

        level = Instantiate(level_prefab, pos, rota);
        level.transform.localScale = new Vector3(0.0025f, 0.01f, 0.0025f);

        level.transform.GetChild(0).GetChild(18).GetComponent<EndCollision>().SetMultiplicator(difficulty);
        level.transform.GetChild(0).GetChild(18).GetComponent<EndCollision>().SetGame(this);
        way_points = level.transform.GetChild(2);
        spawn_point = level.transform.GetChild(0).GetChild(0);
        areas = level.transform.GetChild(1);
        wave = level.GetComponent<Wave>();

        for(int i=0; i<areas.childCount; i++){
            occupied.Add(false);
        }
    }
    public void ValidateTower(GameObject preview, string typ){
        //here we wanna add the tower to the very next area 
        float min = 100f;
        Transform area = null;
        int index = -1;
        for(int i=0; i<areas.childCount; i++){
            if(!occupied[i]){
                Vector3 area_pos = areas.GetChild(i).position;
                float dist = Vector3.Distance(preview.transform.position, area_pos);
                if(dist <= min){
                    area = areas.GetChild(i);
                    min = dist;
                    index = i;
                }
            }
        }
        bool not_enough = false;
        switch(typ){
            case "archer":
                if(archer_cost<=golds){
                    towers.Add( Instantiate(archer_prefab, area.position, area.rotation) );
                    golds -= archer_cost;
                } else {
                    Debug.Log("Not Enough to create ARCHER");
                    not_enough = true;
                }
                break;
            case "bomber":
                if(bomber_cost<=golds){
                    Debug.Log("Creating BOMBER");
                    towers.Add( Instantiate(bomber_prefab, area.position, area.rotation) );
                    golds -= bomber_cost;
                } else {
                    Debug.Log("Not Enough to create BOMBER");
                    not_enough = true;
                }
                break;
            default:
                break;
        }

        if(not_enough){
            ui.GetComponent<UI>().NotEnoughGolds();
        } else {
            towers[towers.Count-1].GetComponent<Tower>().Setup(area);
            not_placed = false;
        }
        preview.transform.GetChild(0).gameObject.SetActive(false);
    }
    //Handling marker track loss
    public void LooseTowerTrack(GameObject marker){
        //must implement
    }
    public void LooseLevelTrack(){
        //must implement
    }

    //Some Setters
    public void SetIngameUI(bool b){ ingame_ui = b; }
}
