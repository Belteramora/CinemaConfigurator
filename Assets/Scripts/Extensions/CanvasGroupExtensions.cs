using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class CanvasGroupExtensions
{
    public static void Hide(this CanvasGroup canvasGroup, float animationTime)
    {
        canvasGroup.DOFade(0, animationTime).OnStart(() => 
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    public static void Show(this CanvasGroup canvasGroup, float animationTime)
    {
        canvasGroup.DOFade(1f, animationTime).OnStart(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    
}
