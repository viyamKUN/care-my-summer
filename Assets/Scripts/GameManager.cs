using System.Collections;
using System.Collections.Generic;
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

    }

    void loadData()
    {
        initData(); // TODO
    }

    void deleteData()
    {

    }

    void initData()
    {
        _cStat = new CharacterStatus();
    }
}
