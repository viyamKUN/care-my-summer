using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using CharacterNamespace;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterVisualManage _cVisualManager = null;
    [SerializeField] private UIManager _uiManager = null;
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;
    [Space]
    [SerializeField] private float _saveRate = 2f;
    [Space]
    [SerializeField] private GameObject _rainObject = null;

    UserData _userData;
    CharacterStatus _cStat;
    GameObjectSystem.WeatherStat _nowWeather;

    float _saveTimeBucket = 0;

    void Start()
    {
        _gameObjectManager.ReadData();

        loadData();
        _cVisualManager.SetLevel(_cStat.Level);

        _uiManager.SetGageUI(_cStat.Level, _cStat.GrowGage);
        _uiManager.SetEnvStatUI(_cStat.Temper, _cStat.Water);
        _uiManager.SetMoneyUI(_userData.Money);

        _cVisualManager.SetStats(_cStat.Temper, _cStat.Water);

        SetRain((GameObjectSystem.WeatherStat)(_cStat.Level + 1));

        _saveTimeBucket = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddWaterDrop(100);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            deleteData();
        }
        if (Time.time - _saveTimeBucket > _saveRate)
        {
            _saveTimeBucket = Time.time;
            saveData();
        }
    }

    void saveData()
    {
        var filename = FixedValue.SAVE_PATH;

        BinaryFormatter bin = new BinaryFormatter();
        FileStream fs = File.Create(filename);
        bin.Serialize(fs, _userData);
        fs.Close();
    }

    void loadData()
    {
        var filename = FixedValue.SAVE_PATH;

        if (!File.Exists(filename))
        {
            _userData = new UserData();
            _cStat = _userData.cStatus;
            return;
        }
        BinaryFormatter bin = new BinaryFormatter();
        FileStream fs = File.Open(filename, FileMode.Open);
        if (fs != null && fs.Length > 0)
        {
            _userData = (UserData)bin.Deserialize(fs);
            _cStat = _userData.cStatus;
            fs.Close();
            return;
        }
    }
    void deleteData()
    {
        var filename = FixedValue.SAVE_PATH;
        File.Delete(filename);
    }
    public void SetRain(GameObjectSystem.WeatherStat weather)
    {
        _nowWeather = weather;
        _uiManager.SetRainText(_nowWeather);
        switch (weather)
        {
            case GameObjectSystem.WeatherStat.NONE:
                _rainObject.SetActive(false);
                break;
            case GameObjectSystem.WeatherStat.소나기:
                _rainObject.SetActive(false);
                // TODO Turn On 소나기 object
                break;
            default:
                _rainObject.SetActive(true);
                break;
        }
    }
    public void AddWaterDrop(int amt)
    {
        _cStat.GrowGage += amt;
        if (FixedValue.MAX_GAGE[_cStat.Level] <= _cStat.GrowGage)
        {
            if (_cStat.Level >= FixedValue.MAX_LEVEL)
            {
                Debug.Log("Call Ending");
                _userData.Money += 2000;
                return;
            }
            _cVisualManager.SetLevel(++_cStat.Level);
        }
        _uiManager.SetGageUI(_cStat.Level, _cStat.GrowGage);
        saveData();
    }
    public void ChangeTemper(int amt)
    {
        _cVisualManager.SetStats(_cStat.Temper, _cStat.Water);
    }

    public void ChangeWater(float amt)
    {
        _cVisualManager.SetStats(_cStat.Temper, _cStat.Water);
    }
    public void AddMoney(int amt)
    {
        _userData.Money += amt;
        _uiManager.SetMoneyUI(_userData.Money);
        saveData();
    }
    public bool UseMoney(int amt)
    {
        if (amt < 0)
            if (_userData.Money < amt)
                return false;

        _userData.Money -= amt;
        _uiManager.SetMoneyUI(_userData.Money);
        saveData();
        return true;
    }
}
