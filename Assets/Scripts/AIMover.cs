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

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.inertiaTensor = Vector3.one;
        rb.inertiaTensorRotation = new Quaternion(0, 0, 0,1);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
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

    void FixedUpdate()
    {
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
