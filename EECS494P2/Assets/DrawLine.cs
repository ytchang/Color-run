using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {
    LineRenderer lineRenderer;
    public static Material lineColor;
    GameObject[] dots;
    float lineSpd=0.2f;
    float reloadTime;
    int i = 0;
    public static bool lineOver=false;
    // Use this for initialization
    void Start () {
     //   this.transform.position = new Vector3(1000000, 20, 30000);
        lineRenderer = this.GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetColors(c1, c2);
        //lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.material= lineColor;
        dots = GameObject.FindGameObjectsWithTag("Dot");

        lineOver = false;
        if (CueLevels.campaign == false) lineSpd = 0;
        else lineSpd = 0.2f;
        reloadTime = 0;
        i = 0;
    }
	
	// Update is called once per frame
	void Update () {
        reloadTime += Time.deltaTime;
        if (reloadTime >= lineSpd&&i<=dots.Length)
        {
            reloadTime = 0;
            lineRenderer.SetVertexCount(i + 1);
            if (i < dots.Length) lineRenderer.SetPosition(i, new Vector3( dots[i].transform.position.x,dots[i].transform.position.y,-5));
            if(i==dots.Length) lineRenderer.SetPosition(i, new Vector3(dots[0].transform.position.x, dots[0].transform.position.y, -5));
            i++;
        }
        if (i > dots.Length)
        {
            lineOver = true;

        }
    }
}
