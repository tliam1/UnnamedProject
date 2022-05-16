using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidBody_Things : MonoBehaviour
{
    // recall: rigidbody = gravity things, Hiearchy: shows objects active in scene
    //         Inspector: object information, Project Files: houses scripts, prfabs, etc

    // Fact of the day: compiling the script while running the game in unity will often break the
        // actively running game, just stop it in unity and play it again if this happens

    // today we will learn about rigidbody and some input methods
    // previously we got the rigidbody on the object through 
    // drag and drop in the inspector. However, we dont really need to do this
    // we can do this in the start function (see below)

    public Rigidbody2D rb; // creates an empty rigidbody in the inspector
    // do not drag anything into it in the inspector, we will use code toda+y

    public float constantGravityValue; // THIS WILL BE SET IN THE INSPECTOR
                                       // (Do this last) Look in the increasegravity function to see where this is used 

    // Start is called before the first frame update
    void Start()
    {
        //assigned the rigidbody through code alone
        rb = this.GetComponent<Rigidbody2D>();
        // this = the gameobject the script is attached to(it will tell you that this is redunant, which it is as "this" is recognized by default)
        // this gameobject --> get a component from it --> <specifier>();
        // we need the () as this is like a function call to get the specified component
        // Now we dont need to set anything in the inspector and we "could" make it private if we wanted to 


        // lets start using this component (rigidbody)
        rb.gravityScale = 0;
        // this sets the gravity to 0 at the start!
        // how things are formatted --> rb.innerworkings
        // the period is like searching what's within a component
        // if you do rb. and wait and see what auto pops up you can see some of these extensions
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Input = input
            // GetKey() = gets a key that is being held down, returns true so long as it is held down
            // getkey(keycode.space) = keycode parameter specifies what key it is looking for. Keycode.space is a parameter for the GetKey() function
            increaseGravity(); // calls the VOID function down below

            // other types of input functions --> Input.getkeydown() true the one frame it is pressed down
            // .getkeyup() = true the frame the key is released
        }
        else if (rb.gravityScale != 0) 
        {
            // if the gravityscale is anything but 0 and we are not holding down space, reset it to 0
            rb.gravityScale = 0;

            // play the scene before you read the rest of the comments in the function!
            // Notice how you continue to move even after we release space and set gravity to 0
            // this is because we still have some velocity from the applied gravity
            // to avoid this we can up the drag value and slow down overtime 
            // or just reset our velocity to 0
            // Uncomment the two rb modifiers below:

            //notice how velocity is a vector2 as you have an x and y velocity 
            //rb.velocity = new Vector2(0, 0);  // comment this line out when trying drag (down below)
            // note: short hand --> rb.velocity = Vector2.zero;
            // but how? shouldn't shorthand be new Vector2.zero?
            // NO! the compiler reads this as Vector2 Vector2.zero (note vector2.zero is short hand for vector2(0,0))
            // Vector2 Vector2.zero is like creating a new vector2 (like vector2 a = new vector2(0,0)), just without a variable name
            // feel free to try and replace it with short hand


            // next is drag!
            // UNCOMMENT THIS LINE-->
            rb.drag = 0.5f;
            // drag works like normal drag and will slow your rb.velocity vector down over time
            // if you have anymore questions make sure you talk to me
        }
    }


        void increaseGravity()
    {
        // here we are setting some actual gravity
        // notice how we add an f after 0.1, this specifies that this is a float
        // (you get an error if not specified)
        // feel free to add more stuff here and mess around with gravity
        //rb.gravityScale = 0.3f;   

        // or use can use a float variable that can be changed in the inspector
        // this float varable can be used to test different gravity values at runtime/playtime
        rb.gravityScale = constantGravityValue;
        // change constant gravity value in the inspector! not in code!!!
        // Note: negative gravity works here, have fun testing!
    }
}// this semicolon ends the class! WE ARE DONE! 
