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
    [SerializeField] private float _foxComeRate = 20f;
    [Space]
    [SerializeField] private GameObject _rainObject = null;
    [SerializeField] private GameObject _powerRainObject = null;
    [SerializeField] private GameObject _rainbowObject = null;
    [SerializeField] private GameObject _foxObject = null;
    [Space]
    [SerializeField] private ParticleSystem _particle = null;

    UserData _userData;
    CharacterStatus _cStat;
    GameObjectSystem.WeatherStat _nowWeather;

    Coroutine _foxCome = null;
    Coroutine _stopRain = null;
    Coroutine _powerRain = null;

    float _oneSecondBucket = 0;
    float _foxTimeBucket = 0;

    int _miningMoneyAmount = 0;
    bool _isGameOver = false;

    void Start()
    {
        _gameObjectManager.ReadData();

        loadData();
        _cVisualManager.SetLevel(_cStat.Level);

        _uiManager.Init();

        _uiManager.SetGageUI(_cStat.Level, _cStat.GrowGage);
        _uiManager.SetEnvStatUI(_cStat.Temper, _cStat.Water);
        _uiManager.SetMoneyUI(_userData.Money);

        _cVisualManager.SetStats(_cStat.Temper, _cStat.Water);

        if (_cStat.HasSeed) _uiManager.ShowSeed();

        SetRain((GameObjectSystem.WeatherStat)(_cStat.Level + 1));

        _gameObjectManager.SetEnvironmentObjects(_userData.Environments);

        _foxTimeBucket = Time.time;
        _oneSecondBucket = Time.time;

        SetMiningMoneyAmount();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddWaterDrop(100);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddMoney(10000);
        }
        if (Time.time - _foxTimeBucket > _foxComeRate)
        {
            _foxTimeBucket = Time.time;
            _foxCome = StartCoroutine(foxCoroutine());
        }
        if (Time.time - _oneSecondBucket > 1)
        {
            _oneSecondBucket = Time.time;

            AddMoney(_miningMoneyAmount);
            switch (_nowWeather)
            {
                case GameObjectSystem.WeatherStat.NONE:
                    ChangeWater(-0.01f);
                    ChangeTemper(0.1f);
                    break;
                case GameObjectSystem.WeatherStat.소나기:
                    ChangeWater(0.02f);
                    ChangeTemper(-0.1f);
                    AddWaterDrop(_gameObjectManager.GetRainMineAmount(_nowWeather));
                    break;
                default:
                    ChangeWater(0.01f);
                    ChangeTemper(-0.05f);
                    AddWaterDrop(_gameObjectManager.GetRainMineAmount(_nowWeather));
                    break;
            }
            _rainbowObject.SetActive(_cStat.Temper >= FixedValue.MAX_TEMPER - 5 && _cStat.Water >= 0.8f);
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
    public void PlayParticle()
    {
        _particle.gameObject.SetActive(true);
        _particle.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _particle.Stop();
        _particle.Play();
    }
    public void SetRain(GameObjectSystem.WeatherStat weather)
    {
        _nowWeather = weather;
        _uiManager.SetRainText(_nowWeather);
        switch (weather)
        {
            case GameObjectSystem.WeatherStat.NONE:
                _rainObject.SetActive(false);
                _powerRainObject.SetActive(false);
                break;
            case GameObjectSystem.WeatherStat.소나기:
                _rainObject.SetActive(false);
                _powerRainObject.SetActive(true);
                break;
            default:
                _rainObject.SetActive(true);
                _powerRainObject.SetActive(false);
                break;
        }
        SoundManager.Instance.ChangeAudio(_nowWeather);
    }
    public void AddWaterDrop(int amt)
    {
        _cStat.GrowGage += amt;
        if (FixedValue.MAX_GAGE[_cStat.Level] <= _cStat.GrowGage)
        {
            if (_cStat.Level >= FixedValue.MAX_LEVEL)
            {
                CallEnding("Perfect");
                return;
            }
            _cVisualManager.SetLevel(++_cStat.Level);
            SetRain((GameObjectSystem.WeatherStat)(_cStat.Level + 1));
        }
        _uiManager.SetGageUI(_cStat.Level, _cStat.GrowGage);
        saveData();
    }
    public void ChangeTemper(float amt)
    {
        _cStat.Temper = Mathf.Clamp(_cStat.Temper + amt, 0, FixedValue.MAX_TEMPER);
        _cVisualManager.SetStats(_cStat.Temper, _cStat.Water);
        _uiManager.SetEnvStatUI(_cStat.Temper, _cStat.Water);
        saveData();
        if (_cStat.Temper < 5) CallEnding("LowTemper");
    }

    public void ChangeWater(float amt)
    {
        _cStat.Water = Mathf.Clamp(_cStat.Water + amt, 0, 1);
        _cVisualManager.SetStats(_cStat.Temper, _cStat.Water);
        _uiManager.SetEnvStatUI(_cStat.Temper, _cStat.Water);
        saveData();
        if (_cStat.Water < 0.1f) CallEnding("LowWater");
    }
    public void AddMoney(int amt)
    {
        if (_isGameOver) return;

        _userData.Money += amt;
        _uiManager.SetMoneyUI(_userData.Money);
        saveData();
    }
    public bool UseMoney(int amt)
    {
        if (!CanBuy(amt)) return false;

        _userData.Money -= amt;
        _uiManager.SetMoneyUI(_userData.Money);
        saveData();
        return true;
    }
    public bool CanBuy(int target)
    {
        return _userData.Money >= target;
    }
    public bool IsExist(int target)
    {
        return _userData.Environments.Contains(target);
    }
    public void GetSeed()
    {
        _cStat.HasSeed = true;
        saveData();
        _uiManager.ShowSeed();
    }
    public void BuyItem(int id)
    {
        int price = _gameObjectManager.ItemObjects[id].Price;
        if (!UseMoney(price)) return;

        switch (id)
        {
            case 0:
                if (_stopRain != null) StopCoroutine(_stopRain);
                _powerRain = StartCoroutine(callPowerRain(10));
                break;
            case 1:
                if (_powerRain != null) StopCoroutine(_powerRain);
                _stopRain = StartCoroutine(stopRain(30));
                break;
            case 2:
                Debug.Log("산성비 막아주는 아이템");
                break;
            case 3:
                Debug.Log("태풍을 막아주는 아이템");
                break;
            case 4:
                ChangeTemper(10);
                break;
            case 5:
                ChangeWater(0.2f);
                break;
        }
    }
    public bool IsEndingContain(int id)
    {
        return _userData.Endings.Contains(id);
    }
    public void BuyEnvironment(int id)
    {
        int price = _gameObjectManager.EnvObjects[id].Price;
        if (!UseMoney(price)) return;
        _userData.Environments.Add(id);
        _gameObjectManager.SetEnvironmentObjects(_userData.Environments);
        SetMiningMoneyAmount();
    }
    IEnumerator foxCoroutine()
    {
        _foxObject.SetActive(true);
        yield return new WaitForSeconds(15f);
        _foxObject.SetActive(false);
    }
    IEnumerator stopRain(float sec)
    {
        SetRain(GameObjectSystem.WeatherStat.NONE);
        yield return new WaitForSeconds(sec);
        SetRain((GameObjectSystem.WeatherStat)(_cStat.Level + 1));
    }

    IEnumerator callPowerRain(float sec)
    {
        SetRain(GameObjectSystem.WeatherStat.소나기);
        yield return new WaitForSeconds(sec);
        SetRain((GameObjectSystem.WeatherStat)(_cStat.Level + 1));
    }

    public void SetMiningMoneyAmount()
    {
        _miningMoneyAmount = 0;
        foreach (int index in _userData.Environments)
        {
            _miningMoneyAmount += _gameObjectManager.EnvObjects[index].MiningAmount;
        }
    }
    public void CallEnding(string code)
    {
        int endingID = _gameObjectManager.GetEnding(code).ID;
        if (!_userData.Endings.Contains(endingID))
            _userData.Endings.Add(endingID);

        if (!_uiManager.ShowEnding(code)) return;

        _isGameOver = true;

        _userData.Money += 2000;
        _userData.cStatus = new CharacterStatus();
        saveData();
    }
}
