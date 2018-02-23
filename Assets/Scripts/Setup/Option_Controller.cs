using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Controller : MonoBehaviour {

    public bool reset_all;

    [Header("Joystick Controls")]
    public GameObject Joystick_On;
    public GameObject Joystick_Off;

	void Start()
    {
        if (reset_all)
            Reset_All();

        //These set all the controls to visual reflect what the player had set
        Check_Control_Preference();

    }

    public void Reset_All()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Joystick_Controls(bool use)
    {
        PlayerPrefs.SetInt("Joystick", use ? 1 : 0);
    }

    private void Check_Control_Preference()
    {
        if (PlayerPrefs.HasKey("Joystick"))
        {
            if (PlayerPrefs.GetInt("Joystick") == 1)
            {
                Joystick_Off.SetActive(true);
                Joystick_On.SetActive(false);
            }
        }
    }
}
