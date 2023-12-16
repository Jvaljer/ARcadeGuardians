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
    private List<bool> occupied;

    //vuforia handling
    private bool lock_towers = false;
    private bool not_placed = true;

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
    }


    //Marker Detection Handling
    public void DetectLevel(GameObject level){
        ui.GetComponent<UI>().DetectLevel(level);
    }
    public void DetectTower(GameObject marker){
        if(lock_towers){
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
        //StartCoroutine(ShowPlacement(marker, available));
    }
    private IEnumerator ShowPlacement(GameObject preview, List<int> list){
        Debug.Log("ShowPlacement");
        float min = 100f;
        Transform nearest = null;
        GameObject indicator = Instantiate(indicator_prefab, Vector3.zero, Quaternion.identity);
        indicator.SetActive(false);
        while(not_placed){
            Debug.Log("NOT PLACED");
            foreach(int i in list){
                Transform area = areas.GetChild(i);
                Vector3 area_pos = area.position;
                float dist = Vector3.Distance(preview.transform.position, area_pos);
                if(dist <= min){
                    if(nearest!=null) nearest.gameObject.SetActive(true);
                    nearest = area;
                    min = dist;
                }
            }
            Debug.Log("indicator shall be on "+nearest);
            indicator.SetActive(true);
            indicator.transform.position = nearest.position;
            indicator.transform.rotation = nearest.rotation;
            nearest.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
        }
    }

    //UI Validation Handling
    public void ValidateLevel(GameObject marker){
        Vector3 pos = marker.transform.position;
        Quaternion rota = marker.transform.rotation;
        //Vector3 scale = new Vector3(0.015f,0.15f,0.015f);
        Vector3 scale = new Vector3(0.0025f,0.01f,0.0025f);

        Destroy(marker);

        level = Instantiate(level_prefab, pos, rota);
        level.transform.localScale = scale;

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
            Vector3 area_pos = areas.GetChild(i).position;
            float dist = Vector3.Distance(preview.transform.position, area_pos);
            Debug.Log("distance to area "+i+" is "+dist);
            if(dist <= min){
                area = areas.GetChild(i);
                min = dist;
                index = i;
            }
        }
        switch(typ){
            case "archer":
                towers.Add( Instantiate(archer_prefab, area.position, area.rotation) );
                break;
            case "bomber":
                towers.Add( Instantiate(bomber_prefab, area.position, area.rotation) );
                break;
            default:
                break;
        }
        towers[towers.Count-1].GetComponent<Tower>().Setup(area);
        not_placed = false;
    }

    //Handling marker track loss
    public void LooseTowerTrack(){
        //must implement
    }
    public void LooseLevelTrack(){
        //must implement
    }

    //Some Setters
    public void SetIngameUI(bool b){ ingame_ui = b; }
}
