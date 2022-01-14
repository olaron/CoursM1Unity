using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var particle = GetComponent<ParticleSystem>();
        particle.Play();
        Destroy(gameObject,particle.main.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
