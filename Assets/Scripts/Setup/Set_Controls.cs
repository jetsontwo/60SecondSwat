using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Controls : MonoBehaviour {

    public GameObject Joystick_UI, Button_UI;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("Joystick") == 1)
        {
            Joystick_UI.SetActive(true);
        }
        else
            Button_UI.SetActive(true);
	}
}
