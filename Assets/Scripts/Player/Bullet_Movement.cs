using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed;
    private float deactivate_timer = 0;
    public GameObject explosion;

    void OnEnable()
    {
        deactivate_timer = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
        deactivate_timer += Time.deltaTime;

        if (deactivate_timer > 2)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        //If hits enemy, then kill them
        if (c.CompareTag("Enemy"))
        {
            c.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Instantiate(explosion, transform.position, Quaternion.identity);
            //Explosion particle system here and sound effect
        }
        else if(c.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
            Instantiate(explosion, transform.position, Quaternion.identity);
            //Also explode here
        }
        else if(c.CompareTag("Bomb"))
        {
            c.GetComponent<Bomb_Controller>().take_damage();
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
