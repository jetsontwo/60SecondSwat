using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Ownership : MonoBehaviour {

    [SerializeField]
    private string test_name;
    [SerializeField]
    private GameObject own_object, no_own_object;

	// Use this for initialization
	void OnEnable () {
		if(PlayerPrefs.GetInt(test_name) == 1)
        {
            own_object.SetActive(true);
            no_own_object.SetActive(false);
        }
        else
        {
            own_object.SetActive(false);
            no_own_object.SetActive(true);
        }

	}

    //public void setOwnership(bool canOwn)
    //{
    //    PlayerPrefs.SetInt(test_name, canOwn ? 1 : 0);
    //}

    public void Buy(int cost)
    {
        if(PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
            PlayerPrefs.SetInt(test_name, 1);
            own_object.SetActive(true);
            no_own_object.SetActive(false);
        }
    }

    public void Equip()
    {
        PlayerPrefs.SetString("Player_Gun", test_name);
    }
}
