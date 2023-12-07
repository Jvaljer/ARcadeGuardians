using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_ : MonoBehaviour {
    public GameObject archer_test;
    public GameObject bomber_test;
    public GameObject goblin_prefab;
    public GameObject level;
    public List<GameObject> towers;
    private Vector3 level_start;
    public GameObject level_way;
    private int difficulty_level = 0;
    private int base_hp = 45;
    private int player_gold;
    private bool setup;
    private string gmode = "";


    public void Initialize(string mode){
        Debug.Log("Game Showing up");
        if(mode=="test"){
            gmode = "t";
            level.SetActive(true);
        setup = true;
        } else if(mode=="ar"){
            gmode = "ar";
            //must implement
        }
    }
    void Start(){
        Debug.Log("Game Initializes in Background");
        level = GameObject.FindGameObjectsWithTag("level")[0];
        level_start = GameObject.FindGameObjectsWithTag("LevelStart")[0].transform.position;
        level.SetActive(false);
        setup = false;
    }
    void Update(){
        if(setup){
            if(gmode=="t"){
                if (Input.GetKeyDown(KeyCode.A)){
                    Debug.Log("Try Placing an Archer");
                    TryAddArcher(archer_test);
                }
                if (Input.GetKeyDown(KeyCode.B)){
                    Debug.Log("Try Placing a Bomber");
                    TryAddBomber(bomber_test);
                }
                if (Input.GetKeyDown("space")){
                    /*
                    Debug.Log("LAUNCHING A GOBLIN");
                    GameObject goblin = Instantiate(goblin_prefab, level_start, Quaternion.identity);
                    goblin.GetComponent<Goblin_>().SetWayPoints(level_way.transform);
                    goblin.GetComponent<Goblin_>().Launch();
                    */
                    Debug.Log("Launching a Wave !");
                    LaunchWave(difficulty_level);
                }
            }
        }
    }
    public int PlayerGold(){
        return player_gold;
    }
    public void EnnemyReached(int damage){
        base_hp -= damage;
        if(base_hp<=0){
            Loose();
        }
    }
    public void LaunchWave(int difficulty){
        //must implement
    }
    public void Loose(){
        //must implement
    }

    public void TryAddArcher(GameObject tower){
        //must implement
    }
    public void TryAddBomber(GameObject tower){
        //must implement
    }
    public void PlayerPay(int cost){
        player_gold -= cost;
    }
    public void PlayerRefund(int cost){
        player_gold += cost;
    }
}
