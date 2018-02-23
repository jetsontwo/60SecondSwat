using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Controller : MonoBehaviour {

    public int health = 100;
    private bool pulsing = false;
    private BoxCollider2D bc;
    public Timer_Controller tc;
    public GameObject bomb_text;
    //Ranked in terms of largest stage first 
    public GameObject[] bomb_stages;

    public GameObject Mini_Explosion;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    public void take_damage()
    {
        if (--health <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            bomb_text.SetActive(false);
            tc.Win();
        }
        if (!pulsing)
            StartCoroutine(pulse());
        if(health % 25 == 0)
        {
            int new_stage = 3 - (health / 25);
            bomb_stages[new_stage].SetActive(false);
            GameObject explosion = Instantiate(Mini_Explosion, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(1 + (0.2f * new_stage), 1 + (0.2f * new_stage), 1 + (0.2f * new_stage));
            bc.size = new Vector2(bc.size.x / 1.5f, bc.size.y / 1.5f);
            //if(new_stage != 0)
                //bomb_stages[new_stage - 1].
        }
    }

    private IEnumerator pulse()
    {
        pulsing = true;
        transform.localScale -= new Vector3(.1f, .1f, 0);
        yield return new WaitForSeconds(0.05f);
        transform.localScale += new Vector3(.1f, .1f, 0);
        pulsing = false;
    }

    public IEnumerator expand_circle()
    {
        bomb_text.SetActive(false);
        CircleCollider2D c = GetComponent<CircleCollider2D>();
        for(int i = 0; i < 100; ++i)
        {
            c.radius += 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player") || c.CompareTag("Enemy") || c.CompareTag("Bullet"))
            c.gameObject.SetActive(false);
    }
}
