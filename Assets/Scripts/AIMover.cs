using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private static readonly int Speed = Animator.StringToHash("Speed");

    [Tooltip("Max linear speed"), Range(0,15)]
    public float maxSpeed = 5;
    [Tooltip("Max angular speed"), Range(0,15)]
    public float maxAngularSpeed = 5;

    private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.inertiaTensor = Vector3.one;
        rb.inertiaTensorRotation = new Quaternion(0, 0, 0,1);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            if (Input.GetButton("Fire1") && rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.right * 30);    
            }
            if (Input.GetButton("Fire2") && rb.velocity.magnitude < maxAngularSpeed)
            {
                rb.AddTorque(transform.up * 10);    
            }
        }
        
        anim.SetFloat(Speed, rb.velocity.magnitude);
    }
}
