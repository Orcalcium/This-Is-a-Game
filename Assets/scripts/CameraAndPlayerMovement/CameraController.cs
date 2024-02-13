using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player,cam,camStand;
    Quaternion camTargetAngle, TargetAngle;
    private Vector3 mousePos,nextMousePos;
    [SerializeField]
    float camDiameter,rotateSenstivity=6f,scrollSenstivity=3f,defaultCamDiameter=10f, camLerpTargetValue ;
    
    void Start()
    {
        camLerpTargetValue = defaultCamDiameter;
    }

    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    private void LateUpdate()
    {
        
        MoveCamera();
    }


    void Rotate()
    {
   
        if (Input.GetKey(KeyCode.Mouse2))
        {
            
            camTargetAngle=Quaternion.Euler(0,camStand.rotation.eulerAngles.y +Input.GetAxis("Mouse X")*rotateSenstivity, 0);

            camStand.rotation = Quaternion.Slerp(camStand.rotation, camTargetAngle, 0.8f);
        }

    }

    void MoveCamera()
    {
        camDiameter = LerpCamDiameter(camDiameter, scrollSenstivity);
        if (camDiameter < 3) camDiameter = 3;
        if (camDiameter > 30) camDiameter = 30;
        camStand.position = Vector3.Lerp(camStand.position,player.position+0.8f*player.up,0.1f);
        cam.position = camStand.position-cam.forward*camDiameter;
    }
    float LerpCamDiameter(float camDiameter, float scrollSenstivity)
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (camLerpTargetValue < 3) camLerpTargetValue = 3;
            if (camLerpTargetValue > 30) camLerpTargetValue = 30;
            camLerpTargetValue -= Input.mouseScrollDelta.y * scrollSenstivity;
        }
        if(Mathf.Abs(camLerpTargetValue - camDiameter)>0.3)
            camDiameter = Mathf.Lerp(camDiameter, camLerpTargetValue, 0.4f);
        return camDiameter;
    }
}
