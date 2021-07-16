using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pausemenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PausemenuUI;
    public GameObject ScorecanvasUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume(){
        PausemenuUI.SetActive(false);
        ScorecanvasUI.SetActive(true);
        Time.timeScale=1f;
        GameIsPaused=false;
    }

    void Pause(){
        PausemenuUI.SetActive(true);
        ScorecanvasUI.SetActive(false);
        Time.timeScale=0f;
        GameIsPaused=true;
    }

    public void LoadMenu(){
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    public void QuitGame(){
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
