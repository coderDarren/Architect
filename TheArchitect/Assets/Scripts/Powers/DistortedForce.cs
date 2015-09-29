using UnityEngine;
using System.Collections;

public class DistortedForce : MonoBehaviour {

    public float expandRate = 0.25f;
    public Size size;

    [System.Serializable]
    public struct Size { public float min; public float max;}

    float currScale = 0;

    void Start()
    {
        currScale = size.min;
        transform.localScale = new Vector3(currScale, currScale, currScale);
    }

    void Update()
    {
        currScale += expandRate;

        if (currScale > size.max)
            GameObject.Destroy(gameObject);

        transform.localScale = new Vector3(currScale, currScale, currScale);
    }


}
