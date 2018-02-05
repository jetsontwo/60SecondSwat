using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawning : MonoBehaviour {

    private float level_mod, timer;
    private GameObject[] enemies;
    public Transform player_pos;
    public float enemy_spawn_time;

    // Use this for initialization
    void Start () {
        int count = 0;
        enemies = new GameObject[30];
        foreach(Transform t in transform)
            enemies[count++] = t.gameObject;

        StartCoroutine(Random_Spawning());

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > enemy_spawn_time)
        {
            level_mod += 0.01f;
            timer = 0;
        }   
    }

    IEnumerator Random_Spawning()
    {
        int enemy_index = 0;
        while(true)
        {
            yield return new WaitForSeconds(Mathf.Max(Random.Range(0.5f, 3f) - level_mod, 0.5f));
            GameObject enemy = enemies[enemy_index++];
            enemy.SetActive(true);
            int randx = Random.Range(0, 2);
            if (randx == 0)
            {
                enemy.transform.position = new Vector2(player_pos.position.x - 12, Random.Range(-1.5f, 3f));
                enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                enemy.transform.position = new Vector2(player_pos.position.x + 12, Random.Range(-1.5f, 3f));
                enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }

            if (enemy_index == 30)
                enemy_index = 0;
        }
    }
}
