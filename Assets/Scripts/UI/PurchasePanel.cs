using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] private Image _image = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _infoText = null;
    [SerializeField] private Text _price = null;

    public void SetPanel(Sprite sprite, string name, string info, int price)
    {
        _image.sprite = sprite;
        _nameText.text = name;
        _infoText.text = info;
        _price.text = price.ToString();
    }
}
