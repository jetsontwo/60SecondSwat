using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    public Player_Movement pm;
    public Player_Shooting ps;
    public LayerMask button_layer;
    public Sprite[] button_sprites;

    private int cur_direction = -1;


	void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                RaycastHit2D rh = Physics2D.Raycast(pos, Vector2.zero, 0f, button_layer);

                if(rh.collider)
                {
                    if (rh.collider.CompareTag("Right"))
                    {
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[1];
                        //This checks to make sure the player actually moved and wasn't stunned
                        if(pm.set_velocity_mod(1) == 1)
                            cur_direction = 1;
                        ps.dir = 1;
                    }
                    else if (rh.collider.CompareTag("Left"))
                    {
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[1];
                        //This checks to make sure the player actually moved and wasn't stunned
                        if (pm.set_velocity_mod(-1) == 1)
                            cur_direction = -1;
                        ps.dir = -1;
                    }
                    else if (rh.collider.CompareTag("Jump"))
                    {
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[1];
                        pm.attempt_jump();
                    }
                    else if (rh.collider.CompareTag("Shoot"))
                    {
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[1];
                        ps.shoot(cur_direction);
                    }
                }
            }
            else if(Input.GetTouch(i).phase == TouchPhase.Stationary || Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                RaycastHit2D rh = Physics2D.Raycast(pos, Vector2.zero, 0f, button_layer);

                if (rh.collider)
                {
                    if (rh.collider.CompareTag("Right"))
                    {
                        if(pm.set_velocity_mod(1) == 1)
                            cur_direction = 1;
                        ps.dir = 1;
                    }
                    else if (rh.collider.CompareTag("Left"))
                    {
                        if(pm.set_velocity_mod(-1) == 1)
                            cur_direction = -1;
                        ps.dir = -1;
                    }
                    else if (rh.collider.CompareTag("Shoot"))
                    {
                        ps.shoot(cur_direction);
                    }
                }
            }
            else if(Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                RaycastHit2D rh = Physics2D.Raycast(pos, Vector2.zero, 0f, button_layer);

                if (rh.collider)
                {
                    if (rh.collider.CompareTag("Right"))
                    {
                        pm.set_velocity_mod(0);
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[0];
                    }
                    else if (rh.collider.CompareTag("Left"))
                    {
                        pm.set_velocity_mod(0);
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[0];
                    }
                    else if (rh.collider.CompareTag("Jump"))
                    {
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[0];
                    }
                    else if (rh.collider.CompareTag("Shoot"))
                    {
                        rh.collider.GetComponent<SpriteRenderer>().sprite = button_sprites[0];
                    }
                }
            }
        }
    }
}
