using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    //public int scene;

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}

