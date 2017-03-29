using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanPlayer : MonoBehaviour {

    public Animator anim;
    public Rigidbody body;
    private float inputH, inputV;
    private bool isRunning = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
    /* ======================
     * Idle Animation tests
     * ======================
     */
        if (Input.GetKeyDown("1"))
            anim.Play("WAIT01", -1, 0f);
        else if (Input.GetKeyDown("2"))
            anim.Play("WAIT02", -1, 0f);
        else if (Input.GetKeyDown("3"))
            anim.Play("WAIT03", -1, 0f);
        else if (Input.GetKeyDown("4"))
            anim.Play("WAIT04", -1, 0f);

        /* ======================
         * Damage Animation Tests
         * ======================
         */

        if (Input.GetMouseButtonDown(0))
        {
            int n = Random.Range(0, 2);
            if (n == 1)
                anim.Play("DAMAGED00", -1, 0f);
            else
                anim.Play("DAMAGED01", -1, 0f);
        }

        /* =====================
         * Run and Jump Booleans
         * =====================
         */

        if (Input.GetKey(KeyCode.LeftShift))
            isRunning = true;
        else
            isRunning = false;

        if (Input.GetKey(KeyCode.Space))
            anim.SetBool("jump", true);
        else
            anim.SetBool("jump", false);

        /* ==================================
         * Horizontal and Vertical Axes Input
         *  -------------------------------
         * - Controls left and right button presses
         * - Controls speed at which Unity-chan moves.
         */ 

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("run", isRunning);

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 20f * Time.deltaTime;

        if (moveZ <= 0)
            moveX = 0f;

        else if (isRunning)
        {
            moveX *= 3f;
            moveZ *= 5f;
        }
        body.velocity = new Vector3(moveX, 0f, moveZ);
    }
}
