using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject focalPoint;
    public GameObject powerupRing;
    public GameObject stompRing;
    public GameObject rocket;
    public ParticleSystem stompParticle;
    private float verticalInput;
    private float speed;
    private bool hasPowerup;
    private float powerupStrength;
    private bool hasStompPowerup;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 3f;
        focalPoint = GameObject.Find("Focal Point");
        hasPowerup = false;
        powerupStrength = 15.0f;
        hasStompPowerup = false;
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            rb.AddForce(speed * verticalInput * focalPoint.transform.forward);
        }
        Vector3 newPowerupPos = transform.position + new Vector3 (0, -0.5f, 0);
        powerupRing.transform.position = newPowerupPos;
        stompRing.transform.position = newPowerupPos;
        if (Input.GetAxis("Jump") != 0 && hasStompPowerup)
        {
            initiateStomp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupRing.SetActive(true);
            StartCoroutine(nameof(CountdownPowerupRoutine));
        }
        else if (other.CompareTag("RocketPowerup"))
        {
            FireRockets();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("StompPowerup"))
        {
            hasStompPowerup = true;
            stompRing.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator CountdownPowerupRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupRing.SetActive(false);
    }

    void FireRockets()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(rocket, transform.position, rocket.transform.rotation);
        }
    }

    void initiateStomp()
    {
        hasStompPowerup = false;
        stompRing.SetActive(false);
        rb.AddForce(10 * Vector3.up, ForceMode.Impulse);
        StartCoroutine(nameof(StompFollowUpRoutine));
    }
    IEnumerator StompFollowUpRoutine()
    {
        yield return new WaitForSeconds(1);
        rb.AddForce(50 * Vector3.down, ForceMode.Impulse);
        Vector3 impactVector = new Vector3(transform.position.x, 0, transform.position.z);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in enemies)
        {
            item.GetComponent<Rigidbody>().AddExplosionForce(2000f, impactVector, 1500f, 0);
        }
        stompParticle.Play();
    }
}
