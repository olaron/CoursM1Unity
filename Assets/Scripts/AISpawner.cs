using UnityEngine;
using Random = UnityEngine.Random;

public class AISpawner : MonoBehaviour
{
    public Transform prefabAI;
    [Range(0,10)]
    public float spawnInterval = 3;
    private float time = 0;

    public Transform spawnPoint;
    private Vector3 lastSpawnForce;

    private int numberToSpawn = 0;
    private int spawned = 0;

    public void PlayRound(int numberToSpawn,float spawnInterval)
    {
        spawned = 0;
        this.numberToSpawn = numberToSpawn;
        this.spawnInterval = spawnInterval;
    }
    
    private void Spawn()
    {
        if (spawned < numberToSpawn)
        {
            spawned += 1;
            Transform ai = Instantiate(prefabAI);
            ai.position = spawnPoint.position;
            ai.rotation = spawnPoint.rotation;
            Rigidbody rb = ai.GetComponent<Rigidbody>();
            Vector3 spawnForce = spawnPoint.forward * 3;
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
            randomForce = randomForce.normalized;
            spawnForce += randomForce;
            spawnForce *= 3;
            rb.AddForce(spawnForce, ForceMode.Impulse);
            lastSpawnForce = spawnForce;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnInterval)
        {
            Spawn();
            time -= spawnInterval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + lastSpawnForce);
    }
}
