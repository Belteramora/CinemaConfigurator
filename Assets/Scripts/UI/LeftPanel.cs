using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanel : MonoBehaviour
{
    private int currentScreenId;

    public int defaulScreen;
    public float showTime;
    public Button leftButton;
    public Button rightButton;
    public List<CanvasGroup> screens;

    private void Start()
    {
        currentScreenId = 0;
        leftButton.interactable = false;
    }

    public void GoRight()
    {
        var previousId = currentScreenId;
        currentScreenId++;

        if(currentScreenId + 1 >= screens.Count)
        {
            rightButton.interactable = false;
        }

        screens[previousId].Hide(showTime);
        screens[currentScreenId].Show(showTime);

        leftButton.interactable = true;
    }

    public void GoLeft()
    {
        var previousId = currentScreenId;
        currentScreenId--;

        if (currentScreenId - 1 < 0)
        {
            leftButton.interactable = false;
        }

        screens[previousId].Hide(showTime);
        screens[currentScreenId].Show(showTime);

        rightButton.interactable = true;
    }
}
