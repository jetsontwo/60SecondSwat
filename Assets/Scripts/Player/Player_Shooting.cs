using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour {

    //This variable will be between 0 and 1 and represents how much the gun smoke should be there after shooting (so it looks like it heated up)
    private float gun_heat = 0;
    private bool cooling = false;

    public int dir;

    private GameObject[] bullet;
    private Rigidbody2D rb;
    public GameObject gun_holder;
    public ParticleSystem shoot_smoke;
    private ParticleSystem passive_smoke;
    [Header("Bullets")]
    public GameObject bullet_list;                     
    private int cur_bullet_index;
    public GameObject shell;

    public float kickback_power;


    [Header("Reloading")]
    public int clip_size = 7;                           
    private int cur_clip_index = 0;
    public bool can_shoot = true, stunned = false;
    public float reload_time = 0;
    public float shoot_delay;               
    private AudioSource shoot_sound;

	// Use this for initialization
	void Start () {
        passive_smoke = shoot_smoke.transform.GetChild(0).GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        shoot_sound = GetComponent<AudioSource>();

        bullet = new GameObject[40];

        int count = 0;
        foreach(Transform t in bullet_list.transform)
        {
            bullet[count++] = t.gameObject;
        }
	}

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
            shoot(dir);
#endif
        //Makes the passive smoke follow the player's gun if they don't shoot since it doesn't update
        if(dir == 1)
            shoot_smoke.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else
            shoot_smoke.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        shoot_smoke.gameObject.transform.position = transform.position + new Vector3(1f * dir, 0, 0);
    }

    //Shoots in a direction 1 = right -1 (or really anything else) is left
    public void shoot(int direction)
    {
        if (can_shoot && !stunned)
        {
            dir = direction;
            GameObject bul = bullet[cur_bullet_index++];

            //Shoots from edge of player
            bul.transform.position = transform.position + new Vector3(1f * direction, 0, 0);
             

            //Flips the bullet based on direction facing
            if (direction == 1)
            {  
                bul.transform.rotation = Quaternion.identity;
                //Move back the player
                rb.MovePosition(new Vector2(transform.position.x - 0.05f, transform.position.y));
                shoot_smoke.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                bul.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                //Move back the player
                rb.MovePosition(new Vector2(transform.position.x + 0.05f, transform.position.y));
                shoot_smoke.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            bul.SetActive(true);

            //Bullet Shell
            GameObject s = Instantiate(shell, transform.position, Quaternion.identity);
            s.GetComponent<Rigidbody2D>().AddForce(new Vector2(-direction * 50, Random.Range(190, 210)));
            s.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5f, 5f));

            //Screen Shake
            StartCoroutine(Shake());

            //Recoil Gun
            StartCoroutine(Recoil());

            //Smoke Effects
            shoot_smoke.gameObject.transform.position = transform.position + new Vector3(1.1f * direction, 0, 0);
            shoot_smoke.Emit(10);
            shoot_smoke.gameObject.GetComponent<Animator>().Play("Shoot");

            //Smoke After Shooting Effect
            gun_heat = (gun_heat + 0.05f) < 1 ? gun_heat + 0.05f : 1;
            if (!cooling)
                StartCoroutine(Cool_Gun());

            //Shooting Sounds
            shoot_sound.pitch = Random.Range(.95f, 1.05f);
            shoot_sound.Play();


            //Clip Stuff
            if (cur_clip_index >= clip_size)
            {
                cur_clip_index = 0;
                StartCoroutine(Reload());
            }
            else
            {
                cur_clip_index++;
                StartCoroutine(Shoot_Delay_Wait());
            }


            //Loops the bullet list index back around
            if (cur_bullet_index == 40)
            {
                cur_bullet_index = 0;
            }
        }
    }

    IEnumerator Reload()
    {
        can_shoot = false;
        yield return new WaitForSeconds(reload_time);
        can_shoot = true;
    }

    IEnumerator Shoot_Delay_Wait()
    {
        can_shoot = false;
        yield return new WaitForSeconds(shoot_delay);
        can_shoot = true;
    }

    IEnumerator Recoil()
    {
        gun_holder.transform.Rotate(new Vector3(0, 0, 2));
        yield return new WaitForSeconds(0.018f);
        gun_holder.transform.Rotate(new Vector3(0, 0, 2));
        yield return new WaitForSeconds(0.018f);
        gun_holder.transform.Rotate(new Vector3(0, 0, 2));
        yield return new WaitForSeconds(0.018f);
        gun_holder.transform.Rotate(new Vector3(0, 0, -6));
    }

    IEnumerator Shake()
    {
        for(int i = 0; i < 3; ++i)
        {
            Camera.main.transform.position += new Vector3(Random.Range(-.1f, .1f) * kickback_power, Random.Range(-.1f, .1f) * kickback_power, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Cool_Gun()
    {
        cooling = true;
        passive_smoke.Play();
        var main = passive_smoke.main;
        while (gun_heat > 0)
        {
            main.startColor = new Color(1, 1, 1, gun_heat);
            yield return new WaitForSeconds(0.1f);
            gun_heat -= 0.02f;
        }
        passive_smoke.Stop();
        cooling = false;
    }
}
