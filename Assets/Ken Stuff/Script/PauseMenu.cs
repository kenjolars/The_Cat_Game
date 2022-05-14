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
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))

        {
            if (GameIsPaused)
            {
                Resume();
                GetComponent<RadialMenu>().enabled = true;
                /*Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;*/
            }
            else
            {
                Pause();
                GetComponent<RadialMenu>().enabled = false;
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
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
