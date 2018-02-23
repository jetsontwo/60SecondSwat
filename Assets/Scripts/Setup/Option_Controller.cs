using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Controller : MonoBehaviour {

    public bool reset_all;

	void Start()
    {
        if (reset_all)
            Reset_All();
    }

    public void Reset_All()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Joystick_Controls(bool use)
    {
        PlayerPrefs.SetInt("Joystick", use ? 1 : 0);
    }

}
