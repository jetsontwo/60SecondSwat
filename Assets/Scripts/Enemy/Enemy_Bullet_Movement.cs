using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Movement : MonoBehaviour {

    private Rigidbody2D rb;
    public Vector3 direction;
    public GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * Time.deltaTime;
    }

    void Update()
    {
        rb.velocity = direction * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        //If hits enemy, then kill them
        if (c.CompareTag("Player"))
        {
            c.GetComponent<Player_Health>().take_damage();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

            //Explosion particle system here and sound effect
        }
        else if (c.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            //Also explode here
        }
    }
}
