using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Definim les variables runSpeed i jumpSpeed
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    private SpriteRenderer sprite;
    public bool attacking = false;
    public float speed;
    public Vector3 lastPosition = Vector3.zero;
 
    Rigidbody2D rb2d;
    public float velocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var localVel = transform.InverseTransformDirection(rb2d.velocity);

        Vector2 pos = transform.position;

        if(Input.GetKeyDown(KeyCode.Space)){
            if (CheckGround.isGrounded) {
                rb2d.velocity = new Vector2(rb2d.velocity.y, jumpSpeed);
                GetComponent<Animator>().SetTrigger("Jump");
            }
        } 
        if (CheckGround.isGrounded) {
            GetComponent<Animator>().SetBool("Grounded", true);
        } else {
            GetComponent<Animator>().SetBool("Grounded", false);
        }

        GetComponent<Animator>().SetFloat("AirSpeedY", localVel.y);
        GetComponent<Animator>().SetInteger("AnimState", 0);
 
        if (Input.GetKey("right")){
            GetComponent<Animator>().SetInteger("AnimState", 1);
            pos.x += runSpeed * Time.deltaTime;
            sprite.flipX = false;
        }
        else if (Input.GetKey("left")){
            GetComponent<Animator>().SetInteger("AnimState", 1);
            pos.x -= runSpeed * Time.deltaTime;
            sprite.flipX = true;
        }
        transform.position = pos;

        if (Input.GetKey("z")) {
            if (attacking == false) {
                GetComponent<Animator>().SetTrigger("Attack1");
                attacking = true;
            }
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                attacking = true;
            } else {
                attacking = false;
            }
        }
    }

    void FixedUpdate() {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }
}
