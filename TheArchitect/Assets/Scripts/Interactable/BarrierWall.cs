using UnityEngine;
using System.Collections;


public class BarrierWall : MonoBehaviour {

    public Vector3 downPosition = Vector3.zero;

    BarrierButton[] buttons;
    int numButtons = 0;
    int numButtonsPushed = 0;
    bool allButtonsPushed = false;
    bool opened = false;

    void Start()
    {
        buttons = GetComponentsInChildren<BarrierButton>();
        numButtons = buttons.Length;
    }

    void Update()
    {
        if (allButtonsPushed && !opened)
        {
            if (Vector3.Distance(transform.position, downPosition) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, downPosition, 10 * Time.deltaTime);
            }
            else
                opened = true;
        }
    }

    public void PushButton()
    {
        numButtonsPushed++;
        if (numButtonsPushed == numButtons)
        {
            allButtonsPushed = true;
        }
    }
}
