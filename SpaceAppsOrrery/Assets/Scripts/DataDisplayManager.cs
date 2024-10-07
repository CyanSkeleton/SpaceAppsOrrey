using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplayManager : MonoBehaviour
{
    public GameObject nameLine;
    public GameObject massLine;
    public GameObject radiusLine;
    public GameObject distanceLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPlanetChange(GameObject planet)
    {
        nameLine.GetComponent<TextMeshPro>().text = planet.name;
        massLine.GetComponent<TextMeshPro>().text = "Mass: " + planet.GetComponent<Planet>().trueMass;
        radiusLine.GetComponent<TextMeshPro>().text = "Radius: " + planet.gameObject.GetComponent<Planet>().trueRadius;
        distanceLine.GetComponent<TextMeshPro>().text = "Distance: " + planet.GetComponent<Planet>().trueDistance;
    }
}
