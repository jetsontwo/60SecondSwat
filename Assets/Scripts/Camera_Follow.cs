using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    public GameObject to_follow;
    private Rigidbody2D player_rb;
    private float accel = 0;
    //private bool is_going_right = false;

    // Use this for initialization
    void Start () {
        player_rb = to_follow.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (player_rb.velocity.x > 0 && accel < 100)
            accel += 15;
        else if (player_rb.velocity.x < 0 && accel > -100)
            accel -= 15;
        else if(player_rb.velocity.x == 0 && accel != 0)
            accel += accel > 0 ? -15 : 15;

        transform.position = new Vector3(to_follow.transform.position.x + (accel / 100f), to_follow.transform.position.y, -10);


        //if (player_rb.velocity.x > 0)
        //{
        //    if (!is_going_right)
        //        accel = accel - 50 < 0 ? 0 : accel - 50;
        //    is_going_right = true;
        //}
        //else if (player_rb.velocity.x < 0)
        //{
        //    if (is_going_right)
        //        accel = accel - 50 < 0 ? 0 : accel - 50;
        //    is_going_right = false;
        //}

        //if (player_rb.velocity.x == 0 && accel > 0)
        //    accel -= 5;
        //else if (accel < 100 && player_rb.velocity.x != 0)
        //    accel += 5;


        //transform.position = new Vector3(Mathf.Lerp(to_follow.transform.position.x, to_follow.transform.position.x + (is_going_right ? 3 : -3), accel / 100f), to_follow.transform.position.y, -10); 
    }
}
