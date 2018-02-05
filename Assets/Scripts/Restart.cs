using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour {

    public GameObject button, load;

	public void restart_click(bool loading)
    {
        if(loading)
        {
            button.SetActive(false);
            load.SetActive(true);
        }
        SceneManager.LoadScene(1);
    }
    
    public void quit()
    {
        Application.Quit();
    }
}
