using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    //Variables
    [SerializeField] string playerTag = "Player";
    [SerializeField] float disappearTime = 3;

    Animator myAnim;

    [SerializeField] bool canReset;
    [SerializeField] float resetTime;

    // Start is called before the first frame update
    private void Start()
    {
        myAnim = GetComponent<Animator>();
        myAnim.SetFloat("Disappear", 1/disappearTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hitting");
        if (collision.transform.tag == playerTag)
        {
            Debug.Log("Hitting");
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
