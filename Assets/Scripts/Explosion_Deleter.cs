using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Deleter : MonoBehaviour {

    private AudioSource explosion_sound;

	// Use this for initialization
	void Start () {
        explosion_sound = GetComponent<AudioSource>();
        explosion_sound.pitch = Random.Range(0.9f, 1.1f);
        explosion_sound.Play();
        Invoke("Delete", .5f);
	}
	
    void Delete()
    {
        Destroy(gameObject);
    }
}
