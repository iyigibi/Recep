using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    Pooler pooler;

    void Start()
    {
        pooler=Pooler.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pooler.SpawnFromPool("Bomb",transform.position,Quaternion.identity);
    }
}
