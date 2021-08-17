using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using CharacterNamespace;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterVisualManage _cVisualManager = null;
    CharacterStatus _cStat;
    void Start()
    {
        loadData();
        _cVisualManager.SetLevel(_cStat.Level);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _cVisualManager.SetLevel(++_cStat.Level);
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
}
