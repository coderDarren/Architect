using UnityEngine;
using System.Collections;

public class CanvasLoader : MonoBehaviour {

    public GameObject UseCanvas;

    void Start()
    {
        GameObject go = Instantiate(UseCanvas) as GameObject;
    }
}
