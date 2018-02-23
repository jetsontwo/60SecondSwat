using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Guns")]
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject assualt_rifle;
    public GameObject machine_gun;

    public GameObject Player;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        PlayerPrefs.SetString("Player_Gun", "Assualt_Rifle");

        switch (PlayerPrefs.GetString("Player_Gun"))
        {
            case "Pistol":
                pistol.SetActive(true);
                break;
            case "Shotgun":
                shotgun.SetActive(true);
                break;
            case "Assualt_Rifle":
                assualt_rifle.SetActive(true);
                break;
            case "Machine_Gun":
                machine_gun.SetActive(true);
                break;
            default:
                pistol.SetActive(true);
                break;
        }
	}
	
}
