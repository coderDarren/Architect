using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFader : MonoBehaviour {

    public bool faded = false; //This will be used to determine if the next button can fade in yet

    Image buttonImage; //to change the color of the button image
    Text txt; //to change the color of the text 
    Color buttonColor;
    Color textColor;
    public bool startFade = false; //to determine if the button should start fading in to the screen
    float smooth = 0; //how fast the button fades in
    bool initialized = false; //whether the proper values have been initialized yet

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        startFade = false;
        faded = false;
        buttonImage = GetComponent<Image>();
        buttonColor = buttonImage.color;
        GetComponent<Button>().interactable = false;
        if (GetComponentInChildren<Text>())
        {
            txt = GetComponentInChildren<Text>();
            textColor = txt.color;
        }
        initialized = true;
    }

    void Update()
    {
        if (startFade)
        {
            Fade(smooth);
            if (buttonColor.a > 0.9)
                faded = true;
        }
    }

    public void Fade(float rate)
    {
        //make sure the proper values have been initialized
        if (!initialized)
            Initialize();
        //set the fade speed
        smooth = rate;
        startFade = true;

        //increase the alpha component of the colors

        //button image color
        buttonColor.a += rate;
        buttonImage.color = buttonColor;
        //text color
        if (txt)
        {
            textColor.a += rate;
            txt.color = textColor;
        }
        if (faded)
            GetComponent<Button>().interactable = true;
    }
}
