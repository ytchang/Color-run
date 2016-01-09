using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HeavenTutorial : MonoBehaviour
{
    Text dialogTxt;
    // float timeCount;
    //public float holdTime = 1.2f;
    //public Sprite terrainTextHighlight;
    int index = 0;
    // Use this for initialization
    void Start()
    {
        CueLevels.dialog.SetActive(true);
        dialogTxt = CueLevels.dialog.GetComponentInChildren<Text>();
        dialogTxt.text = "Here you can both jump and put bricks.";
        index = 0;
        if (CueLevels.retrying == true) { dialogTxt.text = "Click to start!"; index = 3; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            if (index == 0)
            {
                dialogTxt.text = "The goal is to climb as high as you can.";
                index = 1;
            }
            else if (index == 1)
            {
                dialogTxt.text = "This level ends once you fall on the ground.";
                index = 2;
            }
            else if (index == 2)
            {
                dialogTxt.text = "Click to start!";
                index = 3;
            }
            else if (index == 3)
            {
                CueLevels.brickOn = true;
                Player.controlOn = true;
                Player.AIOn = false;
                CueLevels.rotateTerrain = true;
                CueLevels.BGMFadeOut = false;
                CueLevels.BGM.Play();
                CueLevels.BGM.volume = CueLevels.BGMVolume;
                CueLevels.dialog.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
