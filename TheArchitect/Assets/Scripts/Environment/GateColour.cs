using UnityEngine;
using System.Collections;

public class GateColour : MonoBehaviour {

    private System.Random rand;
	// Use this for initialization
	void Start () {
        rand = new System.Random();
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", new Color(rand.Next(0,2), rand.Next(0,2), rand.Next(0,2), 1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
