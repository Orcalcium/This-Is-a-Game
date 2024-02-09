using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class bulletattribute : MonoBehaviour
{

    private float range = 10f,bulletSpeed=50f;
    
    private float bulletLifePeriod, bulletDestroyTime;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.right * bulletSpeed;
        bulletLifePeriod = range / bulletSpeed;
        bulletDestroyTime = Time.time + bulletLifePeriod;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > bulletDestroyTime)
            Destroy(this.gameObject);
    }
}
