using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public Camera camera5;
    public Camera camera6;
    public Camera camera7;

    public SmoothCamera camera1Smooth;

    public GameObject redLeaderFighter;
    public GameObject blueLeaderFighter;
    public GameObject blueLeaderBomber;
    public GameObject midnight;
    public GameObject starmade;

    private Arrive fighterArrived;
    private Arrive bomberArrived;

    private shipController starmadeShip;
    private shipController midnightShip;

    public Vector3 offset;
    private Vector3 tempOffset;
    public Vector3 panOffset;

    public AudioClip engineAudio;
    public AudioClip fighterAudio;
    public AudioClip bombersClosingAudio;
    public AudioClip bombersEngageAudio;
    public AudioClip midnightAudio;
    public AudioClip starmadeAudio;
    public AudioClip retreatAudio;

    private bool observeFighters = false;
    private bool observeBombers = false;

    private bool introStarted = false;
    private bool introDone = false;
    private bool panDone = false;
    private bool bombersDone = false;
    private bool bombersEngagedDone = false;
    private bool midnightDone = false;
    private bool starmadeDone = false;

    private bool panningCam = false;

    private bool bomberEngaged = false;
    private bool widePan = false;

    public bool retreat = false;
    // Start is called before the first frame update
    void Start()
    {
        fighterArrived = blueLeaderFighter.GetComponent<Arrive>();
        bomberArrived = blueLeaderBomber.GetComponent<Arrive>();

        starmadeShip = starmade.GetComponent<shipController>();
        midnightShip = midnight.GetComponent<shipController>();

        offset = camera1.GetComponent<SmoothCamera>().offset;
        tempOffset = offset;

        panOffset = new Vector3(offset.z + 100, offset.y, offset.x + 100);

        camera1Smooth = camera1.GetComponent<SmoothCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(introStarted == false)
        {
            StartCoroutine("introCamera");
        }
        if (fighterArrived.enabled == true && introDone == false && introStarted == true)
        {
            StartCoroutine("introFighterCamera");
        }
        if(fighterArrived.enabled == false && panDone == false && introStarted == true)
        {
            introDone = true;
            StartCoroutine("observeFightersCamera");
        }
        if(observeBombers == true && bombersDone == false)
        {
            StartCoroutine("observeBombersCamera");
            
        }
        if(bomberArrived.enabled == false && bombersEngagedDone == false)
        {
            bombersEngagedDone = true;
            bomberEngaged = true;
            GetComponent<AudioSource>().PlayOneShot(bombersEngageAudio);
        }
        if (bomberArrived.enabled == true && bomberEngaged == true && midnightDone == false && starmadeDone == false && panningCam == false)
        {
            StartCoroutine("Panning");
        }
        if( midnight != null && midnightShip.hpPercent <= 20 && midnightDone == false)
        {

            StartCoroutine("midnightCamera");
        }
        if (starmade != null && starmadeShip.hpPercent <= 20 && starmadeDone == false)
        {
            StartCoroutine("starmadeCamera");
            
        }
        if (((midnight == null && starmadeShip.hpPercent > 20) || (starmade == null && midnightShip.hpPercent > 20)) && panningCam == false)
        {
            StartCoroutine("Panning");
            Debug.Log("sidepan");
        }

        if (midnight == null && starmade == null && retreat == false)
        {
            StartCoroutine("OrderRetreat");
        }

        
        
    }

    void LateUpdate()
    {
        if(observeFighters == true)
        {
            introDone = true;
            if (Vector3.Distance(tempOffset, panOffset) > 1)
            {
                tempOffset = Vector3.Slerp(tempOffset, panOffset, Time.deltaTime);
                camera1Smooth.offset = tempOffset;
            }
            else
            {
                observeFighters = false;
                observeBombers = true;
            }
            
        }
        
    }
    
    IEnumerator introCamera()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera5.enabled = true;
        camera6.enabled = false;
        camera7.enabled = false;


        yield return new WaitForSeconds(7);
        introStarted = true;
        Debug.Log("Introcam");
    }
    IEnumerator Panning()
    {
        panningCam = true;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera5.enabled = false;

        int pickCam = Random.Range(0, 2);

        switch (pickCam)
        {
            case 0:
                camera4.enabled = true;
                camera6.enabled = false;
                camera7.enabled = false;
                break;
            case 1:
                camera4.enabled = false;
                camera6.enabled = true;
                camera7.enabled = false;
                break;
            case 2:
                camera4.enabled = false;
                camera6.enabled = false;
                camera7.enabled = true;
                break;
        }


        yield return new WaitForSeconds(7);
        panningCam = false;
        Debug.Log("panning");
    }
    IEnumerator introFighterCamera()
    {
        introDone = true;
        camera5.enabled = false;

        if (camera1.enabled == false && camera2.enabled == false)
            camera2.enabled = true;
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;


        GetComponent<AudioSource>().PlayOneShot(engineAudio);
        yield return new WaitForSeconds(4);
        introDone = false;
        Debug.Log("Intro fighter cam");
    }

    IEnumerator observeFightersCamera()
    {

        camera1.enabled = true;
        camera2.enabled = false;
        observeFighters = true;
        panDone = true;
        introDone = true;
        yield return null;
        GetComponent<AudioSource>().PlayOneShot(fighterAudio);
        Debug.Log("observe fighter cam");
    }

    IEnumerator observeBombersCamera()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = true;
        camera4.enabled = false;
        camera5.enabled = false;
        camera6.enabled = false;
        camera7.enabled = false;
        observeBombers = false;

        bombersDone = true;
        Debug.Log("bombers");
        GetComponent<AudioSource>().PlayOneShot(bombersClosingAudio);
        yield return null;
        
    }

    IEnumerator midnightCamera()
    {
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera5.enabled = false;
        camera6.enabled = false;
        camera7.enabled = false;

        camera1Smooth.offset = new Vector3(offset.x + 50, offset.y + 30, offset.z + 100);
        camera1Smooth.target = midnight.transform;
        GetComponent<AudioSource>().PlayOneShot(midnightAudio);
        Debug.Log("midnight");
        midnightDone = true;
        yield return null;

    }
    IEnumerator starmadeCamera()
    {
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera5.enabled = false;
        camera6.enabled = false;
        camera7.enabled = false;

        camera1Smooth.offset = new Vector3(offset.x + 50, offset.y + 30, offset.z + 100); 
        camera1Smooth.target = starmade.transform;
        Debug.Log("starmade");
        GetComponent<AudioSource>().PlayOneShot(starmadeAudio);
        starmadeDone = true;
        yield return null;

    }
    IEnumerator OrderRetreat()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera5.enabled = true;
        camera6.enabled = false;
        camera7.enabled = false;
        Debug.Log("retreat");
        GetComponent<AudioSource>().PlayOneShot(retreatAudio);
        retreat = true;
        yield return new WaitForSeconds(10);

    }
}
