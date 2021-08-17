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
    [SerializeField] private float _saveRate = 2f;
    CharacterStatus _cStat;
    float _saveTimeBucket = 0;
    void Start()
    {
        loadData();
        _cVisualManager.SetLevel(_cStat.Level);

        _uiManager.SetGageUI(_cStat.Level, _cStat.GrowGage);
        _uiManager.SetEnvStatUI(_cStat.Temper, _cStat.Water);

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
        bin.Serialize(fs, _cStat);
        fs.Close();
    }

    void loadData()
    {
        var filename = FixedValue.SAVE_PATH;

        if (!File.Exists(filename))
        {
            _cStat = new CharacterStatus();
            return;
        }
        BinaryFormatter bin = new BinaryFormatter();
        FileStream fs = File.Open(filename, FileMode.Open);
        if (fs != null && fs.Length > 0)
        {
            _cStat = (CharacterStatus)bin.Deserialize(fs);
            fs.Close();
            return;
        }
    }

    void deleteData()
    {
        var filename = FixedValue.SAVE_PATH;
        File.Delete(filename);
    }

    public void AddWaterDrop(int amt)
    {
        _cStat.GrowGage += amt;
        if (FixedValue.MAX_GAGE[_cStat.Level] <= _cStat.GrowGage)
        {
            if (_cStat.Level >= FixedValue.MAX_LEVEL)
            {
                Debug.Log("Call Ending");
                return;
            }
            _cVisualManager.SetLevel(++_cStat.Level);
        }
        _uiManager.SetGageUI(_cStat.Level, _cStat.GrowGage);
        saveData();
    }
}
