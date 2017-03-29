using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {

    public GameObject projectile;
    public float speed = 10.0f, destroyTime = 1.5f;

	void Start () {
        //projectile = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            Destroy(proj, destroyTime);
        }
	}
}
