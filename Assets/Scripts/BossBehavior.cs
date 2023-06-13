using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject minion;
    public GameObject spawner;
    private GameObject player;
    private Rigidbody rb;
    private float timerMinions;
    private float timerDash;
    private SpawnManager spawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = spawner.GetComponent<SpawnManager>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        timerMinions = 2;
        timerDash = 5;
        Invoke(nameof(SpawnMinions), timerMinions);
        Invoke(nameof(DashAbility), timerDash);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnMinions()
    {
        timerMinions = Random.Range(5, 7);
        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnPosition = spawnerScript.GenerateSpawnPosition(8f);
            Instantiate(minion, spawnPosition, minion.transform.rotation);
        }
        Invoke(nameof(SpawnMinions), timerMinions);
    }

    void DashAbility()
    {
        timerDash = Random.Range(7, 9);
        Vector3 directionVector = (player.transform.position - transform.position).normalized;
        rb.AddForce(50 * directionVector, ForceMode.Impulse);
    }
}
