using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Controller : MonoBehaviour {

    public int health = 100;
    private bool pulsing = false;
    public Timer_Controller tc;
    public GameObject bomb_text;

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
    }

    private IEnumerator pulse()
    {
        pulsing = true;
        transform.localScale -= new Vector3(.15f, .15f, 0);
        yield return new WaitForSeconds(0.05f);
        transform.localScale += new Vector3(.15f, .15f, 0);
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
