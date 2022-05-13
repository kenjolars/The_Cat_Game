using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    //Variables
    public static bool GameIsPaused = false;

    public GameObject radialMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        radialMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))

        {
            if (GameIsPaused)
            {
                Resume();
                GetComponent<PauseMenu>().enabled = true;
                Debug.Log("Working");

            }
            else
            {
                Pause();
                GetComponent<PauseMenu>().enabled = false;
            }
        }
    }

    public void Resume()
    {
        radialMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        radialMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
