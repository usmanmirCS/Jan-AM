using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbForce : MonoBehaviour
{
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Runs at a fixed rate (50 times per second) - physics engine computes forces and collisions at this time
    private void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(Vector3.forward * 40f); // Vector3.forward is the blue axis globally
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(Vector3.back * 40f); // Vector3.forward is the blue axis globally
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(Vector3.left * 40f); // Vector3.forward is the blue axis globally
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector3.right * 40f); // Vector3.forward is the blue axis globally
        }
    }
}
