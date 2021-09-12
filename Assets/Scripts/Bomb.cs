using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        //Debug.Log("asd");
        transform.Translate(new Vector3(Random.Range(-3f,3f),Random.Range(0f,10f),Random.Range(-10f,10f)));
    }

}
