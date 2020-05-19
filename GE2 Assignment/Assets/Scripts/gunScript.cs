using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{

    public GameObject bullet;
    public float speed;
    public float shotsPerMin;
    public float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShot >= 60 / shotsPerMin)
        {
            GameObject instantiatedBullet = Instantiate(bullet, transform.position, transform.rotation);

            instantiatedBullet.GetComponent<Rigidbody>().velocity += new Vector3(-speed, 0, 0);
            lastShot = Time.time;
        }
        
    }
}
