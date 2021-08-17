using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;
    [SerializeField] private GameObject _content = null;

    EnvironmentUIunit[] _envUnits;
    public void SetUI()
    {
        _envUnits = _content.GetComponentsInChildren<EnvironmentUIunit>();
        int i = 0;
        foreach (Item e in _gameObjectManager.ItemObjects)
        {
            _envUnits[i].SetUnit("I", i, _gameObjectManager.GetItemImage(i), e.Name, e.Info, e.Price);
            i++;
        }
    }
    public void UpdatePanel()
    {
        int i = 0;
        foreach (Item e in _gameObjectManager.ItemObjects)
        {
            _envUnits[i].TurnOnOff(_gameManager.CanBuy(e.Price));
            i++;
        }
    }
}
