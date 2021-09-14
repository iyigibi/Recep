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
        Time.timeScale=0.5f;
        Debug.Log("BANG!");
        foreach(Collider col in Physics.OverlapSphere(transform.position,1f)){
            Rigidbody rig=col.GetComponent<Rigidbody>();
            if(rig)
            {
                rig.AddExplosionForce(20f,transform.position,5f,1f,ForceMode.Impulse);
            }
        }
        gameObject.SetActive(false);
        
    }


    protected override void Update()
    {
        //ANIMATE
        transform.position=new Vector3(startPos.x+Mathf.Sin(Time.time*0.3f+indx*0.04f)*2f,startPos.y+Mathf.Sin(Time.time+indx*0.04f)*2f,startPos.z);
    }
    
}
