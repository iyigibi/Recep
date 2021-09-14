using UnityEngine;

public class GameMain : MonoBehaviour
{
    Pooler pooler;
    private Rigidbody[] rigidbodies;
    private Rigidbody myRigitbody;
    private Animator myAnimator;
    private bool isDragging,isThrew=false;
    private Vector3 startPos,startSlingPos,delta,shootForce,dragScreenPos,forceVector;

    [SerializeField]
    private LineRenderer myRope,trajectionLine;
    
    
    private Transform charHipObj;
    [SerializeField]
    private Transform aimPointObj,seat,charObj,leftSlingObj,rightSlingObj;

    [SerializeField]
    private GameObject cinemachineStateMode,cinemachineRagdoll;


    void Start()
    {
        trajectionLine.positionCount=20;
        pooler=Pooler.Instance;
        charHipObj=GameObject.FindWithTag("charHip").transform;
        startSlingPos=charHipObj.position+Vector3.down;

        //SET ROPE
        myRope.SetPosition(0, leftSlingObj.position);
        myRope.SetPosition(1, charHipObj.position);
        myRope.SetPosition(2, rightSlingObj.position);
        myRigitbody=GetComponent<Rigidbody>();
        rigidbodies=GetComponentsInChildren<Rigidbody>();
        myAnimator=this.GetComponent<Animator>();

        
        //ANIMETE IDLE
        myAnimator.SetInteger("state",0);
        RagdollState(false);
        //SPAWN ITEMS
        for(int i=0;i<100;i++){
            pooler.SpawnFromPool("Coin",new Vector3(0f,5f+Mathf.Sin(i*0.04f)*5f,i*.2f-20),Quaternion.identity);
            pooler.SpawnFromPool("Bomb",new Vector3(Mathf.Sin(i*0.04f)*1f,5f+Mathf.Sin((i)*0.04f)*5f,i*.2f-20),Quaternion.identity);
        
        }
    }

//SET RAGDOOL STATE
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
//
    private void OnTriggerEnter(Collider other) {
        Item item=other.GetComponent<Item>();
        if( item ){
            //ITEM HIT
            if(item.isHit){
                RagdollState(true);
                Time.timeScale=0.5f;
                Invoke("setTimeScale", 1.0f);
            }
            //ITEM COLLECT
            item.doStuff();
        }else{
            //ITEM HIT OTHER STUFF
            RagdollState(true);
            //THIS STOPS SHAKE
            cinemachineRagdoll.SetActive(false);
        }
    }

    void setTimeScale()
    {
        //THIS STOPS SHAKE
        cinemachineRagdoll.SetActive(false);
        Time.timeScale=1f;
    }

    private void Update() {
        
        if(isThrew)
        return;

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isDragging=true;
            myAnimator.SetInteger("state",1);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isThrew=true;
            //Time.timeScale=0.2f;
            //SET MOUSE DELTA
            delta = Input.mousePosition-startPos;

            isDragging=false;
            myAnimator.SetInteger("state",2);
            myRigitbody.useGravity=true;
            //Fire!
            myRigitbody.velocity=forceVector;
        }
        if(isDragging){
            delta = Input.mousePosition-startPos;
            //CHECK FOR POWER
            if(delta.y>10)
            return;
            //FORCE VECTOR
            forceVector=new Vector3((-delta.x*0.05f),delta.y*(-0.05f),delta.y*(-0.05f));
            Vector3 posNew=startSlingPos-forceVector*0.015f;
            charObj.position=posNew;
            charObj.forward=forceVector.normalized;

            Vector3 charhipPos=charHipObj.position;
            aimPointObj.position=charhipPos+forceVector*0.3f;
            myRope.SetPosition(1, charhipPos);
            seat.SetPositionAndRotation(charhipPos,charObj.rotation);

            //TRAJECTION
            for(int i=0;i<trajectionLine.positionCount;i++){
                float deltaT=i*0.1f;
                trajectionLine.SetPosition(i,new Vector3(
                    
                    startSlingPos.x+forceVector.x*deltaT,
                    startSlingPos.y+0.2f+forceVector.y*deltaT-5f*Mathf.Pow(deltaT,2),
                    startSlingPos.z+forceVector.z*deltaT));
            }
        }
        
        

        
    }
    private void fixedUpdate() {
        if(myRigitbody.velocity.magnitude>0){
            //ROTATE MAN
            charObj.forward=myRigitbody.velocity.normalized;
        }   
    }

}
