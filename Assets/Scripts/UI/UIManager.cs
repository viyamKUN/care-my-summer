using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameObjectSystem;

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
    [SerializeField] private PurchasePanel _purchasePanel = null;
    [Space]
    [SerializeField] private GameObject _endingPanel = null;
    [SerializeField] private Text _endingTitle = null;
    [SerializeField] private Text _endingInfo = null;
    [SerializeField] private Image _endingProfile = null;
    [Space]
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;

    public static UIManager Instance = null;
    purchaseTemp _perchaseTemp;

    private void Awake()
    {
        Instance = this;
    }

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
        ShutPurchasePanel();
        _dictPanel.SetActive(!_dictPanel.activeSelf);
    }
    public void ClickEnv()
    {
        _itemPanel.SetActive(false);
        _dictPanel.SetActive(false);
        ShutPurchasePanel();
        if (!_envPanel.activeSelf) _environmentPanel.UpdatePanel();
        _envPanel.SetActive(!_envPanel.activeSelf);
    }
    public void ClickItem()
    {
        _envPanel.SetActive(false);
        _dictPanel.SetActive(false);
        ShutPurchasePanel();
        if (!_itemPanel.activeSelf) _iPanel.UpdatePanel();
        _itemPanel.SetActive(!_itemPanel.activeSelf);
    }
    public void ShowSeed()
    {
        _summerseedIcon.gameObject.SetActive(true);
    }
    public void ShowPurchasePanel(string code, int id)
    {
        _perchaseTemp.Code = code;
        _perchaseTemp.ID = id;

        switch (code)
        {
            case "E":
                EnvObject envObj = _gameObjectManager.EnvObjects[id];
                _purchasePanel.SetPanel(_gameObjectManager.GetEnvImage(id), envObj.Name, envObj.Info(), envObj.Price);
                break;
            case "I":
                Item itemObj = _gameObjectManager.ItemObjects[id];
                _purchasePanel.SetPanel(_gameObjectManager.GetItemImage(id), itemObj.Name, itemObj.Info, itemObj.Price);
                break;
        }
        _purchasePanel.gameObject.SetActive(true);
    }
    public void ShutPurchasePanel()
    {
        _purchasePanel.gameObject.SetActive(false);
    }
    public void Buy()
    {
        switch (_perchaseTemp.Code)
        {
            case "E":
                _gameManager.BuyEnvironment(_perchaseTemp.ID);
                ShutPurchasePanel();
                ClickEnv();
                break;
            case "I":
                _gameManager.BuyItem(_perchaseTemp.ID);
                ShutPurchasePanel();
                ClickItem();
                break;
        }
    }
    public void ShowEnding(string endingCode)
    {
        if (_endingPanel.activeSelf) return;

        EndingSystem.Ending ending = _gameObjectManager.GetEnding(endingCode);
        _endingTitle.text = ending.Name;
        _endingInfo.text = ending.Info;
        _endingProfile.sprite = _gameObjectManager.GetEndingImage(ending.ID);
        _endingPanel.SetActive(true);
    }
}

struct purchaseTemp
{
    public string Code;
    public int ID;
}
