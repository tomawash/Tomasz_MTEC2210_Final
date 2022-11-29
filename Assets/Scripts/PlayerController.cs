using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask ground;

    public GameManager gm;

    public float speed = 200;
    float jumpSpeed = 7.5f;

    private Rigidbody2D rb;

    bool jumping;
    float xMove;

    public float distanceCheckAmount = 1.5f;

    public float halt = 3f;

    public bool charging = false;
    public float power = 0;
    float chargeRate = 0.05f;
    public float chargeLimit = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Are we on the ground? - " + GroundCheck());

        xMove = Input.GetAxisRaw("Horizontal");

        //Regular Jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck() && charging == false)
        {
            power = 0;
            jumping = true;
            charging = false;
        }

        //charging moved to fixed update

        //Charge Jump
        if (Input.GetKey(KeyCode.S) == false && charging == true)
        {
            jumping = true;
            charging = false;
            Debug.Log("Charge Release : " + power);

            transform.localScale = new Vector3(1, 1, 1);
        }

        //Flip sprite
        if (xMove != 0)
        {
            if(xMove > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }



    // Called at a consistent frame rate
    public void FixedUpdate()
    {
        //Charging
        if (Input.GetKey(KeyCode.S) && GroundCheck())
        {
            charging = true;
            power += chargeRate;

            if (power > chargeLimit)
            {
                power = chargeLimit;
            }

            Debug.Log("Power : " + power);

            transform.localScale = new Vector3(1, 1 - power/5, 1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }

        if (jumping == true)
        {
            rb.velocity = Vector2.up * (jumpSpeed + power);
            jumping = false;
            power = 0;
        }

        if(Mathf.Abs(rb.velocity.x) < halt)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }



    // Checks if grounded
    public bool GroundCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceCheckAmount, ground);
    }

    
}
