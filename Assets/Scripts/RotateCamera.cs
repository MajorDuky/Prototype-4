using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 75f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            transform.Rotate(Time.deltaTime * rotationSpeed * horizontalInput * Vector3.up);
        }
    }
}
