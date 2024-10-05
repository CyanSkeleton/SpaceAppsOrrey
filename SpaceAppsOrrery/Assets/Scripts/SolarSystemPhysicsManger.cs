using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemPhysicsManger : MonoBehaviour
{
    float gravity = 100f;
    GameObject[] celestialBodies;

    // Start is called before the first frame update
    void Start()
    {
        celestialBodies = GameObject.FindGameObjectsWithTag("Celestial Body");

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        GravityPhysics();
    }

    void GravityPhysics() 
    {
        foreach (GameObject a in celestialBodies)
        {
            foreach (GameObject b in celestialBodies)
            {
                if(!a.Equals(b))
                {
                    float massA = a.GetComponent<Rigidbody>().mass;
                    float massB = b.GetComponent<Rigidbody>().mass;
                    float distance = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (gravity * (massA * massB) / (distance * distance)));
                }
            }
        }
    }

    void InitialVelocity() 
    {
        foreach (GameObject a in celestialBodies)
        {
            foreach (GameObject b in celestialBodies) 
            {
                if(!a.Equals(b))
                {
                    float massB = b.GetComponent<Rigidbody>().mass;
                    float distance = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((gravity * massB) / distance);
                }
            }
        }
    }
}
