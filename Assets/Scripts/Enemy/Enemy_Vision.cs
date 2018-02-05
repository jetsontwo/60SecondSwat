using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{

    private GameObject player = null;
    public LayerMask collidable_layers;
    private Enemy_Shoot es;
    public bool can_shoot = true;

    void Start()
    {
        es = transform.parent.GetComponent<Enemy_Shoot>();
    }

    public void wait_for_ground()
    {
        can_shoot = false;
        Invoke("can_shoot_now", 1f);
    }

    private void can_shoot_now()

    {
        can_shoot = true;
    }

    void Update()
    {
        if (player && can_shoot)
        {
            RaycastHit2D rh = Physics2D.Linecast(transform.position, player.transform.position, collidable_layers);

            es.shoot_direction = Vector3.Normalize(player.transform.position - transform.position);
            if (rh.collider)
            {
                if (rh.collider.CompareTag("Player"))
                {
                    es.can_shoot_player = true;
                }
                else
                    es.can_shoot_player = false;
            }
        }
        else
            es.can_shoot_player = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            player = other.gameObject;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            player = null;
    }
}
