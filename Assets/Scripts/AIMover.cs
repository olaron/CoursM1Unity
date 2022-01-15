using System;
using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    public static int destroyedNumber = 0;
    
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
    //private bool pathFound = false;

    private Transform player;
    private Collider boxCollider;

    private void Awake()
    {
        //destination = GameObject.FindGameObjectWithTag("Player").transform;
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
        agent.destination = player.position;
        // pathFound = agent.CalculatePath(destination.position, path);
        // if(!pathFound)
        // {
        //     Debug.LogError("Path not found");
        // }
        boxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
        if (transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnDrawGizmos()
    {
        // if (pathFound)
        // {
        //     Gizmos.color = Color.red;
        //     for (int i = 0; i <= path.corners.Length - 2; i++)
        //     {
        //         Gizmos.DrawLine(path.corners[i],path.corners[i+1]);    
        //     }
        // }
    }

    void FixedUpdate()
    {
        //Debug.Log(agent.velocity.magnitude);
        anim.SetFloat(Speed, agent.velocity.magnitude);
    }

    private void OnDestroy()
    {
        destroyedNumber += 1;
    }
    
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<FPSMovement>();
        if (player != null)
        {
            player.Die();
            SpawnerManager.dead = true;
        }
    }
}
