using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set_Gold : MonoBehaviour {

    public Text gold;

    void Update()
    {
        gold.text = PlayerPrefs.GetInt("coins").ToString();
    }
}
