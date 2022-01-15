using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Round
{
    public int enemies;
    public float spawnInterval;
    public float duration;
}

public class SpawnerManager : MonoBehaviour
{
    public AISpawner[] spawners;
    public Round[] rounds;
    public TextMeshProUGUI text;
    
    private int nextRound = 0;
    private float roundDuration;
    private float time;
    private int enemiesKilledBeforeNextRound;

    public static bool dead = false;
    
    void NextRound()
    {
        if (nextRound < rounds.Length)
        {
            var round = rounds[nextRound];
            nextRound += 1;
            text.text = "Round " + nextRound;
            time = 0;
            roundDuration = round.duration;
            AIMover.destroyedNumber = 0;
            enemiesKilledBeforeNextRound = round.enemies * spawners.Length;
            foreach (var spawner in spawners)
            {
                spawner.PlayRound(round.enemies, round.spawnInterval);
            }
        }
        else
        {
            time = 0;
            roundDuration = 2;
            AIMover.destroyedNumber = 0;
            enemiesKilledBeforeNextRound = 1;
            text.text = "You win";
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        NextRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            text.text = "You died\nPress ENTER to retry";
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            time += Time.deltaTime;

            if (time > 1 && text.text.Length > 0)
            {
                text.text = "";
            }
        
            if (time >= roundDuration || AIMover.destroyedNumber >= enemiesKilledBeforeNextRound)
            {
                NextRound();
            }
        }
    }
}
