using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpIn : MonoBehaviour
{
    public GameObject cssFleet;
    public GameObject unscFleet;

    public GameObject[] flocks;
    public GameObject warpEffect;
    public AudioClip introAudio;
    public AudioClip engageAudio;

    private bool introDone = false;
    private bool warpDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine("IntroVoice");
       
    }

    void Update()
    {
        if (introDone == true && warpDone == false)
            StartCoroutine("Warp");
    }

    IEnumerator IntroVoice()
    {
        GetComponent<AudioSource>().PlayOneShot(introAudio);
        yield return new WaitForSeconds(7);
        introDone = true;
    }

    IEnumerator Warp()
    {
        warpDone = true;
        GameObject warp = Instantiate(warpEffect, transform.position, transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(engageAudio);
        yield return new WaitForSeconds(4);
        foreach (GameObject flock in flocks)
        {
            flock.SetActive(true);
        }
        cssFleet.SetActive(true);
        

    }
}
