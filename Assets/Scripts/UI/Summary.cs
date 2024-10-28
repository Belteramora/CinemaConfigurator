using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    [SerializeField] private TMP_Text summaryValue;
    [SerializeField] private TMP_Text summaryLabel;


    public void SetText(string value)
    {
        summaryValue.text = value;
    }

    public void SetText(string label, string value)
    {
        summaryLabel.text = label;
        summaryValue.text = value;
    }
}