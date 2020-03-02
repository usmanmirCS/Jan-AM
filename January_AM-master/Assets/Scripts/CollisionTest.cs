using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("This box was hit");

        Instantiate(prefabToInstantiate, transform.position + Vector3.up * 10f, prefabToInstantiate.transform.rotation); // Make a clone of prefabToInstantiate
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + " Exited this cube");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
