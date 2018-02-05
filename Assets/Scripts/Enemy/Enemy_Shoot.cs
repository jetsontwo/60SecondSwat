using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour {

    public Vector3 shoot_direction;
    public GameObject bullet_prefab, gun;
    public float wait_time_between_shots, wait_time_between_bursts, offset;
    public int bullet_rotation;
    public int bursts;
    public bool can_shoot_player;
    private AudioSource shoot_sound;
	
	void Start()
    {
        shoot_sound = GetComponent<AudioSource>();
        StartCoroutine(shoot());
    }

    private IEnumerator shoot()
    {
        yield return new WaitForSeconds(offset);
        while(true)
        {
            if(can_shoot_player)
            {
                for (int i = 0; i < bursts; ++i)
                {
                    GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
                    //Flips the gun sprite if it pases halfway
                    if (shoot_direction.x < 0)
                        gun.GetComponentInChildren<SpriteRenderer>().flipY = true;
                    else
                        gun.GetComponentInChildren<SpriteRenderer>().flipY = false;


                    float angle = Mathf.Atan2(shoot_direction.y, shoot_direction.x) * Mathf.Rad2Deg;
                    gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                    shoot_sound.pitch = Random.Range(.9f, 1.1f);
                    shoot_sound.Play();
                    bullet.GetComponent<Enemy_Bullet_Movement>().direction = shoot_direction * 750;
                    bullet.SetActive(true);
                    yield return new WaitForSeconds(wait_time_between_shots);
                }
                yield return new WaitForSeconds(wait_time_between_bursts);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
