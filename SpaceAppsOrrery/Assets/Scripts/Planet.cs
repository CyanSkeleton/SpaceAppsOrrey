using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : MonoBehaviour
{
    public float trueMass;
    public float trueRadius;
    public float trueDistance;
    public float modifiedMass;
    public float modifiedRadius;
    public float modifiedDistance;
    public float baseTrailRendererModifiedTime;
    public Rigidbody rb;
    public TrailRenderer tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    void SwapToTrueMass()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.mass = trueMass;
        this.gameObject.transform.localScale = new Vector3(trueRadius, trueRadius, trueRadius);
        this.gameObject.transform.position = new Vector3(trueDistance, 0, 0);
    }
    public void ScaleTrailRendererTime(float gravity)
    {
        tr.time = (float)(((0.1 / gravity) * 1.5f) * baseTrailRendererModifiedTime);
    }
}
