using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{

    public GameObject bullet;
    private Rigidbody rb;
    public float speed;
    public float shotsPerMin;
    public float lastShot;
    // Start is called before the first frame update
    void OnEnable()
    {
        lastShot = Time.time;
        lastShot -= (Random.Range(0, 60 / shotsPerMin));
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShot >= 60 / shotsPerMin)
        {
            GameObject instantiatedBullet = Instantiate(bullet, transform.position, transform.rotation);

            rb = instantiatedBullet.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.down * speed);
            //+= new Vector3(-speed, 0, 0);
            lastShot = Time.time;
        }
        
    }
}
