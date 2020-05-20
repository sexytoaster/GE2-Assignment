using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLasers : MonoBehaviour
{
    public int delay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        Destroy(this.gameObject, delay);
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        Debug.Log("CollisionDestroy");
    }
}
