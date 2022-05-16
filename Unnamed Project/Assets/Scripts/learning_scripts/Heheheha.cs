using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heheheha : MonoBehaviour
{
    public Transform Location;

    public Vector2 jumpForce;

    public Rigidbody2D beeb;

    public float jumpTime;


    [Header("GroundChecking")] //this header helps organize the inspector
    public LayerMask groundLayer; // set in inspector
    public float rayDistance; // set in inspector
    public bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        jumpForce = new Vector2(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        canJump = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        //this creates a ray that detects if it touches a layer that groundLayer is assigned to in the inspector
        //this will probably be something like "ground", I can show you how to do this when you see it tomorrow
        //Physics2D.Raycast(start_of_ray, direction_of_ray, distance_of_ray, layerToCheck)
        //if the ray hits that layer, can Jump = true
        //ondrawgizmos() function below, helps us see this in unity before run time
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            //we want to stop all y-forces before a jump to get consistant jumping
            beeb.velocity = new Vector2(beeb.velocity.x, 0);
            //setting beeb.velocity.y = 0 gets an annoying error
            //unity doesnt allow changing of a specific variable that way, you need to do it as above
            // ... as far as I know

            beeb.AddForce(jumpForce * 5f);
        }

        beeb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 2, Input.GetAxis("Vertical")) * 2);
    }


    private void OnDrawGizmos()
    {
        //I want the line to be red
        Gizmos.color = Color.red;
        //only scene is scene window, not game window
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + (Vector2.down * rayDistance));
        //this draws a line in the scene window in unity
        //Gizmos.DrawLine(start, end)
        //I used (vector2)transform.position as we cant add a vector3 with a vector2
        //using (vector2) before transform.position, we specify we only want the (x,y) of transform.position
    }

}