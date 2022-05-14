using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    //Variables
    public GameObject unlockWall;
    public GameObject keyDisappear;
    public GameObject Player;

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
        if(other.gameObject == Player)
        {
            unlockWall.SetActive(false);
            keyDisappear.SetActive(false);
        }
    }
}
