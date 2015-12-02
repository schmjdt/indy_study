using UnityEngine;
using System.Collections;

public class ObjectPublic : MonoBehaviour {

    public Color publicColor;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = publicColor;
	}

    public void objSetColor(Color c)
    {
        publicColor = c;
    }
}
