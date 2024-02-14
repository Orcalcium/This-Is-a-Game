using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponGameObeject;
    public GameObject bulletPrefab;
    public float fireDamage = 10f;
    public float fireRange = 40f;
    public float maxSpread = 0.5f;
    public float maxSpreadTime =5f;
    public Animator characterAnimator;
    private float nextFireTime;
    float firerate = 30f;
    private bool isAim;
    float timeBetweenShots=0.1f;
    private List<GameObject> bulletList=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = 1f/firerate;
    }

    // Update is called once per frame
    void Update()
    {
        isAim = characterAnimator.GetBool("isAim");
        HandleAttack();
    }
    float absMaxSpreadTime=0;
    private void HandleAttack()
    {
        if (isAim)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
                absMaxSpreadTime =maxSpreadTime+Time.time;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                EnableFiring(absMaxSpreadTime);
            }
        }

    }
    private void EnableFiring(float absMaxSpreadTime)
    {
        if (nextFireTime < Time.time)
        {
            Debug.Log(timeBetweenShots);
            Transform characterTransform=characterAnimator.GetComponentInParent<Transform>();
            Debug.Log("absMaxSpreadTime"+absMaxSpreadTime);
            float absSpread=Mathf.Min(maxSpread*(1-Mathf.Max((absMaxSpreadTime-Time.time),0)/maxSpreadTime),maxSpread);
            Quaternion bulletRotation = characterTransform.rotation*Quaternion.Euler(0f,Random.Range(absSpread,-absSpread),0f);
            GameObject bullet = Instantiate(bulletPrefab, weaponGameObeject.transform.position + transform.right * 0.3f, bulletRotation); 
            nextFireTime = Time.time + 0.1f;
        }
        
    }
}
