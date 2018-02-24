using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Controller : MonoBehaviour {

	public void SetGunString(string gun_name)
    {
        PlayerPrefs.SetString("Player_Gun", gun_name);
    }
}
