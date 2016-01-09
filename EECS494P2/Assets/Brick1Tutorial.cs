using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Brick1Tutorial : MonoBehaviour {
    Text dialogTxt;
    //    float timeCount;
    //  public float holdTime = 1.2f;
    int Index;
    // Use this for initialization
    void Start () {
        
        CueLevels.dialog.SetActive(true);
        dialogTxt = CueLevels.dialog.GetComponentInChildren<Text>();
        //dialogTxt.text = "This time Colory will jump on it's own. You can click on screen to put bricks";
        dialogTxt.text = "This time, Color Bug will jump on it's own";
        Index = 1;
        if (CueLevels.retrying == true)
        {
            Index = 3;
            dialogTxt.text = "Put a brick and start!";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Index == 1)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space")) {
                dialogTxt.text = "You can click on screen to put bricks";
                Index = 2;
            }
        }
        else if (Index == 2)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space"))
            {
                dialogTxt.text = "Put a brick and start!";
                Index = 3;
                CueLevels.brickOn = true;
            }
            

        }
        else if (Index==3)
        {
            if (Input.GetMouseButtonDown(0)) {
                CueLevels.BGMFadeOut = false;
                CueLevels.BGM.Play();
                CueLevels.BGM.volume = CueLevels.BGMVolume;
                CueLevels.rotateTerrain = true;
                CueLevels.dialog.SetActive(false);
                CueLevels.mode = 2;
                Destroy(this.gameObject);
            }
        }
	}
}
