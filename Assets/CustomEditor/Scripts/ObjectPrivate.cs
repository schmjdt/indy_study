using UnityEngine;
using System.Collections;

public class ObjectPrivate : MonoBehaviour {

    [SerializeField]
    private Color privateColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Renderer>().material.color = privateColor;
    }

    public void objSetColor(Color c)
    {
        privateColor = c;
    }

    public Color getColor() { return privateColor; }
}
