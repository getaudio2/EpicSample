﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Sensor_HeroKnight : MonoBehaviour {

    /*private int m_ColCount = 0;

    private float m_DisableTimer;

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_ColCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }*/
    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("TRUE");
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision){
        Debug.Log("FALSE");
        isGrounded = false;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
