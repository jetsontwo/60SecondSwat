using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Properties : MonoBehaviour {

    [Header("Shooting Properties")]
    public int clip_size;
    public GameObject bullet_pooling_list;
    public GameObject bullet_shell;
    public float reload_time;
    public float damage;
    public float kickback_power;

    [Header("Smoke")]
    public ParticleSystem shoot_smoke;
}
