using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInformation : MonoBehaviour
{
    //Variables
    public GameObject Player;
    public GameObject cat;
    public GameObject catShadow;
    public GameObject catInfo;

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
            catInfo.SetActive(true);
            cat.SetActive(false);
            catShadow.SetActive(false);
        }
    }
}
