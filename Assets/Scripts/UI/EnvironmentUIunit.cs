using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class EnvironmentUIunit : MonoBehaviour
{
    [SerializeField] private Button _button = null;
    [SerializeField] private Image _image = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _infoText = null;
    [SerializeField] private Text _price = null;
    int _id = 0;
    string _code = "";
    public void SetUnit(string code, int id, Sprite sprite, string name, string info, int price)
    {
        _id = id;
        _code = code;

        _image.sprite = sprite;
        _nameText.text = name;
        _infoText.text = info;
        _price.text = price.ToString();
    }

    public void ClickToBuy()
    {
        UIManager.Instance.ShowPurchasePanel(_code, _id);
    }
    public void TurnOnOff(bool isonOff)
    {
        _button.interactable = isonOff;
    }
}
