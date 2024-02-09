using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gun;
    public CharacterController controller;
    public Animator animator;
    public Transform characterTransform,camStand;
    public Camera cam;
    public float speed = 1f,jumpHeight=1.6f,gravity=9.8f;

    KeyCode[] WASD = new KeyCode[4] { KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A };

    float[] PorN = new float[4] { 1, -1, 1, -1 };

    Vector3[] ForR = new Vector3[4] ;
    Vector3 dir,moveVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("isArmed"))gun.SetActive(true);
        else gun.SetActive(false);

        if(animator.GetBool("isAim")){
            WeaponMove();
            WeaponUse();
            ArmedRotate();
        }
        else{
            Move();
            MoveRotate();
        }

    }
    void Move()
    {
        moveVector = Vector3.zero;//initialize
        if(controller.isGrounded)//initialize if is on the ground
        dir = Vector3.zero;

        ForR = new Vector3[4] { camStand.transform.forward, camStand.transform.forward, camStand.transform.right, camStand.transform.right };//同樣的東西做一個陣列存起來，不會亂掉

        //move
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(WASD[i]))
            {
                moveVector += ForR[i] * PorN[i];
                dir = transform.forward;
            }
        }
        moveVector=moveVector.normalized;
        //detect if run or not
        dir *= speed;
        if (Input.GetKey(KeyCode.LeftShift))
            dir *= 4f;
        else
            dir *=1.5f;
        controller.Move(dir*Time.deltaTime);
    }    
    void MoveRotate()
    {
        for(int i = 0; i <4; i++)
            if(Input.GetKey(WASD[i]))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(moveVector), 0.1f);
            }
    }
    //controller.SimpleMove();

    void WeaponMove()
    {
        moveVector = Vector3.zero;//initialize
        if (controller.isGrounded)//initialize if is on the ground
            dir = Vector3.zero;

        ForR = new Vector3[4] { camStand.transform.forward, camStand.transform.forward, camStand.transform.right, camStand.transform.right };//同樣的東西做一個陣列存起來，不會亂掉

        //move
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(WASD[i]))
            {
                dir += ForR[i] * PorN[i];
            }
        }
        //detect if run or not
        dir = dir.normalized;
        dir *= speed;
        dir *= 1.5f;
        controller.Move(Vector3.Lerp(controller.velocity, dir, 0.5f)*Time.deltaTime);
    }
    void WeaponUse()
    {
        ArmedRotate();
        if(Input.GetKey(KeyCode.Mouse0))
        Fire();
    }
    void ArmedRotate()
    {

        float targetAngle = 0;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            targetAngle = Mathf.Atan2(hit.point.x-characterTransform.position.x, hit.point.z - characterTransform.position.z);
        }
        Vector3 targetVector=new Vector3(0,targetAngle*180/Mathf.PI,0);
        Quaternion targetRotation=Quaternion.identity;
        targetRotation.eulerAngles=targetVector;
        characterTransform.rotation = Quaternion.Lerp(characterTransform.rotation,targetRotation, 0.1f);
    }
    void Fire()
    {
        
    }
}
