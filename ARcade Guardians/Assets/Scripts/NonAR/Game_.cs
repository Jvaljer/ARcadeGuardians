using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_ : MonoBehaviour {
    public GameObject goblin_prefab;
    public GameObject level;
    public List<GameObject> towers;
    public GameObject level_start;
    public GameObject level_way;

    void Start(){
        Debug.Log("Game Start");
        level_start = GameObject.FindGameObjectsWithTag("LevelStart")[0];
    }
    void Update(){
        if (Input.GetKeyDown("space")){
            Debug.Log("LAUNCHING A GOBLIN");
            GameObject goblin = Instantiate(goblin_prefab, level_start.transform.position, Quaternion.identity);
            goblin.GetComponent<Goblin_>().SetWayPoints(level_way.transform);
            goblin.GetComponent<Goblin_>().Launch();
        }
    }
}
