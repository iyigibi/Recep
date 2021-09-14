using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public Coin(){
        this.name_="coin";
        isHit=false;
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
    
}
