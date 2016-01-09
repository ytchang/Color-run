using UnityEngine;
using System.Collections;

public class BrickBehavior : MonoBehaviour {
    public Sprite highlight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void anime()
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<SpriteRenderer>().sprite = highlight;

    }

}
