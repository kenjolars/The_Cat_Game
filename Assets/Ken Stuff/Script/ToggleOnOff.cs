using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnOff : MonoBehaviour
{
    //Variables 
    /*public GameObject toggle;
    public GameObject lighting;
    bool isOnToggle;
    bool isOnLighting;*/
    public GameObject dayItems;
    public GameObject nightItems;
    public Material dayMat;
    public Material nightMat;

    // Start is called before the first frame update
    void Start()
    {
        dayItems.SetActive(true);
        nightItems.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab is pressed");
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            isOnToggle = !isOnToggle;
            isOnLighting = !isOnLighting;
            toggle.SetActive(isOnToggle);
            lighting.SetActive(!isOnLighting);
            Debug.Log("E is pressed");
        }*/

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RenderSettings.skybox = dayMat;
            Debug.Log("1 is being pressed");
            dayItems.SetActive(true);
            nightItems.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RenderSettings.skybox = nightMat;
            Debug.Log("1 is being pressed");
            dayItems.SetActive(false);
            nightItems.SetActive(true);
        }
    }
}
