using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour {

    private bool can_take_damage = true;
    public int max_health, cur_health;
    public Sprite stun1, stun2;
    public SpriteRenderer stun_sprite;
    private AudioSource hurt;

    void Start()
    {
        hurt = stun_sprite.GetComponent<AudioSource>();
        cur_health = max_health;
    }

    public void take_damage()
    {
        if (can_take_damage)
        {
            StartCoroutine(Flash(2));
            cur_health--;
            hurt.pitch = Random.Range(0.9f, 1.1f);
            hurt.Play();
            if(cur_health <= 0)
            {
                StartCoroutine(stun());
                cur_health = max_health;
            }
        }
    }

    private IEnumerator Flash(int num_flashes)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for(int i = 0; i < num_flashes; ++i)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .2f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator stun()
    {
        StopCoroutine("Flash");
        can_take_damage = false;
        gameObject.GetComponent<Player_Movement>().can_move = false;
        Player_Shooting ps = GetComponent<Player_Shooting>();
        hurt.Play();
        float count = 0;
        int sprite_swap = 0;
        stun_sprite.enabled = true;
        ps.stunned = true;
        while (count < 1)
        {
            if (++sprite_swap % 10 == 0)
                stun_sprite.sprite = stun_sprite.sprite == stun2 ? stun1 : stun2;
            count += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        stun_sprite.enabled = false;
        gameObject.GetComponent<Player_Movement>().can_move = true;
        ps.stunned = false;
        StartCoroutine(i_frames());
    }

    private IEnumerator i_frames()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .4f);
        yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));
        can_take_damage = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
