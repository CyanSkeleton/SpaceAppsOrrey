using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Planet : MonoBehaviour
{
    public float trueMass;
    public float trueMassExponent;
    public float trueRadius;
    public float trueDistance;
    public float modifiedMass;
    public float modifiedRadius;
    public float modifiedDistance;
    public float baseTrailRendererModifiedTime;
    public float fov;
    public Rigidbody rb;
    public TrailRenderer tr;
    public TMP_Text nameLine;
    public TMP_Text massLine;
    public TMP_Text radiusLine;
    public TMP_Text distanceLine;
    public CinemachineFreeLook cam;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponentInChildren<TrailRenderer>();
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
        if(tr == null)
        {
            tr = GetComponentInChildren<TrailRenderer>();
        }

        tr.time = (float)(((0.1 / gravity) * 1.5f) * baseTrailRendererModifiedTime);
    }

    public void OnPlanetChange()
    {
        nameLine.text = "Name: " + name;
        print(name);
        massLine.text = "Mass: " + trueMass + " X 10^" + trueMassExponent + " kg";
        print(trueMassExponent);
        radiusLine.text = "Radius: " + trueRadius + " miles";
        distanceLine.text = "Distance: " + trueDistance + " million miles";
        cam.m_Lens.FieldOfView = fov;
    }
}
