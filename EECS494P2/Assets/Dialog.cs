using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
    int index = 0;
    Text dialogText;
    public Sprite ALeft;
    public Sprite ARight;
    public Sprite ALeftDark;
    public Sprite ARightDark;
	// Use this for initialization
	void Start () {
        index = 0;
        CueLevels.dialog.SetActive(true);
        dialogText = CueLevels.dialog.GetComponentInChildren<Text>();
        CueLevels.AlienLeft.SetActive(true);
        CueLevels.AlienRight.SetActive(true);
        CueLevels.AlienLeft.GetComponent<Image>().sprite = ALeftDark;
        dialogText.text = "'Wow, that's a nice sun.'";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            if (index == 0)
            {
                CueLevels.AlienLeft.GetComponent<Image>().sprite = ALeft;
                CueLevels.AlienRight.GetComponent<Image>().sprite = ARightDark;
                dialogText.text = "'It's really fun to draw with a color bug.'";
                index = 1;
            }
            else if (index == 1)
            {
                dialogText.text = "'Just put number on bricks then they will try their best to jump on it.'";
                index = 2;
            }
            else if (index == 2)
            {
                dialogText.text = "'Try it your self!'";
                index = 3;
            }
            else if (index==3)
            {
                CueLevels.AlienLeft.GetComponent<Image>().sprite = ALeftDark;
                CueLevels.AlienRight.GetComponent<Image>().sprite = ARight;
                dialogText.text = "'Sounds fun. Let me draw something...'";
                index = 4;
            }else if (index == 4)
            {

                CueLevels.dialog.SetActive(false);
                Instantiate(level2.level2Script.PreBrickPrefab);
                CueLevels.AlienLeft.SetActive(false);
                CueLevels.AlienRight.SetActive(false);
                Destroy(this.gameObject);
            }
        }
	}
}
