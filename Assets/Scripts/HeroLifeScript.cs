using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLifeScript : MonoBehaviour
{
    public int vidas;
    public int maxVidas;
    public GameObject jugador;
    private HeroLifeScript puntosVida;

    // Start is called before the first frame update
    void Start()
    {
        vidas = 2;
        maxVidas = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void perderVida(int cantVida) {
        vidas = vidas - cantVida;

        if (vidas <= 0) {
            Debug.Log("muerto xd");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        jugador = GameObject.FindWithTag("Player");
        puntosVida = jugador.GetComponent<HeroLifeScript>();
        puntosVida.perderVida(2);
    }
}
