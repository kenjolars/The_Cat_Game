using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Variables
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))

        {
            if (GameIsPaused)
            {
                Resume();
                //GetComponent<RadialMenu>().enabled = true;

            }
            else
            {
                Pause();
                //GetComponent<RadialMenu>().enabled = false;
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
