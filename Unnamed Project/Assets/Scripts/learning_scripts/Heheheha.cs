using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heheheha : MonoBehaviour
{
    public Transform Location;

    public Vector2 Movement;

    public Rigidbody2D beeb;

    public float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        Movement = new Vector2(0, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            beeb.AddForce(Movement);
        }

        beeb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 2, Input.GetAxis("Vertical")) * 2);







    }
}