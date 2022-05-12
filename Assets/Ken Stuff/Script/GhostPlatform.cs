using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    //Variables
    public GameObject Player;
    [SerializeField] float disappearTime = 3f;

    Animator myAnim;

    [SerializeField] bool canReset;
    [SerializeField] float resetTime;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myAnim.SetFloat("Disappear", 1 / disappearTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            myAnim.SetBool("Trigger", true);
        }
    }

    public void TriggerReset()
    {
        if(canReset)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        myAnim.SetBool("Trigger", false);
    }
}
