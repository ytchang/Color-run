using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level1Tutorial : MonoBehaviour {
    //public Canvas dialog;
    Text dialogTxt;
    float timeCount;
    public float holdTime = 1.2f;
    public Sprite terrainTextHighlight; 
    int Index = 1;
	// Use this for initialization
	void Start () {
        CueLevels.dialog.SetActive(true);
        dialogTxt = CueLevels.dialog.GetComponentInChildren<Text>();
        dialogTxt.text = "Press Space to jump";
        Index = 1;
        if (CueLevels.retrying == true) { dialogTxt.text = "Jump and start!"; Index = 4; }
	}
	
	// Update is called once per frame
	void Update () {
        if (Index==1&&Input.GetKeyUp("space"))
        {
            dialogTxt.text = "Hold space to jump higher";
            Index = 2;
        }

        if (Index==2&&Input.GetKeyDown("space"))
        {
            timeCount = 0;
            Index = 3;
            
        }
        if(Index==3) timeCount += Time.deltaTime;
        if (Input.GetKeyUp("space")&&timeCount >= holdTime&&Index==3)
        {
            dialogTxt.text = "Jump and start!";
            Index = 4;
        }else if (Input.GetKeyUp("space") && timeCount < holdTime && Index == 3)
        {
            Index = 2;
        }
        if (Input.GetKeyDown("space")&&Index==4)
        {
            CueLevels.rotateTerrain = true;
            CueLevels.BGMFadeOut = false;
            CueLevels.BGM.Play();
            CueLevels.BGM.volume = CueLevels.BGMVolume;
            CueLevels.dialog.SetActive(false);
            //CueLevels.terrainText.GetComponent<SpriteRenderer>().sprite = terrainTextHighlight;
            Destroy(this.gameObject);
        }
    }
}
