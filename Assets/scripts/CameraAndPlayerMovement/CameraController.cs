using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player,cam,camStand;
    public Animator playerAnimator;
    Quaternion camTargetAngle, TargetAngle;
    private Vector3 mousePos,nextMousePos;
    [SerializeField]
    float camDiameter,rotateSenstivity=6f,scrollSenstivity=3f,defaultCamDiameter=10f, camLerpTargetValue,maxCamDiamater=10f,minCamDiameter=30f ;
    
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
        RotateCamStand();
    }
    private void LateUpdate()
    {
        
        MoveCamera();
    }


    void RotateCamStand()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            camTargetAngle=Quaternion.Euler(0,camStand.rotation.eulerAngles.y +Input.GetAxis("Mouse X")*rotateSenstivity, 0);
            camStand.rotation = Quaternion.Slerp(camStand.rotation, camTargetAngle, 0.8f);
        }

    }

    void MoveCamera()
    {
        if(playerAnimator.GetBool("isArmed"))
        {
            Quaternion targetRotation=camStand.rotation*Quaternion.Euler(80f,0f,0f);
            cam.rotation=Quaternion.Slerp(cam.rotation,targetRotation,0.02f);
            minCamDiameter=5f;
            maxCamDiamater=20f;
            camDiameter = LerpCamDiameter(camDiameter, scrollSenstivity);
            if (camDiameter < minCamDiameter) camDiameter = minCamDiameter;
            if (camDiameter > maxCamDiamater) camDiameter = maxCamDiamater;
            camStand.position = Vector3.Lerp(camStand.position,player.position+0.8f*player.up,0.05f);
            cam.position = camStand.position-cam.forward*camDiameter;
        }
        else
        {
            Quaternion targetRotation=camStand.rotation*Quaternion.Euler(35f,0f,0f);
            cam.rotation=Quaternion.Slerp(cam.rotation,targetRotation,0.02f);
            minCamDiameter=3f;
            maxCamDiamater=5f;
            camDiameter = LerpCamDiameter(camDiameter, scrollSenstivity);
            if (camDiameter < minCamDiameter) camDiameter = minCamDiameter;
            if (camDiameter > maxCamDiamater) camDiameter = maxCamDiamater;
            camStand.position = Vector3.Lerp(camStand.position,player.position+1.2f*player.up,0.05f);
            cam.position = camStand.position-cam.forward*camDiameter;
        }
    }
    float LerpCamDiameter(float camDiameter, float scrollSenstivity)
    {
        if (camLerpTargetValue < minCamDiameter) camLerpTargetValue = minCamDiameter;
        if (camLerpTargetValue > maxCamDiamater) camLerpTargetValue = maxCamDiamater;
        if (Input.mouseScrollDelta.y != 0)
        {
            camLerpTargetValue -= Input.mouseScrollDelta.y * scrollSenstivity;
        }
        if(Mathf.Abs(camLerpTargetValue - camDiameter)>0.3)
            camDiameter = Mathf.Lerp(camDiameter, camLerpTargetValue, 0.4f);
        return camDiameter;
    }
}
