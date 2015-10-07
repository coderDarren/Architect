using UnityEngine;
using System.Collections;

public class BarrierButton : MonoBehaviour {

    public float pushStep = 1;

    Vector3 pushedPosition = Vector3.zero;
    BarrierWall wall;
    bool pushed = false;
    Color pushedColor;
    Material mat;

    void Start()
    {
        wall = GetComponentInParent<BarrierWall>();
        pushedPosition = transform.localPosition - Vector3.forward * 0.55f;
        pushedColor = wall.GetComponent<Renderer>().material.color;
        mat = GetComponent<Renderer>().material;
    }

    public void PushButton()
    {
        if (!pushed)
        {
            if (Vector3.Distance(transform.localPosition, pushedPosition) > 0.1f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, pushedPosition, pushStep * Time.deltaTime);
            }
            else
            {
                pushed = true;
                wall.PushButton();
                mat.color = pushedColor;
            }
        }
    }
}
