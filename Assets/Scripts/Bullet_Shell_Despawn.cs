using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shell_Despawn : MonoBehaviour {

    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Fade());
	}
	
    private IEnumerator Fade()
    {
        while(sr.color.a > 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
