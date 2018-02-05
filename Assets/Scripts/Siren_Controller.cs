using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren_Controller : MonoBehaviour {

    public float offset;
    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr =GetComponent<SpriteRenderer>();
        StartCoroutine(shift_color());
	}

    IEnumerator shift_color()
    {
        yield return new WaitForSeconds(offset);
        while(true)
        {
            for (int i = 0; i < 30; ++i)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 0.01f);
                transform.position += new Vector3(0, 0.1f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 30; ++i)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.01f);
                transform.position -= new Vector3(0, 0.1f, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
