using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptMainMenu : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;

    void Start() {
        clickSound = GetComponent<AudioSource>();
    }

    void PlaySound() {
        clickSound.Play();
    }

    public void Exit(){
        Application.Quit();
    }

    public void StartGame(){
        SceneManager.LoadScene("EpicSample");
    }
}

