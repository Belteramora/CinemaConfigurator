using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public float fadeTime;

    public int defaultScreenId;

    public List<CanvasGroup> screens;

    [HideInInspector]
    public CanvasGroup currentScreen;


    private void Start()
    {
        currentScreen = screens[defaultScreenId];
        currentScreen.Show(fadeTime);
    }

    public void ChangeScreen(int screenId)
    {
        currentScreen.Hide(fadeTime);

        screens[screenId].Show(fadeTime);

        currentScreen = screens[screenId];
    }
}
