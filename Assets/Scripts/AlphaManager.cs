using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaManager : MonoBehaviour
{
    [Header("UI Images to hide")]
    public List<Image> imagesToHide;

    [Header("Sprites to hide")]
    public List<SpriteRenderer> spritesToHide;

    /// <summary>
    /// Set alpha of all images and sprites to 0
    /// </summary>
    public void HideAll()
    {
        // Hide UI Images
        foreach (var img in imagesToHide)
        {
            if (img != null)
            {
                Color c = img.color;
                c.a = 0f;
                img.color = c;
            }
        }

        // Hide Sprites
        foreach (var sr in spritesToHide)
        {
            if (sr != null)
            {
                Color c = sr.color;
                c.a = 0f;
                sr.color = c;
            }
        }
    }

    /// <summary>
    /// Restore alpha of all images and sprites to 1
    /// </summary>
    public void ShowAll()
    {
        // Show UI Images
        foreach (var img in imagesToHide)
        {
            if (img != null)
            {
                Color c = img.color;
                c.a = 1f;
                img.color = c;
            }
        }

        // Show Sprites
        foreach (var sr in spritesToHide)
        {
            if (sr != null)
            {
                Color c = sr.color;
                c.a = 1f;
                sr.color = c;
            }
        }
    }
}
