using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptMainMenu : MonoBehaviour
{
    public void Exit(){
        Application.Quit();
    }

    public void StartGame(){
        SceneManager.LoadScene("EpicSample");
        Debug.Log("hola xd");
    }
}

