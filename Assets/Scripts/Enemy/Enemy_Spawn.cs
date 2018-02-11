using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{

    private bool is_enabled = false;
    public GameObject enemy;
    private GameObject player;
    public float min, max;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn_Enemies());
    }

    private IEnumerator Spawn_Enemies()
    {
        while (true)
        {
            if(is_enabled)
            {
                transform.position = new Vector3(Random.Range(player.transform.position.x - 5f, player.transform.position.x + 5f), transform.position.y, transform.position.z);
                if (transform.position.x < min)
                    transform.position = new Vector3(min + Random.Range(0, 5f), transform.position.y, transform.position.z);
                else if (transform.position.x > max)
                    transform.position = new Vector3(max - Random.Range(0, 5f), transform.position.y, transform.position.z);
                yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
                GameObject e = Instantiate(enemy, transform.position, Quaternion.identity);
                e.GetComponentInChildren<Enemy_Vision>().wait_for_ground();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            is_enabled = true;
            player = c.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            is_enabled = false;
            player = null;
        }
    }
}
