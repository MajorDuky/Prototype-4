using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : MonoBehaviour
{
    private GameObject[] enemies;
    private GameObject target;
    private Rigidbody rb;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 50f;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            int randomIndex = Random.Range(0, enemies.Length);
            target = enemies[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.AddForce(speed * Time.deltaTime * direction, ForceMode.Impulse);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
