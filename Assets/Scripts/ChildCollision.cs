using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    public AnimationAction animationAction;
    private void OnCollisionEnter(Collision other) {
        animationAction.onChildCollision(other);
    }

}
