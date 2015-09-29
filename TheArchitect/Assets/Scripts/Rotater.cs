using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public Vector3 rotateAxis = new Vector3(0, 1, 0);
    public float rotateSmooth = 5f;
    public bool parentable = false;

    float _rotateSmooth = 0;

    void Start()
    {
        _rotateSmooth = rotateSmooth;
    }

    void FixedUpdate()
    {
        transform.rotation *= Quaternion.AngleAxis(_rotateSmooth, rotateAxis);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            if (parentable)
                col.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            if (parentable)
            {
                col.transform.parent = null;
                col.transform.localScale = col.transform.GetComponent<CharacterMovement>().playerScale;
            }
        }
    }
}
