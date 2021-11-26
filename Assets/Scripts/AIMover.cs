using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private NavMeshAgent agent;
    private static readonly int Speed = Animator.StringToHash("Speed");

    [Tooltip("Max linear speed"), Range(0,15)]
    public float maxSpeed = 5;
    [Tooltip("Max angular speed"), Range(0,15)]
    public float maxAngularSpeed = 5;

    private Transform destination;

    private NavMeshPath path;
    private bool pathFound = false;

    private Transform player;

    private void Awake()
    {
        destination = GameObject.FindGameObjectWithTag("Destination").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.inertiaTensor = Vector3.one;
        rb.inertiaTensorRotation = new Quaternion(0, 0, 0,1);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        agent.destination = destination.position;
        pathFound = agent.CalculatePath(destination.position, path);
        if(!pathFound)
        {
            Debug.LogError("Path not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (pathFound)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i <= path.corners.Length - 2; i++)
            {
                Gizmos.DrawLine(path.corners[i],path.corners[i+1]);    
            }
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            if (Input.GetButton("Fire1") && rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * 30);    
            }
            if (Input.GetButton("Fire2") && rb.velocity.magnitude < maxAngularSpeed)
            {
                rb.AddTorque(transform.up * 10);    
            }
        }
        
        Debug.Log(agent.velocity.magnitude);
        anim.SetFloat(Speed, agent.velocity.magnitude);
    }
}
