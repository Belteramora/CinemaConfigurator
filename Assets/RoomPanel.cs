using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    private float width;
    private float length;
    private float height;

    public TMP_InputField widthIF;
    public TMP_InputField lengthIF;
    public TMP_InputField heightIF;

    public RoomController controller;
    public TMP_Text creationLabel;
    public TMP_Text roomRedrawButtonText;

    public GameObject displaySetting;
    public TMP_InputField displaySizeIF;

    public void Start()
    {
        if (controller.room == null) 
        {
            creationLabel.text = "Room is not created. Create?";
        }

        controller.SetOnDisplayStatusChangeProvider(OnDisplayStatusChange);
    }

    public void SetWidth(string value)
    {
        
        var temp = ValidateAndParse(value);

        if (temp > 0)
            width = temp;
        else
            widthIF.text = width.ToString();

    }

    public void SetLength(string value)
    {

        var temp = ValidateAndParse(value);

        if (temp > 0)
            length = temp;
        else
            lengthIF.text = length.ToString();


    }

    public void SetHeight(string value)
    {

        var temp = ValidateAndParse(value);

        if (temp > 0)
            height = temp;
        else
            heightIF.text = height.ToString();


    }

    public void RedrawRoom()
    {
        controller.RedrawRoom(width, length, height);

        creationLabel.text = string.Empty;
        roomRedrawButtonText.text = "Resize room";
    }

    public float ValidateAndParse(string value)
    {
        if (float.TryParse(value, out float temp))
        {
            if(temp > 0)
                return temp;
        }

        return -1;
    }

    public void OnDisplayStatusChange(bool isAdded)
    {
        if (isAdded)
        {
            displaySizeIF.SetTextWithoutNotify("0");
            displaySetting.SetActive(true);
        }
        else
        {
            displaySetting.SetActive(false);
        }
        
    }

    public void ResizeDisplay(string value)
    {
        var temp = ValidateAndParse(value);

        if(temp > 0 && temp < 20f)
        {
            controller.ResizeDisplay(temp);
        }
    }

    
}
