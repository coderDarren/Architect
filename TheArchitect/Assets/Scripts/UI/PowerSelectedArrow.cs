using UnityEngine;
using System.Collections;

public class PowerSelectedArrow : MonoBehaviour {

    public float oscillateDist = 2;

    Vector3 initialPos = Vector3.zero;
    Vector3 currentPos = Vector3.zero;
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        initialPos = rt.position;
        currentPos = initialPos;
    }

    void Update()
    {
        initialPos.y = -60;
        currentPos.y = Mathf.PingPong(Time.time * 4, oscillateDist) + initialPos.y;
        currentPos.x = 0;
        rt.localPosition = currentPos;
    }
}
