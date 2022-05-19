using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heheheha : MonoBehaviour
{
    public Vector2 jumpForce;

    public Rigidbody2D beeb;

    public float jumpTime;

    public int [] bales;

    [Header("GroundChecking")] //this header helps organize the inspector
    public LayerMask groundLayer; // set in inspector
    public float rayDistance; // set in inspector
    public bool canJump = false;

    [Header("DashChecking")]
    public bool canDash = false;

    // Start is called before the first frame update
    void Start()
    {
        jumpForce = new Vector2(0, 1);
        bales[5] = 2;
        StartCoroutine(waitForTime(5));
    }

    // Update is called once per frame
    void Update()
    {
        canJump = Physics2D.Raycast(new Vector2(transform.position.x - 0.38f, transform.position.y - rayDistance), Vector2.right, 0.88f, groundLayer);
        //Debug.Log(canJump);
        if (transform.position.y <= -5.4f)
        { 
            transform.position = new Vector3(0, 0, 0);
        }

        if (!canJump)
        {
            beeb.gravityScale = Mathf.Lerp(beeb.gravityScale, 5, Time.deltaTime * 1); //decrease multiplier to increase gravity slower
        }

        else if (canJump && beeb.gravityScale != 0)
        {
            beeb.gravityScale = 0;
        }


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
            Debug.Log("Key");
            //setting beeb.velocity.y = 0 gets an annoying error
            //unity doesnt allow changing of a specific variable that way, you need to do it as above
            // ... as far as I know

            beeb.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            applyDash();
        } 

        beeb.velocity = (new Vector2(Input.GetAxis("Horizontal") * 5, beeb.velocity.y));

    }

    public IEnumerator waitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("This time has passed: " + time);

    }




    public void applyDash()
    {
        beeb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 5, 0), ForceMode2D.Impulse);
        canDash = false;
    }



    IEnumerator dashCoolDown(float coolDownTime)
    {
        yield return new WaitForSeconds(coolDownTime);

    }




    private void OnDrawGizmos()
    {
        //I want the line to be red
        Gizmos.color = Color.red;
        //only scene is scene window, not game window
        Gizmos.DrawLine(new Vector2(transform.position.x - 0.38f, transform.position.y) + (Vector2.down) * rayDistance, new Vector2(transform.position.x + 0.51f, transform.position.y) + (Vector2.down) * rayDistance);
        //this draws a line in the scene window in unity
        //Gizmos.DrawLine(start, end)
        //I used (vector2)*transform.position as we cant add a vector3 with a vector2
        //using (vector2) before transform.position, we specify we only want the (x,y) of transform.position

    }

}