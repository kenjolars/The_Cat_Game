using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnOff : MonoBehaviour
{
    //Variables 
    public GameObject toggle;
    public GameObject lighting;
    bool isOnToggle;
    bool isOnLighting;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab is pressed");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isOnToggle = !isOnToggle;
            isOnLighting = !isOnLighting;
            toggle.SetActive(isOnToggle);
            lighting.SetActive(!isOnLighting);
            Debug.Log("E is pressed");
        }
    }
}