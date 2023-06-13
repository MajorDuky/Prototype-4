using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private Vector3 followVector;
    private Rigidbody rb;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        followVector = (player.transform.position - transform.position).normalized;
        rb.AddForce(speed * followVector);
        if (transform.position.y <= -25)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            Vector3 awayFromEnemy = other.transform.position - transform.position;
            rb.AddExplosionForce(1500f, awayFromEnemy, 1000f, 125f);
            Destroy(other.gameObject);
            explosionParticle.Play();
        }
    }
}
