using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAction : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Rigidbody myRigitbody;
    private Animator myAnimator;
    public static GameObject animationAction;
    // Start is called before the first frame update
    void Start()
    {
        //animationAction=this.insta
        rigidbodies=GetComponentsInChildren<Rigidbody>();
        myAnimator=this.GetComponent<Animator>();
        RagdollState(false,true);

    }

    void RagdollState(bool state,bool isInit=false){
        foreach(Rigidbody item in rigidbodies)
                {
                    item.isKinematic=!state;
                   if(isInit){
                        item.gameObject.AddComponent<ChildCollision>().animationAction=this;
                   }   
                }
        myAnimator.enabled =!state;
        
    }
    public void onChildCollision(Collision other){
        Debug.Log(other.collider);
        RagdollState(true);
    }

    private void OnCollisionEnter(Collision other) {
        //Debug.Log();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
