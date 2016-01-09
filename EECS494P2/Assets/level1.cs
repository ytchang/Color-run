using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level1 : MonoBehaviour {
    public GameObject EndLevelPrefab;
    public GameObject terrrainPrefab;
    public GameObject tutorialPrefab;
    public Sprite groundSprite;
    public Material color;

    GameObject terrain;
    GameObject player;
    Transform terrainTrans;
    GameObject[] dots;
    GameObject[] bricks;
    GameObject[] lines;
   // GameObject textParent;

    // Use this for initialization
    void Start () {
        terrain = GameObject.Find("terrain");
        terrainTrans = terrain.transform;
        //CueLevels.setCamMode = 1;
        //dots = GameObject.FindGameObjectsWithTag("Dot");
        //bricks = GameObject.FindGameObjectsWithTag("Brick");
        //lines = GameObject.FindGameObjectsWithTag("Line");
        GameObject.Find("Ground").GetComponent<SpriteRenderer>().sprite = groundSprite;
        CueLevels.score.SetActive(true);
        CueLevels.cueLevelScript.ClearMap();
        CueLevels.score.GetComponentInChildren<Text>().text = 0.ToString();
        Player.color = color;
        Instantiate(tutorialPrefab);
        DrawLine.lineColor = color;
        CueLevels.mode = 1;
        GameObject levelTerrain= Instantiate(terrrainPrefab);
        levelTerrain.transform.parent = terrain.transform;
    }
	
	// Update is called once per frame
	void Update () {
        //Player.dot.GetComponent<MeshRenderer>().materials[0] = color;
        //full round
        if (terrainTrans.eulerAngles.z > 359)
        {
           // CueLevels.mode = 0;
            Instantiate(EndLevelPrefab);
            Destroy(this.gameObject);         
        }

    }
}
