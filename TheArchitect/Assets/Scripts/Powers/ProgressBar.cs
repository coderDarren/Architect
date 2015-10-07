using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    RectTransform rt;
    Vector3 scale;
    Vector3 initialScale;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        scale = rt.localScale;
        initialScale = scale;
    }

    public void UpdateBar(float max, float current)
    {
        scale.x = (initialScale.x * current) / max;

        if (scale.x > initialScale.x)
            scale.x = initialScale.x;
        if (scale.x < 0)
            scale.x = 0;

        rt.localScale = scale;
    }

}
