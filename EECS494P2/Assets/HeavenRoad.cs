using UnityEngine;
using System.Collections;

public class HeavenRoad : MonoBehaviour {
    //public GameObject terrrainPrefab;
    public GameObject endPrefab;
    public GameObject tutorialPrefab;
    public GameObject BackgroundPrefab;
    public Sprite groundSprite;
    public Material color;
    GameObject terrain;
   // GameObject player;
    int counter;
    bool preAir;
    // Use this for initialization
    void Start () {
        terrain = GameObject.Find("terrain");
        //player = GameObject.Find("Player");
        CueLevels.cueLevelScript.ClearMap();
        CueLevels.setCamMode = 1;
        DrawLine.lineColor = color;
        Player.color = color;
        GameObject.Find("Ground").GetComponent<SpriteRenderer>().sprite = groundSprite;
        CueLevels.brickOn = false;
        Player.controlOn = false;
        //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 7, player.transform.position.z);
        GameObject backGround=Instantiate(BackgroundPrefab);
        //backGround.transform.parent = terrain.transform;
        Instantiate(tutorialPrefab);
        counter = 0;
        preAir = true;
    }

    // Update is called once per frame
    void Update() {
        //print(Player.air);
        if (preAir==true&&Player.air==false) {
            print(Player.nowStanding.tag);
            if (counter == 0 && Player.nowStanding.tag == "Ground") { counter = 1; }
            else if (counter == 1 && Player.nowStanding.tag == "Ground")
            {
                Instantiate(endPrefab);
                Destroy(this.gameObject);
            }
        }
        preAir = Player.air;
    }
}
