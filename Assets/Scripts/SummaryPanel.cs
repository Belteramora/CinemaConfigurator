using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.ComponentModel;
using System.IO;


public class SummaryPanel: MonoBehaviour
{
    private RectTransform rectTransform;
    private List<ItemObject> cachedItems;

    private string currentOption;

    public TMP_Dropdown categoryDropdown;

    public float moveTime;


    public List<Summary> summaries;


    public void Setup()
    {
        rectTransform = GetComponent<RectTransform>();
        categoryDropdown.ClearOptions();
        
        var options = new List<string>();
        options.Add("All");

        foreach(var category in ItemManager.categories)
        {
            options.Add(category);
        }

        categoryDropdown.AddOptions(options);

        currentOption = categoryDropdown.options[0].text;

        foreach(var sum in summaries)
        {
            sum.gameObject.SetActive(false);
        }
    }

    public void UpdateSummary(List<ItemObject> items)
    {
        cachedItems = items;
        UpdateSummary();
    }

    public void ChangeCategory(int value)
    {
        currentOption = categoryDropdown.options[value].text;

        UpdateSummary();
    }

    private void UpdateSummary()
    {
        foreach(var sum in summaries)
        {
            sum.gameObject.SetActive(false);
        }

        if(currentOption == "All")
        {
            SetSummary(summaries[0], "Total price", cachedItems.Sum(c => c.configuration.Price) + "$");
            SetSummary(summaries[1], "Total count", cachedItems.Count.ToString());

            
        }
        else
        {
            SetSummary(summaries[0], currentOption + " total", cachedItems.Where(c => c.configuration.Category == currentOption).Sum(c => c.configuration.Price) + "$");
            SetSummary(summaries[1], currentOption + " count", cachedItems.Where(c => c.configuration.Category == currentOption).Count().ToString());
        }
    }

    public void SetSummary(Summary summary, string value)
    {
        summary.gameObject.SetActive(true);
        summary.SetText(value);
    }

    public void SetSummary(Summary summary, string label, string value)
    {
        summary.gameObject.SetActive(true);
        summary.SetText(label, value);
    }

    public void Open()
    {
        rectTransform.DOAnchorPosX(0, moveTime);
    }

    public void Close()
    {
        rectTransform.DOAnchorPosX(rectTransform.rect.width, moveTime);
    }

    public void ExportToPDF()
    {
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();

        XGraphics gfx = XGraphics.FromPdfPage(page);

        XFont font = new XFont("Verdana", 20, XFontStyleEx.Regular);

        var rect = new XRect(0, 0, page.Width, font.Size);
        var lineOffset = font.Size + 5;


        gfx.DrawString("Total price: " + cachedItems.Sum(c => c.configuration.Price).ToString() + "$", font, XBrushes.Black, rect, XStringFormat.TopLeft);
        rect.Y += lineOffset;
        gfx.DrawString("Total count: " + cachedItems.Count.ToString(), font, XBrushes.Black, rect, XStringFormat.TopLeft);
        rect.Y += lineOffset;

        foreach (var category in ItemManager.categories)
        {
            gfx.DrawString(category + " total: " + cachedItems.Where(c => c.configuration.Category == category).Sum(c => c.configuration.Price) + "$", font, XBrushes.Black, rect, XStringFormat.TopLeft);
            rect.Y += lineOffset;
            gfx.DrawString(category + " count: " + cachedItems.Where(c => c.configuration.Category == currentOption).Count().ToString(), font, XBrushes.Black, rect, XStringFormat.TopLeft);
            rect.Y += lineOffset;

        }

        string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";

        string filename = "Summary.pdf";
        document.Save(downloadsPath + @"\" + filename);

        
    }

}

