using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform mercury;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeCamera()
    {
        GetComponent<CinemachineFreeLook>().LookAt = mercury;
        GetComponent<CinemachineFreeLook>().Follow = mercury;
    }
}
