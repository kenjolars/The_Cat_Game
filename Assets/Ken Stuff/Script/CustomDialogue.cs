using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDialogue : MonoBehaviour
{
    //Varibles
    public GameObject Player;
    public GameObject sign;
    public GameObject otherText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            sign.SetActive(true);
            otherText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            sign.SetActive(false);
            otherText.SetActive(true);
        }
    }
}
