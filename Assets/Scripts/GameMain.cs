using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    Pooler pooler;
    private Rigidbody[] rigidbodies;
    private Rigidbody myRigitbody;
    private Animator myAnimator;
    private bool isDragging=false;
    private Vector3 startPos,startSlingPos,delta,shootForce,dragScreenPos,forceVector;
    public LineRenderer myRope;
    
    
    private Transform charHipObj;
    [SerializeField]
    private Transform aimPointObj,seat,charObj,leftSlingObj,rightSlingObj;

    [SerializeField]
    private GameObject cinemachineStateMode;


    // Start is called before the first frame update
    void Start()
    {
        pooler=Pooler.Instance;
        charHipObj=GameObject.FindWithTag("charHip").transform;
        startSlingPos=charHipObj.position+Vector3.down;
        myRope.SetPosition(0, leftSlingObj.position);
        myRope.SetPosition(1, charHipObj.position);
        myRope.SetPosition(2, rightSlingObj.position);
        myRigitbody=GetComponent<Rigidbody>();
        rigidbodies=GetComponentsInChildren<Rigidbody>();
        myAnimator=this.GetComponent<Animator>();

        
        
        myAnimator.SetInteger("state",0);
        RagdollState(false);

        for(int i=0;i<100;i++){
            pooler.SpawnFromPool("Bomb",new Vector3(Random.Range(-3f,3f),Random.Range(0f,10f),Random.Range(-10f,10f)),Quaternion.identity);
            pooler.SpawnFromPool("Coin",new Vector3(Random.Range(-3f,3f),Random.Range(0f,10f),Random.Range(-10f,10f)),Quaternion.identity);
        }
    }

    void RagdollState(bool state){
        foreach(Rigidbody item in rigidbodies)
                {
                        item.isKinematic=!state;
                        item.detectCollisions=state;
                    
                }
        myAnimator.enabled =!state;
        cinemachineStateMode.SetActive(!state);
        myRigitbody.isKinematic=state;
        myRigitbody.detectCollisions=!state;

    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("coin")){
            other.GetComponent<Coin>().collected();
            return;
        }
        
        myAnimator.SetInteger("state",3);
        RagdollState(true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isDragging=true;
            myAnimator.SetInteger("state",1);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Time.timeScale=0.2f;
            //SET MOUSE DELTA
            delta = Input.mousePosition-startPos;

            isDragging=false;
            myAnimator.SetInteger("state",2);
            myRigitbody.useGravity=true;
            //Fire!
            myRigitbody.AddForce(forceVector);
        }
        if(isDragging){
            delta = Input.mousePosition-startPos;
            if(delta.y>10)
            return;
            forceVector=new Vector3((-delta.x*3f),delta.y*(-3f),delta.y*(-3f));
            Vector3 posNew=startSlingPos-forceVector*0.001f;
            charObj.position=posNew;
            charObj.forward=forceVector.normalized;
            
            aimPointObj.position=startSlingPos+forceVector*0.001f;


            Vector3 charhipPos=charHipObj.position;
            myRope.SetPosition(1, charhipPos);
            seat.SetPositionAndRotation(charhipPos,charObj.rotation);
          
        }
        


        
    }
    private void fixedUpdate() {
        if(myRigitbody.velocity.magnitude>0){
            charObj.forward=myRigitbody.velocity.normalized;
        }   
    }

}
