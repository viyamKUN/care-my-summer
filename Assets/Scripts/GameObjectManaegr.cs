using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameObjectSystem;

public class GameObjectManaegr : MonoBehaviour
{
    [SerializeField] private CSVReader _csvReader = null;

    Dictionary<WeatherStat, int> _rainData = null;
    List<EnvObject> _envObjects;
    List<Item> _items;

    public void ReadData()
    {
        _envObjects = _csvReader.ReadEnvironments();
        _items = _csvReader.ReadItems();
        _rainData = new Dictionary<WeatherStat, int>()
        {
            {WeatherStat.NONE,0}, {WeatherStat.안개비,3}, {WeatherStat.는개,10}, {WeatherStat.이슬비,20}, {WeatherStat.가랑비,40}, {WeatherStat.장대비,60}
        };
    }

    public int GetRainMineAmount(WeatherStat weather)
    {
        return _rainData[weather];
    }
}
