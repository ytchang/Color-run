using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CueLevels : MonoBehaviour {
    // Camera cam;
    // Transform camTran;
    public GameObject brickPrefab;
    GameObject terrain;
    GameObject player;
    Transform terrainTrans;
    Transform camTrans;
    Transform playerTrans;
    //Player playerScript;
    public float omega=5;
    Vector3 relPos;
    Vector3 initialCamPos;
    public Material color;
    Camera cam;

    

    public float mode1Size;
    public float mode2Size;
    public static int mode;
    public static bool brickOn = false;
    public static int setCamMode;
    public static bool rotateTerrain=false;

    public GameObject EndLevelPrefab;
    public GameObject Level1Prefab;

    public float brickReload;
    float reload=0;

    public static GameObject currLevel;
    public static GameObject nextLevel;
    public static int levelIndex;
    public GameObject[] levelList;
    public GameObject heavenRoadLevel;
    public static CueLevels cueLevelScript;


    public static GameObject menu;
    public static GameObject heavenRoad;
    public static GameObject menu2;
    public static GameObject dialog;
    public static GameObject score;
    public static AudioSource BGM;
    public static GameObject terrainText;
    public static GameObject AlienLeft;
    public static GameObject AlienRight;
    public static bool BGMFadeOut;
    public static float BGMVolume;
    public static bool retrying;

    public static bool campaign;
    //public static int unlocked;

    GameObject[] dots;
    GameObject[] bricks;
    GameObject[] lines;
    GameObject[] others;


    public Texture2D cursorTextureSmall;
    public Texture2D cursorTextureMedium;
    public CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot;

    // Use this for initialization
    void Start () {

        

        cueLevelScript = this;
        camTrans = this.GetComponent<Transform>();
        terrain = GameObject.Find("terrain");
        terrainTrans = terrain.transform;
        player = GameObject.Find("Player");
        playerTrans = player.transform;
        relPos = new Vector3(camTrans.position.x - playerTrans.position.x,
            camTrans.position.y - playerTrans.position.y, 0);
        initialCamPos = camTrans.position;
        menu = GameObject.Find("MainMenu");
        heavenRoad = GameObject.Find("HeavenRoadButton");
        dialog = GameObject.Find("Dialog");
        menu2 = GameObject.Find("Menu2");
        score = GameObject.Find("Score");
        BGM = this.GetComponent<AudioSource>();
        BGMVolume = BGM.volume;
        terrainText = GameObject.Find("terrainText");
        AlienLeft = GameObject.Find("AlienLeft");
        AlienRight = GameObject.Find("AlienRight");
        AlienLeft.SetActive(false);
        AlienRight.SetActive(false);
        dialog.SetActive(false);
        menu2.SetActive(false);
        score.SetActive(false);
      // if(unlocked==0) heavenRoad.GetComponent<Button>().interactable = false;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        //playerScript = player.GetComponent<Player>();
        DrawLine.lineColor = color;
        Player.color = color;
        levelIndex = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (campaign == true) currLevel = levelList[levelIndex - 1];
        else currLevel = heavenRoadLevel;
        if (levelList.Length > levelIndex) nextLevel = levelList[levelIndex];
        if (setCamMode == 1)
        {
            cam.orthographicSize = mode1Size;
            camTrans.position = initialCamPos;
            if (playerTrans.position.y > 2) camTrans.position = new Vector3(player.transform.position.x + relPos.x, player.transform.position.y - 2, camTrans.position.z);
            else camTrans.position = initialCamPos;
        }
        if (setCamMode == 2)
        {
            cam.orthographicSize = mode2Size;
            camTrans.position = initialCamPos;
            if (playerTrans.position.y > mode2Size-2) camTrans.position = new Vector3(player.transform.position.x + relPos.x, player.transform.position.y - (mode2Size - 2), camTrans.position.z);
            else camTrans.position = initialCamPos;
        }

        if(rotateTerrain==true) terrainTrans.Rotate(new Vector3(0, 0, omega * Time.deltaTime));
        if (mode == 1)
        {
            setCamMode = 1;
            Player.AIOn = false;
            Player.controlOn = true;
            //rotate terrain
            //terrainTrans.Rotate(new Vector3(0, 0, omega * Time.deltaTime));
        }
        if (mode == 2)
        {
            setCamMode = 2;
            Player.AIOn = true;
            Player.controlOn = false;
            brickOn = true;
            //rotate terrain
            //terrainTrans.Rotate(new Vector3(0, 0, omega * Time.deltaTime));
        }

        //rotate camera
        //camTrans.RotateAround(new Vector3(0, -18, -10), new Vector3(0, 0, 1), omega * Time.deltaTime);

        //put bricks
        if (brickOn == true) {
            reload += Time.deltaTime;
            if (setCamMode == 2)
            {
                hotSpot = new Vector2(cursorTextureSmall.height / 2, cursorTextureSmall.width / 2);
                Cursor.SetCursor(cursorTextureSmall, hotSpot, cursorMode);
            }else if (setCamMode == 1)
            {
                hotSpot = new Vector2(cursorTextureMedium.height / 2, cursorTextureMedium.width / 2);
                Cursor.SetCursor(cursorTextureMedium, hotSpot, cursorMode);
            }

            if (Input.GetMouseButtonDown(0) && reload>=brickReload)
            {
                var v3 = Input.mousePosition;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                v3.z = 0;
                GameObject brick = Instantiate(brickPrefab);
                brick.transform.position = v3;
                brick.transform.parent = terrain.transform;
                reload = 0;
            }
        }
        else
        {
            Cursor.SetCursor(null, new Vector2(0, 0), cursorMode);
        }

        if (BGMFadeOut == true)
        {
            BGM.volume -= 0.05f * Time.deltaTime;
            if (BGM.volume - 0.05f * Time.deltaTime <= 0) BGMFadeOut = false;
        }
        
       
    }

    public void cueCampaign()
    {
        campaign = true;
        menu.SetActive(false);
        levelIndex = 1;
        retrying = false;
        currLevel = Instantiate(levelList[levelIndex - 1]);
        
    }
    public void cueHeavenRoad()
    {
        campaign = false;
        menu.SetActive(false);
        levelIndex = 0;
        retrying = false;
        currLevel = Instantiate(heavenRoadLevel);
    }
    public void ClearMap()
    {
        dots = GameObject.FindGameObjectsWithTag("Dot");
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        lines = GameObject.FindGameObjectsWithTag("Line");
        others = GameObject.FindGameObjectsWithTag("Other");
        for (int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i]);
        }
        for (int i = 0; i < bricks.Length; i++)
        {
            Destroy(bricks[i]);
        }
        for (int i = 0; i < lines.Length; i++)
        {
            Destroy(lines[i]);
        }
        for (int i = 0; i < others.Length; i++)
        {
            Destroy(others[i]);
        }
    }
}
