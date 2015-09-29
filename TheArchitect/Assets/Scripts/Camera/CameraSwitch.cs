using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    StandardCamera standard;
    TopDownCamera topDown;
    RTSCamera rts;

    void Start()
    {
        standard = GetComponent<StandardCamera>();
        topDown = GetComponent<TopDownCamera>();
        rts = GetComponent<RTSCamera>();

        //SwitchToStandard();
    }

    public void SwitchToStandard(Transform onTarget)
    {
        standard.enabled = true;
        standard.Initialize(onTarget);
        rts.enabled = false;
    }

    public void SwitchToRTS(Vector3 atPosition)
    {
        standard.enabled = false;
        transform.position = atPosition;
        rts.enabled = true;

    }
}
