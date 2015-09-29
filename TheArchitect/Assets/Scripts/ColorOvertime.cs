using UnityEngine;
using System.Collections;

public class ColorOvertime : MonoBehaviour {

    public int materialIndex = 1; //light mat
    [System.Serializable]
    public struct Smooth { public float min; public float max; }
    public Smooth smooth;
    public bool inOrder = false;
    public Color[] colors;

    Material mat;
    Color matColor;
    Color toColor;
    int colorIndex = 0;
    float _colorSmooth;
    float colorSmooth;

    void Start()
    {
        colorSmooth = Random.Range(smooth.min, smooth.max);
        _colorSmooth = colorSmooth;
        mat = GetComponent<Renderer>().materials[materialIndex];
        toColor = mat.GetColor("_EmissionColor");
    }

    void Update()
    {
        if (ColorsMatch(colors[colorIndex]))
        {
            GetNextColor();
        }

        mat.SetColor("_EmissionColor", toColor);
        mat.SetColor("_Color", toColor);
    }

    void GetNextColor()
    {
        if (inOrder)
        {
            if (colorIndex < colors.Length - 1)
                colorIndex++;
            else
                colorIndex = 0;
        }
        else
        {
            colorIndex = Random.Range(0, colors.Length - 1);
        }
    }

    bool ColorsMatch(Color c)
    {
        matColor = mat.GetColor("_EmissionColor");
        toColor.r = Mathf.Lerp(matColor.r, c.r, _colorSmooth * Time.deltaTime);
        toColor.g = Mathf.Lerp(matColor.g, c.g, _colorSmooth * Time.deltaTime);
        toColor.b = Mathf.Lerp(matColor.b, c.b, _colorSmooth * Time.deltaTime);
        toColor.a = mat.GetColor("_Color").a;

        mat.SetColor("_EmissionColor", toColor);
        mat.SetColor("_Color", toColor);
        
        if (Mathf.Abs(matColor.r - c.r) < 0.1f &&
            Mathf.Abs(matColor.g - c.g) < 0.1f &&
            Mathf.Abs(matColor.b - c.b) < 0.1f)
            return true;
        return false;
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void AlterSpeed(float speedFactor)
    {
        _colorSmooth = (colorSmooth * speedFactor);
    }

}
