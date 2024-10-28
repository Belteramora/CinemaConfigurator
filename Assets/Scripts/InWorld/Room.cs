using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Room : MonoBehaviour
{
    [HideInInspector] public Transform display;
    [HideInInspector] public List<Transform> speakers = new List<Transform>();
    [HideInInspector] public Transform cam;
    [HideInInspector] public float displaySize;

    [HideInInspector] public float width;
    [HideInInspector] public float length;
    [HideInInspector] public float height;

    public float animationTime;

    public Transform displayPoint;

    public Transform[] speakerPoints;
    public Transform[] soundproofPoints;

    [HideInInspector] public int currentSpeakerPointAvailable;
    [HideInInspector] public int currentSoundproofPointAvailable;

    public Transform cameraPoint;

    public Transform leftSide;
    public Transform rightSide;
    public Transform topSide;
    public Transform bottomSide;

    public void Redraw(float width, float length, float height)
    {
        this.width = width;
        this.length = length;
        this.height = height;

        Sequence seq = DOTween.Sequence();

        seq.
            Append(leftSide.DOScale(new Vector3(0.2f, height, length), animationTime)).
            Join(rightSide.DOScale(new Vector3(0.2f, height, length), animationTime)).
            Join(topSide.DOScale(new Vector3(width, height, 0.2f), animationTime)).
            Join(bottomSide.DOScale(new Vector3(width, height, 0.2f), animationTime)).
            Join(leftSide.DOLocalMove(new Vector3(-(width / 2 - 0.1f), height / 2, 0), animationTime)).
            Join(rightSide.DOLocalMove(new Vector3(width / 2 - 0.1f, height / 2, 0), animationTime)).
            Join(topSide.DOLocalMove(new Vector3(0, height / 2, length / 2 - 0.1f), animationTime)).
            Join(bottomSide.DOLocalMove(new Vector3(0, height / 2, -(length / 2 - 0.1f)), animationTime)).
            OnStart(() =>
            {
                if (display != null)
                {
                    display.SetParent(null);
                }

                if (speakers != null && speakers.Count > 0)
                {
                    foreach (var speaker in speakers)
                    {
                        speaker.SetParent(null);
                    }
                }

                if (cam != null)
                {
                    cam.SetParent(null);
                }
            }).
            OnComplete(() =>
            {
                if (display != null)
                {
                    display.SetParent(displayPoint);
                    display.localPosition = Vector3.zero;
                }

                if (speakers != null && speakers.Count > 0)
                {
                    for(int i = 0; i < speakers.Count; i++)
                    {
                        speakers[i].SetParent(speakerPoints[i]);
                        speakers[i].localPosition = Vector3.zero;
                    }
                }

                if (cam != null)
                {
                    cam.SetParent(cameraPoint);
                    cam.localPosition = Vector3.zero;
                }
            });


        seq.Play();

        
    }
}
