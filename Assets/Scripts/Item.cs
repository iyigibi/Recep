using UnityEngine;

public class Item : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    public static int indexKey=0;
    public bool isHit;
    protected string name_;
    protected Vector3 startPos;
    public virtual bool doStuff() {
        return true;
       // Debug.Log("item");
    }
    public void OnObjectSpawn()
    {
        //Debug.Log();
        this.name=name_+indexKey.ToString();
        startPos=transform.position;
        //transform.Translate();
        indexKey++;
    }

    protected virtual void Update()        
     {

    }

}
