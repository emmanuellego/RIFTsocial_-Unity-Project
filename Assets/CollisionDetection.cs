using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    void DestroyProjectile(Collision col)
    {
        if (col.gameObject.name == "Boundary")
            Destroy(col.gameObject);
    }
}
