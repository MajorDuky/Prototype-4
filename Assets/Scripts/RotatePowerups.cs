using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerups : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime * Vector3.up);
    }
}
