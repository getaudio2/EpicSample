using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;

    public float health = 100f;
    public GameObject SkeletonObject;
    public GameObject CampfireObject;
    public GameObject AppleObject;
    public SkeletonScript skeletonScript;
    public float distanceHS;
    public float distanceHC;
    public float distanceHA;

    // Use this for initialization
    void Start ()
    {
        SkeletonObject = GameObject.FindWithTag("Skeleton");
        CampfireObject = GameObject.FindWithTag("Campfire");
        AppleObject = GameObject.FindWithTag("Mansanita");
        skeletonScript = SkeletonObject.GetComponent<SkeletonScript>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").gameObject.GetComponent<Sensor_HeroKnight>();
        distanceHS = Vector3.Distance (transform.position, SkeletonObject.transform.position);
        distanceHC = Vector3.Distance (transform.position, CampfireObject.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
        if (health == 0f) {
            SceneManager.LoadScene("GameOver");
        }
        if (SkeletonObject != null) {
            distanceHS = Vector3.Distance (transform.position, SkeletonObject.transform.position);
        }
        distanceHC = Vector3.Distance (transform.position, CampfireObject.transform.position);
        distanceHA = Vector3.Distance (transform.position, AppleObject.transform.position);

        m_grounded = Sensor_HeroKnight.isGrounded;
        m_animator.SetBool("Grounded", m_grounded);

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        /*if (!m_grounded) //&& Sensor_HeroKnight.isGrounded)
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded) //&& !Sensor_HeroKnight.isGrounded)
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }*/

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling )
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        //Attack
        if(Input.GetKeyDown("z") && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            if (distanceHS < 3f) { 
                skeletonScript.skeleHealth = skeletonScript.skeleHealth - 10;
            }
            
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetKeyDown("x") && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling) //&& !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
            

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    /*public void perderVida(int cantVida) {
        vidas = vidas - cantVida;

        if (vidas <= 0) {
            Debug.Log("muerto xd");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.gameObject.tag == "Skeleton") {
            Physics2D.IgnoreCollision(SkeletonObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } else if (collision.gameObject.tag == "Campfire") {
            Physics2D.IgnoreCollision(CampfireObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (distanceHS < 2f && SkeletonObject != null){
            health = health - 10f;
        } else if (distanceHC < 2f){
            health = health - 1f;
        } else if (distanceHA < 1f) {
            health = 100f;
        } else if (collision.gameObject.tag == "Void") {
            health = 0f;
        } else if (collision.gameObject.tag == "Chest") {
            SceneManager.LoadScene("Victory");
        }

    }

}
