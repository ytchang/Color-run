using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour {

    static public bool AIOn;
    static public bool controlOn;

    public float initialSpeed;
    public float speedDecrease;
    public float gravity;
    bool startSlow=false;

    public int rayCastNum=8;
    public float rayRadius;
    Vector3[] rayDir;
    RaycastHit rayHit;
    float rayLen;
    public static GameObject nowStanding;

    public float dotReload;
    public GameObject dotPrefab;
    float groundTime;
    public static Material color;
    
    GameObject player;
    GameObject terrain;
    GameObject cam;
    GameObject ground;
    GameObject dot;
    float omega;
    Rigidbody playerRigid;
    float playerH;

    //float holdTime=0;
   
    public static bool air=true;
    bool preAir;
    bool findAir=true;

    public float raydarRadius;
    public int raydarNum;
    Vector3[] raydarDir;
    RaycastHit raydarHit;
    Collider nearestCol;
    float minX;
    CueLevels camScript;
    bool detectBrick=false;
    public float releaseRate;
    public float jumpRate;




    // Use this for initialization
    void Start () {
        AIOn = true;
        controlOn = true;

        player = this.gameObject;
        playerRigid = this.GetComponent<Rigidbody>();
        playerRigid.velocity = new Vector3(0, 0, 0);
        playerH = player.transform.lossyScale.y;
        terrain = GameObject.Find("terrain");
        ground = GameObject.Find("Ground");
        cam = GameObject.Find("Main Camera");
        camScript = cam.GetComponent<CueLevels>();
        omega = camScript.omega*Mathf.Deg2Rad;

        Physics.gravity = new Vector3(0, -gravity, 0);
        
        rayDir = new Vector3[rayCastNum];
        for (int i = 0; i < rayCastNum; i++)
        {
            rayDir[i] = new Vector3(Mathf.Cos(Mathf.PI / (rayCastNum-1) * i+Mathf.PI), Mathf.Sin(Mathf.PI / (rayCastNum - 1) * i + Mathf.PI), 0);

        }
        rayLen = playerH * (0.5f + rayRadius);
        raydarDir = new Vector3[raydarNum];
        for (int i = 0; i < raydarNum; i++)
        {
            raydarDir[i] = new Vector3(Mathf.Cos(2 * Mathf.PI / (raydarNum-1) * i/2-Mathf.PI/2), Mathf.Sin(2 * Mathf.PI / (raydarNum-1) * i/2-Mathf.PI/2), 0);
        }
        

        preAir = air;
    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<MeshRenderer>().material = color;
        dotPrefab.GetComponent<MeshRenderer>().material = color;
        findAir = true;
        Vector3 pos = transform.position;
        for(int i = 0; i < rayCastNum; i++)
        {
            Debug.DrawRay(pos, rayDir[i]* rayLen, Color.green);
            if (Physics.Raycast(pos, rayDir[i],out rayHit, rayLen))
            {
                findAir = false;
                nowStanding = rayHit.collider.gameObject;
            }
        }
        air = findAir;
        //if (air) print("air");
        //else
        //{
        //   // playerRigid.velocity = new Vector3(0, 0, 0);
        //    print("ground");
        //}

        if (preAir ==true && air == false)
        {
            if (nowStanding.tag == "Brick")
            {
                nowStanding.GetComponent<BrickBehavior>().anime();
                if (CueLevels.score.activeSelf == true)
                {
                    int score = int.Parse(CueLevels.score.GetComponentInChildren<Text>().text);
                    score += 100;
                    CueLevels.score.GetComponentInChildren<Text>().text = score.ToString();

                }
            }
            //playerRigid.velocity = new Vector3(0, 0, 0);
            groundTime = 0;
            dot = Instantiate(dotPrefab);
            dot.transform.position = rayHit.point;
            dot.transform.parent = terrain.transform;
            
        }
        else if (preAir == false && air == false)
        {
            groundTime += Time.deltaTime;
            if (groundTime >= dotReload&&CueLevels.rotateTerrain==true)
            {
                GameObject dot = Instantiate(dotPrefab);
                dot.transform.position = rayHit.point;
                dot.transform.parent = terrain.transform;
                groundTime = 0;
            }
        }

        if (controlOn == true)
        {
            //Control
                    //if (Input.GetKey("space"))
                    //{
                    //    //holdTime += Time.deltaTime;
                    //}
                    if (Input.GetKeyDown("space"))
                    {
                         if(air==false) playerRigid.velocity = new Vector3(0, initialSpeed, 0);
                        this.GetComponent<AudioSource>().Play();
                    }
                    if (Input.GetKeyUp("space"))
                    {
                        startSlow = true;
                        //holdTime = 0;
                    }

        }
                    if (startSlow == true)
                    {
                        playerRigid.velocity = new Vector3(0, playerRigid.velocity.y- speedDecrease * Time.deltaTime, 0);
                        if (playerRigid.velocity.y <= 0) startSlow = false;
                    }



        if (AIOn == true)
        {
        //AI
                detectBrick = false;
                minX = raydarRadius;
                //if (air==true) { 
                    for (int i = 0; i < raydarNum; i++)
                    {
                        Debug.DrawRay(pos, raydarDir[i] * raydarRadius, Color.red);
                        if (Physics.Raycast(pos, raydarDir[i], out raydarHit, raydarRadius))
                        {
                            if (minX > raydarHit.collider.transform.position.x-pos.x&& raydarHit.collider.gameObject != nowStanding&&raydarHit.collider.gameObject!=ground)
                            {
                                minX = raydarHit.collider.transform.position.x-pos.x;
                                nearestCol = raydarHit.collider;
                                    detectBrick = true;
                            }
                        }
                    }

                if (detectBrick == true) {
                    //print("brick:" + nearestCol.gameObject+" g:"+nowStanding);
                    float brickHeight = nearestCol.transform.position.y - terrain.transform.position.y;
                    float playerHeight = pos.y - terrain.transform.position.y;
                    float reachTime = (nearestCol.transform.position.x - pos.x) / brickHeight / omega;
                    float height = nearestCol.transform.position.y - pos.y;

                    if (air)
                    {
                        if (playerRigid.velocity.y > 0)
                        {

                            if (gravity * reachTime*0.5*releaseRate <= -height && height < 0)
                            {
                                startSlow = true;
                                //print("height:" + height + " brick y:" + nearestCol.transform.position.y + "player y:" + pos.y);
                            }
                        }
                    }
                    else
                    {
                        if (reachTime <= 2 * initialSpeed / gravity*jumpRate)
                        {
                        if (reachTime * gravity / 2 < -height && height < 0)
                        {

                        }
                        // if (nowStanding.tag == "Brick")
                        else {

                            playerRigid.velocity = new Vector3(0, initialSpeed, 0);
                            this.GetComponent<AudioSource>().Play();

                        }
                        }
                    }

                }
        }

        

        




        preAir = air;
    }

    //void onTriggerEnter(Collider col)
    //{
    //    air = false;
    //    print("triEnter");


    //}
    //void onTriggerLeave(Collider col)
    //{
    //    air = true;
    //    print("triLeave");
    //}






    void test()
    {
        //if (Physics.SphereCast(ray, transform.lossyScale.y * 0.51f) == true)
        //{
        //    print("pos:" + transform.position +
        //        "sphere:" + Physics.SphereCast(ray, transform.lossyScale.y * 0.51f) +
        //        "y:" + transform.lossyScale.y);

        //}
        //else { print("pos:" + transform.position + "leave:" + Physics.SphereCast(ray, transform.lossyScale.y * 0.51f)); }
        //Physics.Raycast(transform.position, new Vector3(1, 0, 0));

    }
}
