using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigGunScript : MonoBehaviour
{

    public GameObject bullet;
    public GameObject[] enemys;
    public GameObject enemy;
    private Rigidbody rb;
    public float speed;
    public float shotsPerMin;
    public float lastShot;
    public float firingArc;
    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        lastShot -= (Random.Range(0, 60 / shotsPerMin));
        aquireTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy == null)
        {
            aquireTarget();
        }
        if(Time.time - lastShot >= 60 / shotsPerMin)
        {
            
            //Vector3.forward
            Vector3 directionVector = (enemy.transform.position - transform.position).normalized;
            //if target is in from of the gun, with a little bit of traverse limitation so it cannot shoot in hemispheres
            if(Vector3.Dot(directionVector, transform.forward) > firingArc)
            {
                GameObject instantiatedBullet = Instantiate(bullet, transform.position, transform.rotation);

                rb = instantiatedBullet.GetComponent<Rigidbody>();
                rb.AddForce(directionVector * speed);
                //+= new Vector3(-speed, 0, 0);
                lastShot = Time.time;
            }
            
        }
        
    }

    private void aquireTarget()
    {
        if (tag == "blueTeamShip")
        {
            enemys = GameObject.FindGameObjectsWithTag("redTeamShip");
        }
        if (tag == "redTeamShip")
        {
            enemys = GameObject.FindGameObjectsWithTag("blueTeamShip");
        }
        enemy = enemys[Random.Range(0, enemys.Length)];
    }
}
