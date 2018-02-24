using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Guns")]
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject assualt_rifle;
    public GameObject machine_gun;
    private GameObject cur_gun;

    [Header("Bullets")]
    public GameObject bullet_pool;
    public GameObject rifle_bullets;

    [Header("Player")]
    public Player_Shooting ps;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        
        ///////////////////////////////////////////Setting up the player's gun////////////////////////////////////////////////////

        switch (PlayerPrefs.GetString("Player_Gun"))
        {
            case "Pistol":
                cur_gun = Instantiate(pistol, ps.gun_holder.transform.position + new Vector3(-.4f, -.183f, 0), Quaternion.Euler(0, 180, 0), ps.gun_holder.transform);
                break;
            case "Shotgun":
                cur_gun = Instantiate(shotgun, ps.gun_holder.transform.position + new Vector3(-.4f, -.183f, 0), Quaternion.Euler(0, 180, 0), ps.gun_holder.transform);
                break;
            case "Assualt_Rifle":
                cur_gun = Instantiate(assualt_rifle, ps.gun_holder.transform.position + new Vector3(-.4f, -.183f, 0), Quaternion.Euler(0, 180, 0), ps.gun_holder.transform);
                break;
            case "Machine_Gun":
                cur_gun = Instantiate(machine_gun, ps.gun_holder.transform.position + new Vector3(-.4f, -.183f, 0), Quaternion.Euler(0, 180, 0), ps.gun_holder.transform);
                break;
            default:
                pistol.SetActive(true);
                break;
        }

        Gun_Properties gp = cur_gun.GetComponent<Gun_Properties>();

        ps.clip_size = gp.clip_size;

        int bullet_list_size = gp.bullet_list_size;
        for (int i = 0; i < bullet_list_size; ++i)
        {
            Instantiate(rifle_bullets, bullet_pool.transform);
        }
        ps.bullet_list = bullet_pool;
        ps.shell = gp.bullet_shell;
        ps.shot_count = gp.shot_count;

        ps.reload_time = gp.reload_time;
        //ps. = gp.damage;

        ps.kickback_power = gp.kickback_power;
        ps.shoot_smoke = gp.shoot_smoke;
        ps.shoot_delay = gp.shoot_delay;
        ps.automatic = gp.automatic;
        ps.shell_spawn_location = gp.shell_spawn_location;
        ps.spread = gp.spread;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

}
