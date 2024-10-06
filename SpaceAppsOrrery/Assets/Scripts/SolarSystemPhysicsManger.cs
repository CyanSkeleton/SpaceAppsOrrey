using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemPhysicsManger : MonoBehaviour
{
    public float targetGravity = 100f;
    public float gravity = 100f;
    GameObject[] celestialBodies;
    GameObject sun;

    // Start is called before the first frame update
    void Start()
    {
        gravity = targetGravity;
        celestialBodies = GameObject.FindGameObjectsWithTag("Celestial Body");
        sun = this.gameObject;

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        if(targetGravity != gravity)
        {
            GravityChange();
        }
            GravityPhysics();
    }

    void GravityPhysics() 
    {
        foreach (GameObject a in celestialBodies)
        {
            a.transform.LookAt(sun.transform);
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
            a.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if (a.GetComponent<Planet>() != null)
            {
                a.GetComponent<Planet>().ScaleTrailRendererTime(gravity);
            }
            foreach (GameObject b in celestialBodies) 
            {
                if(!a.Equals(b))
                {
                    float massB = b.GetComponent<Rigidbody>().mass;
                    float distance = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((gravity * massB) / distance);
                }
                if(gravity == 0)
                {
                    a.GetComponent<TrailRenderer>().Clear();
                }
            }
        }
    }

    void GravityChange()
    {
        gravity = targetGravity;
        sun.GetComponent<TrailRenderer>().Clear();
        foreach (GameObject a in celestialBodies)
        {
            InitialVelocity();
        }
    }
}
