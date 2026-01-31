using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HidePlayerAndImages : MonoBehaviour
{
    [Header("Player Sprite")]
    public SpriteRenderer playerSprite; // Assign your player's SpriteRenderer here
    public SpriteRenderer weaponSprite;

    [Header("UI Images to Hide")]
    public List<Image> imagesToHide; // Assign any UI Images you want to hide

    [Header("TextMeshPro to Hide")]
    public List<TextMeshProUGUI> textsToHide; // Assign TextMeshProUGUI elements

    /// <summary>
    /// Hide everything by setting alpha to 0
    /// </summary>
    public void SetAlphaZero()
    {
        // Hide player sprite
        if (playerSprite != null)
        {
            Color playerColor = playerSprite.color;
            playerColor.a = 0f;
            playerSprite.color = playerColor;
        }

        // Hide weapon sprite
        if (weaponSprite != null)
        {
            Color weaponColor = weaponSprite.color;
            weaponColor.a = 0f;
            weaponSprite.color = weaponColor;
        }

        // Hide UI images
        foreach (var img in imagesToHide)
        {
            if (img != null)
            {
                Color imgColor = img.color;
                imgColor.a = 0f;
                img.color = imgColor;
            }
        }

        // Hide TextMeshProUGUI
        foreach (var txt in textsToHide)
        {
            if (txt != null)
            {
                Color txtColor = txt.color;
                txtColor.a = 0f;
                txt.color = txtColor;
            }
        }
    }

    /// <summary>
    /// Restore everything by setting alpha to 1
    /// </summary>
    public void RestoreAlpha()
    {
        // Restore player sprite
        if (playerSprite != null)
        {
            Color playerColor = playerSprite.color;
            playerColor.a = 1f;
            playerSprite.color = playerColor;
        }

        // Restore weapon sprite
        if (weaponSprite != null)
        {
            Color weaponColor = weaponSprite.color;
            weaponColor.a = 1f;
            weaponSprite.color = weaponColor;
        }

        // Restore UI images
        foreach (var img in imagesToHide)
        {
            if (img != null)
            {
                Color imgColor = img.color;
                imgColor.a = 1f;
                img.color = imgColor;
            }
        }

        // Restore TextMeshProUGUI
        foreach (var txt in textsToHide)
        {
            if (txt != null)
            {
                Color txtColor = txt.color;
                txtColor.a = 1f;
                txt.color = txtColor;
            }
        }
    }
}
