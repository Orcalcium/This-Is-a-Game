using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponGameObeject;
    public GameObject bulletPrefab;
    public float fireDamage = 10f;
    public float fireRange = 40f;
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
    private void HandleAttack()
    {
        if (isAim)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                EnableFiring();
            }
        }

    }
    private void EnableFiring()
    {
        if (nextFireTime < Time.time)
        {
            Debug.Log(timeBetweenShots);
            GameObject bullet = Instantiate(bulletPrefab, weaponGameObeject.transform.position + transform.right * 0.3f, weaponGameObeject.transform.rotation);
            bullet.transform.rotation = characterAnimator.GetComponentInParent<Transform>().rotation;
            bulletList.Add(bullet); 
            nextFireTime = Time.time + 0.1f;
        }
        
    }
}
