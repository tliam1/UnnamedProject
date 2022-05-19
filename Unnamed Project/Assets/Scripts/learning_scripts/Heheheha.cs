using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heheheha : MonoBehaviour
{

    public Rigidbody2D beeb;

    [Header ("random Tests")]
    public int[] bales;

    [Header ("Jumping")]
    public Vector2 jumpForce;
    public float jumpTime;

    [Header("GroundChecking")] //this header helps organize the inspector
    public LayerMask groundLayer; // set in inspector
    public float rayDistance; // set in inspector
    public bool canJump = false;

    [Header("DashChecking")]
    public bool canDash;
    public bool isDashing;
    Timer dashCoolDownTimer;
    public float dashCoolDownTime; //inspector

    [Header("Movement Mods")]
    public float horizontalMovementModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        jumpForce = new Vector2(0, 1);
        bales[5] = 2;
        canDash = true;
        isDashing = false;

        dashCoolDownTimer = gameObject.AddComponent<Timer>(); //creates and adds timer component at run time
        dashCoolDownTimer.Duration = dashCoolDownTime; //sets duration
        dashCoolDownTimer.AddTimerFinishedListener(() => //sets end condition of the timer (Triggers a unity event, with isDashing being the even contents)
        {
            isDashing = false;
            if (!canJump)
                beeb.gravityScale = 5f;
        });
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

        if (!canJump && !isDashing)
        {
            beeb.gravityScale = Mathf.Lerp(beeb.gravityScale, 5, Time.deltaTime * 1); //decrease multiplier to increase gravity slower
        }else if ((canJump && beeb.gravityScale != 0) || isDashing)
        {
            beeb.gravityScale = 0;
        }

        if (canJump && !canDash && !isDashing)
            canDash = true;
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

            beeb.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash && !isDashing)
        {
            applyDash();
        }

        if (!isDashing)
        {
            beeb.velocity = (new Vector2(Input.GetAxis("Horizontal") * 7 * horizontalMovementModifier, beeb.velocity.y));
            horizontalMovementModifier = Mathf.Lerp(horizontalMovementModifier, 1, Time.deltaTime);
        }
        else
        {
            horizontalMovementModifier = 1.5f;
        }


    }

    public void applyDash()
    {
        Debug.Log("RAN");
        beeb.velocity = new Vector2(0, 0);
        beeb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * 2, Input.GetAxisRaw("Vertical") * 2), ForceMode2D.Impulse); //Raw just means movement is just 0 or 1, no value inbetween
        canDash = false;
        isDashing = true;
        dashCoolDownTimer.Run();
        //to check if it is running at anytime do:
        // dashCoolDownTimer.Running (NOT USED LIKE A FUNCTION RUNNING IS A BOOL THAT IS TRUE WHEN TIMER IS RUNNING OR FALSE WHEN NOT RUNNING)
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