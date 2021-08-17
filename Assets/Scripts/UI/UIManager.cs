using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _levelText = null;
    [SerializeField] private Text _moneyText = null;
    [SerializeField] private Text _rainText = null;
    [SerializeField] private Image _waterdropGage = null;
    [SerializeField] private Image _temperGage = null;
    [SerializeField] private Image _waterGage = null;
    [Space]
    [SerializeField] private GameObject _itemPanel = null;
    [SerializeField] private GameObject _envPanel = null;
    [SerializeField] private GameObject _dictPanel = null;

    public void SetGageUI(int level, int waterdrop)
    {
        _levelText.text = FixedValue.LEVEL_NAME[level];
        _waterdropGage.fillAmount = (float)waterdrop / FixedValue.MAX_GAGE[level];
    }
    public void SetEnvStatUI(int temper, float water)
    {
        _waterGage.fillAmount = water;
        _temperGage.fillAmount = (float)temper / FixedValue.MAX_TEMPER;
    }
    public void SetMoneyUI(int amt)
    {
        _moneyText.text = amt.ToString() + " 장";
    }
    public void SetRainText()
    {
        _rainText.text = ""; //가 내린다..
    }
    public void ClickDict()
    {
        _dictPanel.SetActive(!_dictPanel.activeSelf);
    }
    public void ClickEnv()
    {
        _envPanel.SetActive(!_envPanel.activeSelf);
    }
    public void ClickItem()
    {
        _itemPanel.SetActive(!_itemPanel.activeSelf);
    }
}
