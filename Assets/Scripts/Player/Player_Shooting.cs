using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour {


    private GameObject[] bullet;
    private Rigidbody2D rb;
    public GameObject gun_holder;
    public ParticleSystem shoot_smoke;
    [Header("Bullets")]
    public GameObject bullet_list;
    private int cur_bullet_index;


    [Header("Reloading")]
    public int clip_size = 7;
    private int cur_clip_index = 0;
    public bool can_shoot = true, stunned = false;
    public float reload_time = 0;
    private AudioSource shoot_sound;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        shoot_sound = GetComponent<AudioSource>();

        bullet = new GameObject[40];

        int count = 0;
        foreach(Transform t in bullet_list.transform)
        {
            bullet[count++] = t.gameObject;
        }
	}

    //void Update()
    //{
    //    shoot(-1);
    //}

    public void shoot(int direction)
    {
        if (can_shoot && !stunned)
        {
            GameObject bul = bullet[cur_bullet_index++];
            //Shoots from edge of player
            bul.transform.position = transform.position + new Vector3(1.2f * direction, 0, 0);
            //Flips the bullet based on direction facing
            if (direction == 1)
            {
                bul.transform.rotation = Quaternion.identity;
                rb.MovePosition(new Vector2(transform.position.x - 0.1f, transform.position.y));
                shoot_smoke.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                bul.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                rb.MovePosition(new Vector2(transform.position.x + 0.1f, transform.position.y));
                shoot_smoke.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            bul.SetActive(true);
            StartCoroutine(Shake());
            shoot_smoke.gameObject.transform.position = transform.position + new Vector3(.9f * direction, 0, 0);
            shoot_smoke.Play();
            shoot_sound.pitch = Random.Range(.95f, 1.05f);
            shoot_sound.Play();
            cur_clip_index++;

            //Loops the bullet list index back around
            if (cur_bullet_index == 40)
                cur_bullet_index = 0;

            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        can_shoot = false;
        for(int i = 0; i < 10; ++i)
        {
            gun_holder.transform.rotation = Quaternion.Euler(new Vector3(0, gun_holder.transform.rotation.eulerAngles.y, i));
        }
        yield return new WaitForSeconds(reload_time/2f);
        for (int i = 10; i > 0; --i)
        {
            gun_holder.transform.rotation = Quaternion.Euler(new Vector3(0, gun_holder.transform.rotation.eulerAngles.y, i));
        }
        yield return new WaitForSeconds(reload_time / 2f);
        can_shoot = true;
    }

    IEnumerator Shake()
    {
        for(int i = 0; i < 3; ++i)
        {
            Camera.main.transform.position += new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
