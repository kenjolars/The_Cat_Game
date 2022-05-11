using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    //Variables
    //[SerializeField] string playerTag = "Player";
    public GameObject Player;
    [SerializeField] float disappearTime = 3;

    Animator myAnim;

    Vector3 velocity;

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitting");
        if (other.tag == "Player")
        {
            Debug.Log("Hitting");
            myAnim.SetBool("Trigger", true);
            velocity.y = -2f;
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
