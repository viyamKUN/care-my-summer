using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class EnvironmentPanel : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;
    [SerializeField] private GameObject _content = null;

    EnvironmentUIunit[] _envUnits;
    public void SetUI()
    {
        _envUnits = _content.GetComponentsInChildren<EnvironmentUIunit>();
        int i = 0;
        foreach (EnvObject e in _gameObjectManager.EnvObjects)
        {
            _envUnits[i].SetUnit("E", i, _gameObjectManager.GetEnvImage(i), e.Name, e.Info(), e.Price);
            i++;
        }
    }
    public void UpdatePanel()
    {
        int i = 0;
        foreach (EnvObject e in _gameObjectManager.EnvObjects)
        {
            _envUnits[i].TurnOnOff(_gameManager.CanBuy(e.Price) && (!_gameManager.IsExist(i)));
            i++;
        }
    }
}
