using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbScript : MonoBehaviour
{
    private Rigidbody rb;
    public bool ladderClimb = false;

 void Start()
     {
      rb = GetComponent<Rigidbody>();
     }
 
  void Update()
     {
      if(ladderClimb){
 Vector3 directionRot = new Vector3(0, 0, 0);  //direction of rotation
 Vector3 movement = new Vector3(0, Input.GetAxisRaw("Vertical"), 0).normalized; //object movement
   rb.useGravity = false;}
    else rb.useGravity = true; 

    if (ladderClimb == true && Input.GetKeyDown("w"))
    {
      rb.transform.position += Vector3.up;
    }
 }
 
     void OnTriggerEnter(Collider collider){
      if(collider.gameObject.tag == "ladder"){
      if(Input.GetKeyUp(KeyCode.E)) ladderClimb = !ladderClimb;}}
 
    void OnTriggerExit(Collider collider){
    if(collider.gameObject.tag == "ladder") ladderClimb = false;}


}