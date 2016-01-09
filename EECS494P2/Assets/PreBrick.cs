using UnityEngine;
using System.Collections;

public class PreBrick : MonoBehaviour {
    GameObject terrain;
    Transform terrainTrans;
    Transform camTrans;
    Transform playerTrans;
    //GameObject patternLine;
    //Vector3 relPos;
    //Vector3 initialCamPos;
    Camera cam;
    float camH, camW;
    Vector3 camCenter;
    Bounds b;
    public float freezeTime = 2;
    float currTime;
    // Use this for initialization
    void Start () {
        CueLevels.cueLevelScript.ClearMap();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camTrans = cam.GetComponent<Transform>();
        terrain = GameObject.Find("terrain");
        terrainTrans = terrain.transform;
        GameObject.Find("Ground").GetComponent<SpriteRenderer>().sprite = level2.level2Script.groundSprite;
        GameObject pLine=Instantiate(level2.level2Script.PatternLinePrefab);
        pLine.transform.parent = terrainTrans;
        b = getParentBound(terrain);
        cam.transform.position = new Vector3(b.center.x, b.center.y, -10);
        cam.orthographicSize = 2;
        camH = cam.orthographicSize;
        camW = camH * cam.aspect;
        camCenter = cam.transform.position;
        currTime = 0;
    }

    // Update is called once per frame
    void Update () {
        camH = cam.orthographicSize;
        camW = camH * cam.aspect;
        if (b.max.y - camCenter.y > camH * 0.8f
               || b.min.y - camCenter.y < -camH * 0.8f
               || b.max.x - camCenter.x > camW * 0.8f
               || b.min.x - camCenter.x < -camW * 0.8f)
            cam.orthographicSize += b.size.y * 0.8f * Time.deltaTime;
        else
        {
            currTime += Time.deltaTime;

        }

        if (currTime >= freezeTime)
        {
            CueLevels.setCamMode = 2;
            Instantiate(level2.level2Script.tutorialPrefab);
            Destroy(this.gameObject);
        }

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
