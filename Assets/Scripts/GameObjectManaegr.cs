using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class GameObjectManaegr : MonoBehaviour
{
    [SerializeField] private CSVReader _csvReader = null;
    [SerializeField] private Sprite[] _itemImages = null;
    [SerializeField] private Sprite[] _envImages = null;
    [Space]
    [SerializeField] private GameObject[] _environments = null;

    Dictionary<WeatherStat, int> _rainData = null;
    List<EnvObject> _envObjects;
    List<Item> _items;
    public List<EnvObject> EnvObjects => _envObjects;
    public List<Item> ItemObjects => _items;

    public void ReadData()
    {
        _envObjects = _csvReader.ReadEnvironments();
        _items = _csvReader.ReadItems();
        _rainData = new Dictionary<WeatherStat, int>()
        {
            {WeatherStat.NONE,0}, {WeatherStat.안개비,3}, {WeatherStat.는개,10}, {WeatherStat.이슬비,20}, {WeatherStat.가랑비,40}, {WeatherStat.장대비,60}, {WeatherStat.소나기,100}
        };
    }
    public void SetEnvironmentObjects(List<int> myEnvList)
    {
        foreach (int index in myEnvList)
        {
            _environments[index].SetActive(true);
        }
    }
    public int GetRainMineAmount(WeatherStat weather) => _rainData[weather];

    public Sprite GetEnvImage(int index) => _envImages[index];

    public Sprite GetItemImage(int index) => _itemImages[index];

}
