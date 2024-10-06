using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    float angle = 0;
    float distanceFromPlanet;
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlanet = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        angle += (((float)4.6875));
    }

    private void FixedUpdate()
    {
        x = Mathf.Cos(angle);
        y = Mathf.Sin(angle);

        transform.localPosition = new Vector3(x, 0, y);
    }
}
