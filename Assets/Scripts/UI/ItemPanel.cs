using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;
    [SerializeField] private GameObject _content = null;

    EnvironmentUIunit[] _envUnits;
    public void SetUI()
    {
        _envUnits = _content.GetComponentsInChildren<EnvironmentUIunit>();
        int i = 0;
        foreach (Item e in _gameObjectManager.ItemObjects)
        {
            _envUnits[i].SetUnit(_gameObjectManager.GetEnvImage(i), e.Name, e.Info, e.Price);
            i++;
        }
    }
}
