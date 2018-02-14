using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed, jump_strength;
    public LayerMask ground_layers;
    bool on_ground = false;
    [HideInInspector]
    public bool facing_right = true;
    public GameObject gun_holder;
    public bool can_move = true;
    private RaycastHit2D[] hit = new RaycastHit2D[3];


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        on_ground = touching_ground();

        //Lets the player use the keyboard to move in the Editor
#if UNITY_EDITOR
        if (can_move)
        {
            if (Input.GetKey(KeyCode.A))
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            else if (Input.GetKey(KeyCode.D))
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            else
                rb.velocity = new Vector2(0, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && on_ground)
                rb.AddForce(new Vector2(rb.velocity.x, jump_strength));
        }
#endif
    }

    bool touching_ground()
    {
        //Checks to see if the player is in contact with the ground
        hit[0] =  Physics2D.Raycast(transform.position, Vector2.down, 1f, ground_layers);
        hit[1] = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.down, 1f, ground_layers);
        hit[2] = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0, 0), Vector2.down, 1f, ground_layers);


        //Checks to see if it hit anything
        for(int i = 0; i < 3; ++i)
        {
            if (hit[i].collider)
                return true;
        }


        return false;
    }

    public int set_velocity_mod(float new_vel_mod)
    {
        if (can_move)
        {
            //Sets the new velocity using either 1 for forward, -1 for back, or 0 for not moving
            rb.velocity = new Vector2(speed * Time.deltaTime * new_vel_mod, rb.velocity.y);
            if (new_vel_mod == 1)
                gun_holder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else if (new_vel_mod == -1)
                gun_holder.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return 1;
        }
        return 0;
    }

    public void attempt_jump()
    {
        if (on_ground && rb.velocity.y == 0 && can_move)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.001f);
            rb.AddForce(new Vector2(0, jump_strength));
        }
    }
}
