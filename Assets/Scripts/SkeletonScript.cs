using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    private float attackTimer = 0.0f;
    private Rigidbody2D rb2d;
    private Animator animator;
    public int skeleHealth;
    // Start is called before the first frame update
    void Start()
    {
        skeleHealth = 50;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (skeleHealth == 0) {
            Destroy(gameObject);
        }
    }

}
