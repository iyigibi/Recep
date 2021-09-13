using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    public static int indexKey=0;
    protected string name_;
    public void OnObjectSpawn()
    {
        //Debug.Log();
        this.name=name_+indexKey.ToString();
        //transform.Translate();
        indexKey++;
    }

}
