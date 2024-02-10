using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator ani;
    public Transform player,camStand;
    public CharacterController chController;
    public bool isWalk=false, isRun,isArmed,isIdle,isAim,mouseRightPressed,aimW,aimA,aimS,aimD;
    Vector3 walkDir;
    // Start is called before the first frame update
    void Start()
    {
        isWalk = isRun = false;

        isIdle=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))mouseRightPressed=true;
        if(Input.GetKeyUp(KeyCode.Mouse1))mouseRightPressed=false;
        //determine main states
        if(Input.GetKeyDown(KeyCode.Alpha1))isArmed=!isArmed;

        if (isArmed && mouseRightPressed)isAim = true;
        else isAim = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {

            //determine walk direction
            walkDir = Vector3.zero;
            walkDir.y =camStand.rotation.eulerAngles.y- player.rotation.eulerAngles.y ;
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A))
                    walkDir.y += 315;
                else if (Input.GetKey(KeyCode.D))
                    walkDir.y += 45;
                else walkDir.y += 0;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                    walkDir.y += 225;
                else if (Input.GetKey(KeyCode.D))
                    walkDir.y += 135;
                else walkDir.y += 180;
            }
            else if (Input.GetKey(KeyCode.A)) walkDir.y += 270;
            else if (Input.GetKey(KeyCode.D)) walkDir.y += 90;

            if (walkDir.y < 0) walkDir.y = 360f + walkDir.y;
            if (walkDir.y > 360) walkDir.y = walkDir.y - 360f;

            if(walkDir.y<=60f||walkDir.y>=300f)aimW=true;
            else aimW=false;
            if(walkDir.y >= 210f && walkDir.y <= 330f) aimA=true;
            else aimA = false; 
            if(walkDir.y >= 120f && walkDir.y <= 240f)aimS=true;
            else aimS = false;
            if (walkDir.y >= 30f && walkDir.y <= 150f) aimD = true;
            else aimD = false;

            if (Input.GetKey(KeyCode.LeftShift)&&!isAim)
            {
                isRun = true;
                isWalk = isIdle= false;
            }
            else
            {
                isWalk = true;
                isRun = isIdle = false;
            }
        }
        else
        {
            aimW = aimA = aimS = aimD = false;
            isWalk =  isRun= false;
            isIdle =true;
        }

        //set the bools to the state machine papameter bools
        ani.SetBool("isWalk", isWalk);
        ani.SetBool("isRun", isRun);
        ani.SetBool("isArmed",isArmed);
        ani.SetBool("isIdle",isIdle);
        ani.SetBool("isAim",isAim);

        ani.SetBool("a", aimA);
        ani.SetBool("w", aimW);
        ani.SetBool("s", aimS);
        ani.SetBool("d", aimD);

    }
}
