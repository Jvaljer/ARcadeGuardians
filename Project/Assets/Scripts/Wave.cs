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
                ennemies_in = 10+ (3*cpt);
                break;
            case "medium":
                ennemies_in = 12+ (4*cpt);
                break;
            case "hard":
                ennemies_in = 15+ (5*cpt);
                break;
            default:
                ennemies_in = -1;
                break;
        }

        //we wanna have at least half of ennemies being goblins
        int half = ennemies_in / 2;
        int g_cnt = half;
        int w_cnt = 0;
        
        // Adjust counts based on the desired behavior
        for (int i = 0; i < half; i++) {
            float r = Random.Range(0f, 100f);
            if(r<(25f+(cpt*2f))){
                w_cnt++;
            } else {
                if(diff=="medium" && (r<50f+(cpt*2.5f))){
                    w_cnt++;
                } else if(diff=="hard" && (r<75f+(cpt*2f))){
                    w_cnt++;
                } else {
                    g_cnt++;
                }
            }
        }

        queue = new List<string>();

        for(int i=0; i<ennemies_in; i++){
            float r = Random.Range(0f,10f);
            if(g_cnt<=0){
                queue.Add("g");
            } else if(w_cnt<=0){
                queue.Add("w");
            } else {
                if(r<=4f){
                    queue.Add("w");
                    w_cnt--;
                } else {
                    queue.Add("g");
                    g_cnt--;
                }
            }
        }
    }

    //Methods
    public void Begin(){
        StartCoroutine(WaveRun());
    }
    public void Reset(){
        //must implement
    }

    //Coroutines
    private IEnumerator WaveRun(){
        for(int i=0; i<queue.Count; i++){
            float nxt_ennemy = Random.Range(1f, 2.5f);
            GameObject next;
            switch(queue[i]){
                case "g":
                    next = Instantiate(goblin, spawn.position, Quaternion.identity);
                    break;
                case "w":
                    next = Instantiate(wolf, spawn.position, Quaternion.identity);
                    break;
                default:
                    next = null;
                    break;
            }

            next.GetComponent<Ennemy>().SetWayPoints(way);
            next.GetComponent<Ennemy>().Launch();
            yield return new WaitForSeconds(nxt_ennemy);
        }
        //HERE IS THE PROBLEM : 
        // ennemies aren't put INSIDE of the level, maybe should try actually freezing
        /*GameObject next;
        next = Instantiate(goblin, spawn.position, Quaternion.identity);
        next.GetComponent<Ennemy>().SetWayPoints(way);
        next.GetComponent<Ennemy>().Launch();
        game.EndWave();
        queue.Clear();
        yield return new WaitForSeconds(5f);*/
    }
}