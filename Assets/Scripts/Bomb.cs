using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    static private int ind=0;
    private int indx;
    public Bomb(){
        this.name_="bomb";
        isHit=true;
        indx=ind;
        ind++;
        
    }
    public override bool doStuff()
    {
        //base.doStuff();
        //letsContDown();
        bang();
        return isHit;
    }
    public void bang(){
        Debug.Log("BANG!");
        Collider[] colliders=Physics.OverlapSphere(transform.position,1f);
        
        
        foreach(Collider col in colliders){
            Debug.Log("collide"+col.gameObject.name);
            Rigidbody rig=col.GetComponent<Rigidbody>();
            if(rig)
            {
                rig.AddExplosionForce(50f,transform.position,5f,1f,ForceMode.Impulse);
            }
        }
        gameObject.SetActive(false);
        
    }


    protected override void Update()
    {
        transform.position=new Vector3(startPos.x+Mathf.Sin(Time.time+indx*0.04f)*2f,startPos.y,startPos.z);
    }
    
}
