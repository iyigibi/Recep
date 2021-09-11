using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        //Debug.Log("asd");
        transform.Translate(new Vector3(Random.Range(-10,10),Random.Range(-10,10),Random.Range(-10,10)));
    }

}
