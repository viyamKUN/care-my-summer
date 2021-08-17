using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class EnvironmentPanel : MonoBehaviour
{
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;
    [SerializeField] private GameObject _content = null;

    EnvironmentUIunit[] _envUnits;
    public void SetUI()
    {
        _envUnits = _content.GetComponentsInChildren<EnvironmentUIunit>();
        int i = 0;
        foreach (EnvObject e in _gameObjectManager.EnvObjects)
        {
            _envUnits[i].SetUnit(_gameObjectManager.GetEnvImage(i), e.Name, string.Format("초당 {0}개의 나뭇잎", e.MiningAmount), e.Price);
            i++;
        }
    }
}
