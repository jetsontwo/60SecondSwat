using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Properties : MonoBehaviour {

    [Header("Shooting Properties")]
    public int clip_size;
    public GameObject bullet_shell;
    public int bullet_list_size;
    [Range(0, 5)]
    public float reload_time;
    [Range(0, 1)]
    public float shoot_delay;
    public float damage;
    [Range(0, 2)]
    public float kickback_power;
    public bool automatic;

    public Transform shell_spawn_location;


    [Header("Smoke")]
    public ParticleSystem shoot_smoke;
}
