using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleScript : MonoBehaviour
{
    // NOTE: Hiearchy shows active gameobjects in scene
    // NOTE2: inspector shows components on a specific gameobject
    // NOTE3: pay attention to brackets and semicollons, you will get annoyed by this
    // Note4: have fun, fuck with some shit and try things out
    // the worst you can do is cause an infinite loop and crash unity

    // public means it can be seen in the inspector and can be used in other scripts/class
    // private means it can't be seen in the inspector and can only be used in this class/script

    // types of variables that will often be used
    public Transform objTransform; // this holds a position, rotation, and scale of an object
    public GameObject anObject; // this holds an objects info (all components in inspector: script data, name, transform, etc)
    public Vector2 Pos1; // this holds an (x,y) position   
    public Vector3 pos2; // this holds an (x,y,z) position, for the most part we use vector2 tho (mostly a 3d thing)
    // try set the value of pos2 in the inspector
    public Rigidbody2D rb; // this component is a built in component to add mass, gravity, drag, and velocity to an object
    public AudioSource audioSource; // adds an audiosource to your scene
    public int anInt; //an int, the usual
    public float aFloat; // a float, it got decimals

    // Start is called before the first frame update /// good for assigning data to variables
    void Start()
    {
        // WARNING! DO NOT UNCOMMENT THE LINE BELOW AS IT WILL INSTANTIATE LOOP FOREVER!!!!!!
        // anObject = this.gameObject; //this = this script/class the .gameobect extention says: "grab the gameobject this script is attached to"
        // you will see now that you dont need to set onObject in the inspector window

        // how assigning a new vector2 value works
        Pos1 = new Vector2(1, 2); //we are giving the vector2 a value of a new vector2.
                                  // notice how getting rid of new causes an error. You need it, without it, it thinks you are trying to make a new vector2 variable
                                  // this is saying a new vector2 value, not variable



        // Instantiate(anObject, position (vector2 or 3), rotation)
        Instantiate(anObject, Pos1, Quaternion.identity); // anObject is set to "A_Sqaure" (can be seen/changed in the inspector)
        // Instantiate = create
        // Quaternion.identity = the gameobjects rotation that this script is attached to
        // (literally all you need to know about quaternions for now, they are confusing)

        for (anInt = 3; anInt < 10; anInt++)
        {
            // for(start value (setting anInt = 3 before loop starts), loop condition, incrementation){ contents }

            Debug.Log("anInt value is: " + anInt); //we use debug.log to print stuff to the console. 
            // we also have a print() function, but i've literally never used it
        }
        // once anInt = 10 the loop ends
    }

    // Update is called once per frame
    void Update()
    {
        if (anInt == 10) // this is how an if/else if sequence works
        {
            // anInt = 10 right after the for loop in start
            anInt = returnsInt();
            Debug.Log("After The For Loop, anInt in update is: " + anInt);
        } else if (anInt == 250)
        {
            // anInt = 250 after returnInt() returns a value to it
            anInt += 10;
            Debug.Log("After The For returnsInt Function, anInt in elseIf is: " + anInt);
        }
        else
        {
            // this will run every frame after the final else if so, I dont want to print anything here
            // you typically dont want something running each frame, you ussually want a condition
        }
    }



    // FixedUpdate executes 50 times per second. (The game code runs around 200 fps on a test machine.) This is slower than Update
    // why would we use this? well because rb uses physics and all physics should be done in fixed update
    // FixedUpdate() is called immediately before the physics update at a set time interval.
    // Since it is immediately before the physics update, any physics changes you apply occur immediately
    // and by a fairly predictable amount since the time between calls is as close to fixed as Unity gets
    private void FixedUpdate()
    {
        physicsLogic(anInt); // this is how you call a function with a parameter 
        // this function does nothing atm. Just runs through emptiness and ends the function
    }

    void physicsLogic(int exampleParameter)
    {
        // function made by me (void = the return type) void = nothing so we dont need to return anything
        // if public or private isn't specified it defaults to private. same with variables
    }

    public int returnsInt()
    {
        // this HAS to return an int as specified. not returning anything = an error
        //
        int localInt = 25; // this is a local variable that can only be used in this function
        localInt = (anInt * localInt); // you can use globals inside a function at anytime
        return localInt;
    }
    

  


} // this ends the class, nothing can be used after this that is wanted to be contained in this class
