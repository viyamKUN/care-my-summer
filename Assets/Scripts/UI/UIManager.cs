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
    [SerializeField] private Image _summerseedIcon = null;
    [Space]
    [SerializeField] private GameObject _itemPanel = null;
    [SerializeField] private GameObject _envPanel = null;
    [SerializeField] private GameObject _dictPanel = null;
    [Space]
    [SerializeField] private EnvironmentPanel _environmentPanel = null;
    [SerializeField] private ItemPanel _iPanel = null;

    public void Init()
    {
        _envPanel.SetActive(true);
        _itemPanel.SetActive(true);
        _environmentPanel.SetUI();
        _iPanel.SetUI();
        _envPanel.SetActive(false);
        _itemPanel.SetActive(false);
    }
    public void SetGageUI(int level, int waterdrop)
    {
        _levelText.text = FixedValue.LEVEL_NAME[level];
        _waterdropGage.fillAmount = (float)waterdrop / FixedValue.MAX_GAGE[level];
    }
    public void SetEnvStatUI(float temper, float water)
    {
        _waterGage.fillAmount = water;
        _temperGage.fillAmount = temper / (float)FixedValue.MAX_TEMPER;
    }
    public void SetMoneyUI(int amt)
    {
        _moneyText.text = amt.ToString() + " 장";
    }
    public void SetRainText(GameObjectSystem.WeatherStat weather)
    {
        if (weather.Equals(GameObjectSystem.WeatherStat.NONE))
        {
            _rainText.text = "";
            return;
        }
        _rainText.text = string.Format("{0}가 내린다..", weather.ToString());
    }
    public void ClickDict()
    {
        _envPanel.SetActive(false);
        _itemPanel.SetActive(false);
        _dictPanel.SetActive(!_dictPanel.activeSelf);
    }
    public void ClickEnv()
    {
        _itemPanel.SetActive(false);
        _dictPanel.SetActive(false);
        _envPanel.SetActive(!_envPanel.activeSelf);
    }
    public void ClickItem()
    {
        _envPanel.SetActive(false);
        _dictPanel.SetActive(false);
        _itemPanel.SetActive(!_itemPanel.activeSelf);
    }
    public void ShowSeed()
    {
        _summerseedIcon.gameObject.SetActive(true);
    }

}
