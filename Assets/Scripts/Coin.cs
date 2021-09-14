using UnityEngine;

public class Coin : Item
{
    static private int ind=0;
    private int indx;
    public Coin(){
        this.name_="coin";
        isHit=false;
        indx=ind;
        ind++;
    }
    public override bool doStuff()
    {
        //base.doStuff();
        collected();
        return isHit;
    }
    private void collected(){
        Debug.Log("Coin collected!");
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        //ANIMATE
        transform.position=new Vector3(startPos.x,startPos.y+2f+Mathf.Sin(Time.time+indx*0.02f)*4f,startPos.z);
    }
    
}
