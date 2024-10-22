using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemPhysicsManger : MonoBehaviour
{
    public float targetGravity = 100f;
    public float gravity = 100f;
    GameObject[] celestialBodies;
    GameObject sun;
    bool frozen;

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
        targetGravity = Mathf.Clamp(targetGravity, 0, 0.75f);
        gravity = Mathf.Clamp(gravity, 0, 0.75f);
        if (targetGravity != gravity)
        {
            GravityChange();
        }
            GravityPhysics();
    }

    void GravityPhysics() 
    {
        Mathf.Clamp(targetGravity, 0, 1);
        Mathf.Clamp(gravity, 0, 1);

        foreach (GameObject a in celestialBodies)
        {
            a.transform.LookAt(sun.transform);
            foreach (GameObject b in celestialBodies)
            {
                if(!a.Equals(b) && gravity != 0)
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
        Mathf.Clamp(targetGravity, 0, 1);
        Mathf.Clamp(gravity, 0, 1);

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
                if(gravity == 0 && a.GetComponent<Planet>() != null)
                {
                    a.GetComponentInChildren<Planet>().tr.Clear();
                }
            }
            if (gravity == 0 && !frozen)
            {
                a.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                a.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                frozen = true;
            }
            else if (gravity != 0 && frozen)
            {
                a.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                frozen = false;
            }
        }
    }

    public void increaseSpeed()
    {
        targetGravity += (float)0.05;
    }

    public void decreaseSpeed()
    {
        targetGravity -= (float)0.05;
    }

    void GravityChange()
    {
        gravity = targetGravity;
        sun.GetComponentInChildren<TrailRenderer>().Clear();
        foreach (GameObject a in celestialBodies)
        {
            InitialVelocity();
        }
    }
}
