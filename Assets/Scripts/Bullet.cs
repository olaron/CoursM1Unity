using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (transform.position.magnitude > 100)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("ok");
        AIMover ai = other.gameObject.GetComponent<AIMover>();
        if (ai != null)
        {
            Debug.Log("bjr");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
