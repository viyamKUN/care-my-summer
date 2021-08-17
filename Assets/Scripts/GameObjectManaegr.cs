using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class GameObjectManaegr : MonoBehaviour
{
    [SerializeField] private CSVReader _csvReader = null;
    List<EnvObject> _envObjects;
    List<Item> _items;

    public void ReadData()
    {
        _envObjects = _csvReader.ReadEnvironments();
        _items = _csvReader.ReadItems();
    }
}
