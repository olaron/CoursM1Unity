using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 100;
    public GameObject explosion;
    
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
    
    void OnCollisionEnter(Collision other)
    {
        AIMover ai = other.gameObject.GetComponent<AIMover>();
        if (ai != null)
        {
            Instantiate(explosion,other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
