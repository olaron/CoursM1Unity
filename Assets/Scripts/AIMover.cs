using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            if (Input.GetButton("Fire1"))
            {
                rb.AddForce(transform.right * 100);    
            }
            if (Input.GetButton("Fire2"))
            {
                rb.AddForce(transform.right * -100);    
            }
        }
        
        anim.SetFloat(Speed, rb.velocity.magnitude);
    }
}
