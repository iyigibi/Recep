using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public Coin(){
        this.name_="coin";
    }
    public void collected(){
        gameObject.SetActive(false);
    }
    
}
