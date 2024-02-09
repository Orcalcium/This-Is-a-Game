using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    public Animator ani;
    public Transform player, camStand;
    public CharacterController chController;
    public bool isWalk = false, isRun, isArmed, isIdle;
    Vector3 walkDir;
    // Start is called before the first frame update
    void Start()
    {
        isWalk = isRun = false;

        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            isRun = true;
            isWalk = isIdle = false;
        }
        else
        {
            isWalk = true;
            isRun = isIdle = false;
        }

        //set the bools to the state machine papameter bools
        ani.SetBool("isWalk", isWalk);
        ani.SetBool("isRun", isRun);
        ani.SetBool("isIdle", isIdle);
    }
}
