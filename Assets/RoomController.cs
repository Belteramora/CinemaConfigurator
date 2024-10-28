using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private Action<bool> onDisplayChanged;

    public Dictionary<string, int> alreadyBought;
    public Dictionary<string, int> categoryLimit;

    [HideInInspector]
    public Room room;

    public GameObject roomPrefab;
    public Transform roomRoot;

    public void Start()
    {
        categoryLimit = new Dictionary<string, int>()
        {
            {"Display", 1 },
            {"Speaker", 4 },
            {"Camera", 1 },
            {"Soundproof", 4 }
        };

        alreadyBought = new Dictionary<string, int>();

        foreach(var category in categoryLimit.Keys)
        {
            alreadyBought.Add(category, 0);
        }
    }

    public void SetOnDisplayStatusChangeProvider(Action<bool> onDisplayChanged)
    {
        this.onDisplayChanged = onDisplayChanged;
    }

    public void RedrawRoom(float width, float lenght, float height)
    {
        if(room == null)
        {
            var roomGo = Instantiate(roomPrefab, roomRoot);
            room = roomGo.GetComponent<Room>();
        }

        room.Redraw(width, lenght, height);
    }

    public void AddItem(string category, GameObject instance)
    {
        alreadyBought[category]++;

        if (room == null) return;

        switch (category)
        {
            case "Display":
                room.display = instance.transform;
                instance.transform.SetParent(room.displayPoint);
                instance.transform.localPosition = Vector3.zero;

                onDisplayChanged?.Invoke(true);
                break;
            case "Camera":
                room.cam = instance.transform;
                instance.transform.SetParent(room.cameraPoint);
                instance.transform.localPosition = Vector3.zero;
                break;
            case "Speaker":
                room.speakers.Add(instance.transform);
                var speakerPoint = room.speakerPoints.First(c => c.childCount == 0);
                instance.transform.SetParent(speakerPoint);
                instance.transform.localPosition = Vector3.zero;
                room.currentSpeakerPointAvailable++;
                break;
            case "Soundproof":
                var soundproofPoint = room.soundproofPoints.First(c => c.childCount == 0);
                instance.transform.localScale = soundproofPoint.parent.localScale;
                instance.transform.SetParent(soundproofPoint);
                instance.transform.localPosition = Vector3.zero;
                room.currentSoundproofPointAvailable++;
                break;
        }
    }

    public void DeleteItem(string category)
    {
        alreadyBought[category]--;

        if (room == null) return;

        switch (category)
        {
            case "Display":
                onDisplayChanged?.Invoke(false);
                break;
            case "Speaker":
                room.currentSpeakerPointAvailable--;
                room.speakers = room.speakers.Where(c =>c != null).ToList();
                break;
            case "Soundproof":
                room.currentSoundproofPointAvailable--;
                break;
        }
    }

    public void ResizeDisplay(float value)
    {
        room.display.SetParent(null);
        room.display.localScale = new Vector3(room.display.localScale.x, value, value * 2);
        room.display.SetParent(room.displayPoint);

        room.displaySize = value;
    }
}
