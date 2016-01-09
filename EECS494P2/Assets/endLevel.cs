using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class endLevel : MonoBehaviour {
    GameObject terrain;
    GameObject player;
    //GameObject menu;
    Transform terrainTrans;
    Transform camTrans;
    Transform playerTrans;
    //Vector3 relPos;
    //Vector3 initialCamPos;
    Camera cam;
    float camH, camW;
    Vector3 camCenter;
    Bounds b;

    public GameObject linePrefab;
    static GameObject thisob;

    // Use this for initialization
    void Start () {
        //Destroy(CueLevels.currLevel);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camTrans = cam.GetComponent<Transform>();
        terrain = GameObject.Find("terrain");
        terrainTrans = terrain.transform;
        player = GameObject.Find("Player");
        playerTrans = player.transform;
        //relPos = new Vector3(camTrans.position.x - playerTrans.position.x,
         //   camTrans.position.y - playerTrans.position.y, 0);
        //initialCamPos = camTrans.position;
        //menu = GameObject.Find("MainMenu");
        Player.AIOn = false;
        Player.controlOn = false;
        CueLevels.brickOn = false;
        CueLevels.rotateTerrain = false;
        CueLevels.mode = 0;
        CueLevels.setCamMode = 0;
        terrainTrans.localRotation = Quaternion.identity;
        b = getParentBound(terrain);
        cam.transform.position = new Vector3(b.center.x, b.center.y-b.extents.y*0.15f, -10);
        Instantiate(linePrefab);
        CueLevels.BGMFadeOut = true;
        thisob = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        camH = cam.orthographicSize;
        camW = camH * cam.aspect;
        camCenter = cam.transform.position;

        if (b.max.y - camCenter.y > camH * 0.8f
                || b.min.y - camCenter.y < -camH *0.8f
                || b.max.x - camCenter.x > camW * 0.8f
                || b.min.x - camCenter.x < -camW * 0.8f)
            cam.orthographicSize += b.size.y * 0.4f * Time.deltaTime;
        if (DrawLine.lineOver == true) 
        {
            // CueLevels.dialog.SetActive(true);
            // CueLevels.dialog.GetComponentInChildren<Text>().text = "Press Enter to continue";
            CueLevels.menu2.SetActive(true);
            //if (Input.GetKey("return"))
            //{
            //    if (CueLevels.nextLevel != CueLevels.currLevel)
            //    {
            //        Instantiate(CueLevels.nextLevel);
            //        CueLevels.levelIndex += 1;
            //    }
            //    else
            //    {
            //        CueLevels.menu.SetActive(true);
            //    }
            //    CueLevels.dialog.SetActive(false);
            //    Destroy(this.gameObject);
            //}
        }

        
    }

    public void continueLevel()
    {
        CueLevels.retrying = false;
        if (CueLevels.levelIndex == 0) { CueLevels.menu.SetActive(true); }
        else { 
            if (CueLevels.nextLevel != CueLevels.currLevel)
            {
            
                Instantiate(CueLevels.nextLevel);
                CueLevels.levelIndex += 1;
            }
            else
            {
                CueLevels.menu.SetActive(true);
            }
        }CueLevels.menu2.SetActive(false);
        Destroy(thisob);
    }
    public void retry()
    {
        CueLevels.retrying = true;
        Instantiate(CueLevels.currLevel);        
        // CueLevels.levelIndex += 1;
        CueLevels.menu2.SetActive(false);
        Destroy(thisob);
    }


        Bounds getParentBound(GameObject ob)
        {
            var b = new Bounds(ob.transform.position, Vector3.zero);
            foreach (Collider r in ob.GetComponentsInChildren<Collider>())
            {
                b.Encapsulate(r.bounds);
            }
            return b;
        }
    }
