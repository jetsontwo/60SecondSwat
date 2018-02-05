using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour {

    private bool can_take_damage = true;
    public Sprite stun1, stun2;
    public SpriteRenderer stun_sprite;

    public void take_damage()
    {
        if (can_take_damage)
        {
            StartCoroutine(stun());
        }
    }

    private IEnumerator stun()
    {
        can_take_damage = false;
        gameObject.GetComponent<Player_Movement>().can_move = false;
        Player_Shooting ps = GetComponent<Player_Shooting>();
        stun_sprite.GetComponent<AudioSource>().Play();
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
