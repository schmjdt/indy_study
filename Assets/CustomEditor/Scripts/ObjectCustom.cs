using UnityEngine;
using System.Collections;

public class ObjectCustom : MonoBehaviour {

    public string publicName = "default";   // Will display default string field

    public float publicFloatMin = .5f;      // Will display default float field w/ min of 0 (Due to custom inspector)
    public float publicFloatField = .5f;    // Will display default float field

    [Range(0f, 1f)]
    public float publicFloatSlide = .5f;         // Will display a slider (Due to [Range()]

    public float publicFloat = .5f;         // Will display a slider (Due to custom inspector)

    [SerializeField]
    Color privateColor;                     // Will display a color field (Due to [SerializeField])

    bool privateBool;                       // Will be hidden (Due to 'private' by default)
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    
    public void objSetColor(Color c)
    {
        privateColor = c;
    }
}
