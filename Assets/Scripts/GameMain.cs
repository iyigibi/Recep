using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Rigidbody myRigitbody;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
        myRigitbody=GetComponent<Rigidbody>();
        rigidbodies=GetComponentsInChildren<Rigidbody>();
        myAnimator=this.GetComponent<Animator>();
        
        myAnimator.SetInteger("state",0);
        RagdollState(false);
    }

    void RagdollState(bool state){
        foreach(Rigidbody item in rigidbodies)
                {
                        item.isKinematic=!state;
                        item.detectCollisions=state;
                    
                }
        myAnimator.enabled =!state;
        myRigitbody.isKinematic=state;
        myRigitbody.detectCollisions=!state;

    }

    private void OnTriggerEnter(Collider other) {
        RagdollState(true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            myAnimator.SetInteger("state",1);
        }
        if (Input.GetMouseButtonUp(0))
        {
            myAnimator.SetInteger("state",2);
        myRigitbody.useGravity=true;
        myRigitbody.AddForce(new Vector3(0,500,500));
        }
    }

}
