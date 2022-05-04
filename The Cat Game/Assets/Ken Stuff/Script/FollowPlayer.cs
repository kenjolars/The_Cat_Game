using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    //Variables 
    public Transform targetPlayer;
    public Transform pet;
    public Transform other;
    NavMeshAgent nav;
    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (other)
        {
            dist = Vector3.Distance(other.transform.position, pet.transform.position);
            Debug.Log("Distance to other: " + other);

            if (dist <= 8)
            {
                nav.SetDestination(targetPlayer.position);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.tag == "Player")
        {
            Debug.Log("Pet will start following");
        }
    }
}
