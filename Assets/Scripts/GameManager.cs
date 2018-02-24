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

        PlayerPrefs.SetString("Player_Gun", "Assualt_Rifle");

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


        ps.clip_size = cur_gun.GetComponent<Gun_Properties>().clip_size;

        int bullet_list_size = cur_gun.GetComponent<Gun_Properties>().bullet_list_size;
        for (int i = 0; i < bullet_list_size; ++i)
        {
            Instantiate(rifle_bullets, bullet_pool.transform);
        }
        ps.bullet_list = bullet_pool;
        ps.shell = cur_gun.GetComponent<Gun_Properties>().bullet_shell;

        ps.reload_time = cur_gun.GetComponent<Gun_Properties>().reload_time;
        //ps. = cur_gun.GetComponent<Gun_Properties>().damage;

        ps.kickback_power = cur_gun.GetComponent<Gun_Properties>().kickback_power;
        ps.shoot_smoke = cur_gun.GetComponent<Gun_Properties>().shoot_smoke;
        ps.shoot_delay = cur_gun.GetComponent<Gun_Properties>().shoot_delay;
        ps.automatic = cur_gun.GetComponent<Gun_Properties>().automatic;
        ps.shell_spawn_location = cur_gun.GetComponent<Gun_Properties>().shell_spawn_location;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

}
