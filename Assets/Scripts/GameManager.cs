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
                pistol.SetActive(true);
                cur_gun = pistol;
                break;
            case "Shotgun":
                shotgun.SetActive(true);
                cur_gun = shotgun;
                break;
            case "Assualt_Rifle":
                assualt_rifle.SetActive(true);
                cur_gun = assualt_rifle;
                break;
            case "Machine_Gun":
                machine_gun.SetActive(true);
                cur_gun = machine_gun;
                break;
            default:
                pistol.SetActive(true);
                break;
        }

        ps.clip_size = cur_gun.GetComponent<Gun_Properties>().clip_size;
        ps.bullet_list = cur_gun.GetComponent<Gun_Properties>().bullet_pooling_list;
        ps.shell = cur_gun.GetComponent<Gun_Properties>().bullet_shell;
        ps.reload_time = cur_gun.GetComponent<Gun_Properties>().reload_time;
        //ps. = cur_gun.GetComponent<Gun_Properties>().damage;
        ps.kickback_power = cur_gun.GetComponent<Gun_Properties>().kickback_power;
        ps.shoot_smoke = cur_gun.GetComponent<Gun_Properties>().shoot_smoke;
        ps.shoot_delay = cur_gun.GetComponent<Gun_Properties>().shoot_delay;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

}
