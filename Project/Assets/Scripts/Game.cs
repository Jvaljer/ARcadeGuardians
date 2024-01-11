using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour{
    //linkings
    private Transform way_points;
    private Transform spawn_point;
    private Transform areas; 
    private List<Transform> path;
    public GameObject ui;
    private GameObject level;
    private bool ingame_ui = false;

    //prefabs
    public GameObject level_prefab;
    public GameObject archer_prefab;
    public GameObject bomber_prefab;
    public GameObject up_indicator;
    public GameObject fire_prefab;
    public GameObject arrows_prefab;

    //Materials
    public Material target_mat;
    public Material path_mat;


    //game variables
    private bool set = false;
    private string difficulty = "";
    private List<GameObject> towers;
    private int archer_cost = 200;
    private int bomber_cost = 275;
    private int spell_cost = 25;
    private List<bool> occupied;

    //vuforia handling
    private bool wave_running = false;
    private bool select = false;

    //player stuff
    private float base_hp = -1f;
    private int golds = 0;

    //wave handling
    private Wave wave;
    private int wave_cnt = 0;
    private int reached = -1;

    //Update Method -> Testing the crucial predicates on each frames
    private void Update(){
        if(set){
            if(base_hp<=0){
                PlayerLoose();
            }
            if(ingame_ui){
                if(wave_running){
                    if(GameObject.Find("Goblin(Clone)")==null && GameObject.Find("Wolf(Clone)")==null){
                        EndWave();
                    }
                }
            }
            if(wave_cnt>10){
                PlayerWin();
            }
            
            ui.GetComponent<UI>().SetGolds(golds);
            ui.GetComponent<UI>().SetHP(base_hp);
        }
    }

    //Game Logic Handling
    public void EnnemyReachedEnd(float damage){
        Debug.Log("taking damages "+damage+" on ennemy "+reached);
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
        golds += 300 - (10*reached) + (wave_cnt*15);
    }

    //Settings Handling
    public void Initialize(string diff){
        difficulty = diff;
        towers = new List<GameObject>();
        golds = 600;
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

        List<int> available = new List<int>();
        for(int i=0; i<occupied.Count; i++){
            if(occupied[i]==false){
                available.Add(i);
            }
        }
    }
    public void DetectFireball(GameObject marker){
        marker.transform.GetChild(0).gameObject.SetActive(true);
        ui.GetComponent<UI>().DetectFireball(marker);
    }
    public void DetectArrows(GameObject marker){
        marker.transform.GetChild(0).gameObject.SetActive(true);
        ui.GetComponent<UI>().DetectArrows(marker);
    }

    //UI Validation Handling
    public void ValidateLevel(GameObject marker){
        Vector3 pos = marker.transform.position;
        Quaternion rota = marker.transform.rotation;

        Destroy(marker);

        level = Instantiate(level_prefab, pos, rota);
        level.transform.localScale = new Vector3(0.004f, 0.01f, 0.004f);

        level.transform.GetChild(0).GetChild(18).GetComponent<EndCollision>().SetMultiplicator(difficulty);
        level.transform.GetChild(0).GetChild(18).GetComponent<EndCollision>().SetGame(this);
        way_points = level.transform.GetChild(2);
        spawn_point = level.transform.GetChild(0).GetChild(0);
        areas = level.transform.GetChild(1);
        
        Transform tmp = level.transform.GetChild(0);
        path = new List<Transform>();
        for(int i=0; i<tmp.childCount; i++){
            if(tmp.GetChild(i).gameObject.CompareTag("path")){
                path.Add(tmp.GetChild(i));
            }
        }

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
                    not_enough = true;
                }
                break;
            case "bomber":
                if(bomber_cost<=golds){
                    towers.Add( Instantiate(bomber_prefab, area.position, area.rotation) );
                    golds -= bomber_cost;
                } else {
                    not_enough = true;
                }
                break;
            default:
                break;
        }

        if(not_enough){
            ui.GetComponent<UI>().NotEnoughGolds(typ);
        } else {
            occupied[index] = true;
            towers[towers.Count-1].GetComponent<Tower>().Setup(area);
        }
        preview.transform.GetChild(0).gameObject.SetActive(false);
    }

    //Handling Upgrades Applyance
    public void ApplyFireUpgrade(GameObject marker){
        float min = 100f;
        GameObject tower = null;
        for(int i=0; i<towers.Count; i++){
            if(towers[i].GetComponent<Tower>().typ=="bomber"){
                Vector3 tower_pos = towers[i].transform.position;
                float dist = Vector3.Distance(marker.transform.position, tower_pos);
                if(dist <= min){
                    tower = towers[i];
                    min = dist;
                }
            }
        }
        if(tower!=null){
            if(((tower.GetComponent<Tower>().level)+1)*75 <= golds){
                tower.GetComponent<Tower>().Upgrade();
                int up_lvl = tower.GetComponent<Tower>().level;
                Vector3 up_pos = new Vector3(tower.transform.position.x, tower.transform.position.y+0.025f, tower.transform.position.z);
                GameObject indicator = Instantiate(up_indicator, up_pos, tower.transform.rotation);
                indicator.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
                float x = up_pos.x+(up_lvl*0.0025f);
                indicator.transform.position = new Vector3(x, up_pos.y, up_pos.z);
                golds -= up_lvl*75;
            }
        }
        marker.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ApplyArrowsUpgrade(GameObject marker){
        float min = 100f;
        GameObject tower = null;
        for(int i=0; i<towers.Count; i++){
            if(towers[i].GetComponent<Tower>().typ=="archer"){
                Vector3 tower_pos = towers[i].transform.position;
                float dist = Vector3.Distance(marker.transform.position, tower_pos);
                if(dist <= min){
                    tower = towers[i];
                    min = dist;
                }
            }
        }
        if(tower!=null){
            if(((tower.GetComponent<Tower>().level)+1)*75 <= golds){
                tower.GetComponent<Tower>().Upgrade();
                int up_lvl = tower.GetComponent<Tower>().level;
                Vector3 up_pos = new Vector3(tower.transform.position.x, tower.transform.position.y+0.025f, tower.transform.position.z);
                GameObject indicator = Instantiate(up_indicator, up_pos, tower.transform.rotation);
                indicator.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
                float x = up_pos.x+(up_lvl*0.0025f);
                indicator.transform.position = new Vector3(x, up_pos.y, up_pos.z);
                golds -= up_lvl*75;
            }
        }
        marker.transform.GetChild(0).gameObject.SetActive(false);
    }

    //Spell Handling
    public void ApplyFireSpell(GameObject marker){
        if(spell_cost>=golds){
            return;
        }
        golds -= spell_cost;
        select = true;
        Transform target = null;
        float min = 100f;
        for(int i=0; i<path.Count; i++){
            Vector3 path_part = path[i].transform.position;
            float dist = Vector3.Distance(marker.transform.position, path_part);
            if(dist <= min){
                target = path[i];
                min = dist;
            }
        }
        if(target!=null){
            StartCoroutine(FireSpell(target));
        }
        marker.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ApplyArrowsSpell(GameObject marker){
        if(spell_cost>=golds){
            return;
        }
        golds -= spell_cost;
        select = true;
        Transform target = null;
        float min = 100f;
        for(int i=0; i<path.Count; i++){
            Vector3 path_part = path[i].transform.position;
            float dist = Vector3.Distance(marker.transform.position, path_part);
            if(dist <= min){
                target = path[i];
                min = dist;
            }
        }
        if(target!=null){
            StartCoroutine(ArrowSpell(target));
        }
        marker.transform.GetChild(0).gameObject.SetActive(false);
    }

    private IEnumerator FireSpell(Transform trgt){
        GameObject fire = Instantiate(fire_prefab, trgt.position, trgt.rotation);
        fire.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        yield return new WaitForSeconds(8f);
        Destroy(fire);
    }

    private IEnumerator ArrowSpell(Transform trgt){
        GameObject arrows = Instantiate(arrows_prefab, trgt.position, trgt.rotation);
        arrows.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        yield return new WaitForSeconds(8f);
        Destroy(arrows);
    }
    //Win & Loose States
    public void PlayerWin(){
        ui.GetComponent<UI>().Win();
        set = false;
    }
    public void PlayerLoose(){
        ui.GetComponent<UI>().Lost();
        set = false;
    }


    //Coroutine used by UI
    public IEnumerator ShowSpellTarget(GameObject marker){
        Transform target = null;
        while(!select){
            float min = 100f;
            for(int i=0; i<path.Count; i++){
                Vector3 path_part = path[i].transform.position;
                float dist = Vector3.Distance(marker.transform.position, path_part);
                if(dist <= min){
                    target = path[i];
                    min = dist;
                }
            }

            if(target!=null){
                target.gameObject.GetComponent<Renderer>().material = target_mat;
            }
            
            yield return new WaitForSeconds(0.25f);

            if(target!=null){
                target.gameObject.GetComponent<Renderer>().material = path_mat;
            }
        }
    }

    //Some Setters
    public void SetIngameUI(bool b){ ingame_ui = b; }
}
