using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour{
    //linkings
    private Game game;
    private Transform spawn;
    private Transform way;

    //ennemies prefabs
    public GameObject goblin;
    public GameObject wolf;

    //Attributes
    private int ennemies_in;
    private Dictionary<string, int> ennemies = new Dictionary<string, int>();
    private List<string> queue;
        
    //Constructor
    public void Set(int cpt, string diff, Transform way_tr, Transform spawn_tr, Game G){
        game = G;
        spawn = spawn_tr;
        way = way_tr;

        switch(diff){
            case "easy":
                ennemies_in = 13+ (5*cpt);
                break;
            case "medium":
                ennemies_in = 18+ (7*cpt);
                break;
            case "hard":
                ennemies_in = 25+ (10*cpt);
                break;
            default:
                ennemies_in = -1;
                break;
        }
        Debug.Log("we want "+ennemies_in+" ennemies");
        queue = new List<string>();

        int half = ennemies_in / 2;
        queue.Add("w");
        int g_cnt = half;
        int w_cnt = 0;

        queue = new List<string>();
        for (int i = 0; i < half; i++) {
            queue.Add("g");
            float r = Random.Range(0f, 100f);
            Debug.Log("r="+r);
            if(r<(25f+(cpt*2f))){
                Debug.Log("Adding a WOLF");
                w_cnt++;
                queue.Add("w");
            } else {
                if(diff=="medium" && (r<50f+(cpt*2.5f))){
                    w_cnt++;
                    queue.Add("w");
                } else if(diff=="hard" && (r<75f+(cpt*2f))){
                    w_cnt++;
                    queue.Add("w");
                } else {
                    g_cnt++;
                    queue.Add("g");
                }
            }
        }
        Debug.Log("and after we have "+queue.Count+" ennemies");
    }

    //Methods
    public void Begin(){
        StartCoroutine(WaveRun());
    }
    public void Reset(){
        //must implement
    }
    public int EnnemiesAmount(){ return ennemies_in; }

    //Coroutines
    private IEnumerator WaveRun(){
        for(int i=0; i<queue.Count; i++){
            float nxt_ennemy = Random.Range(1f, 2.5f);
            GameObject next;
            switch(queue[i]){
                case "g":
                    next = Instantiate(goblin, spawn.position, Quaternion.identity);
                    next.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
                    next.GetComponent<Ennemy>().SetEntity("goblin");
                    break;
                case "w":
                    next = Instantiate(wolf, spawn.position, Quaternion.identity);
                    next.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
                    next.GetComponent<Ennemy>().SetEntity("wolf");
                    break;
                default:
                    next = null;
                    break;
            }

            next.GetComponent<Ennemy>().SetWayPoints(way);
            next.GetComponent<Ennemy>().Travel();
            yield return new WaitForSeconds(nxt_ennemy);
        }
        queue.Clear();
        yield return new WaitForSeconds(5f);
    }
}