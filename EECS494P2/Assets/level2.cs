using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level2 : MonoBehaviour {
    public GameObject EndLevelPrefab;
    public GameObject tutorialPrefab;
    public GameObject dialogPrefab;
    public GameObject PatternLinePrefab;
    public GameObject PreBrickPrefab;
    public Sprite groundSprite;
    public Material color;


    public static level2 level2Script;

    GameObject terrain;



    GameObject textOnTerrain;
    Transform terrainTrans;

    //Transform camTrans;
    //Transform playerTrans;
    ////Vector3 relPos;
    ////Vector3 initialCamPos;
    //Camera cam;
    //float camH, camW;
    //Vector3 camCenter;
    //Bounds b;

    // Use this for initialization
    void Start () {
        DrawLine.lineColor = color;
        Player.color = color;
        level2Script = this;
        
        terrain = GameObject.Find("terrain");
        terrainTrans = terrain.transform;
        Player.controlOn = false;
        CueLevels.score.SetActive(false);
        //  textOnTerrain = GameObject.Find("TutorialOnTerrain(Clone)");
        //if(textOnTerrain!=null)Destroy(textOnTerrain);
        if (CueLevels.retrying == false) Instantiate(dialogPrefab);
        else Instantiate(PreBrickPrefab);
        
    }
	
	// Update is called once per frame
	void Update () {
        //Player.dot.GetComponent<MeshRenderer>().materials[0] = color;
        if (terrainTrans.eulerAngles.z > 359)
        {
            // CueLevels.mode = 0;
            //CueLevels.unlocked = 1;
            //PlayerPrefs.SetInt("ApplePickerHighScore", CueLevels.unlocked);
            CueLevels.heavenRoad.GetComponent<Button>().interactable = true;
            Instantiate(EndLevelPrefab);
            Destroy(this.gameObject);
        }
    }


   
    
}
