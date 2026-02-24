using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image image;
    public TMP_Text amountText;

    public void Set(Sprite icon, int amount)
    {
        image.sprite = icon;
        amountText.text = Convert.ToString(amount);
        image.gameObject.SetActive(true);
        amountText.gameObject.SetActive(true);
    }
    public void Clear()
    {
        image.sprite = null;
        amountText.text = null;
        image.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
    }

    public void SetAmount(int amount)
    {
        amountText.text = Convert.ToString(amount);
    }
}
