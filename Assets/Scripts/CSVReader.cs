using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using GameObjectSystem;
using EndingSystem;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private TextAsset _environment = null;
    [SerializeField] private TextAsset _items = null;
    [SerializeField] private TextAsset _endings = null;

    public List<EnvObject> ReadEnvironments()
    {
        List<EnvObject> envlist = new List<EnvObject>();
        System.IO.StringReader file = new System.IO.StringReader(_environment.text);
        string line = file.ReadLine();
        while ((line = file.ReadLine()) != null)
        {
            List<string> ls = line.Split(',').ToList();
            envlist.Add(new EnvObject(ls[0], int.Parse(ls[1]), int.Parse(ls[2])));
        }
        return envlist;
    }
    public List<Item> ReadItems()
    {
        List<Item> itemlist = new List<Item>();
        System.IO.StringReader file = new System.IO.StringReader(_items.text);
        string line = file.ReadLine();
        while ((line = file.ReadLine()) != null)
        {
            List<string> ls = line.Split(',').ToList();
            itemlist.Add(new Item(ls[0], ls[1], int.Parse(ls[2])));
        }
        return itemlist;
    }

    public Dictionary<string, Ending> ReadEndings()
    {
        Dictionary<string, Ending> endinglist = new Dictionary<string, Ending>();
        System.IO.StringReader file = new System.IO.StringReader(_endings.text);
        string line = file.ReadLine();
        while ((line = file.ReadLine()) != null)
        {
            List<string> ls = line.Split(',').ToList();
            endinglist.Add(ls[0], new Ending(ls[1], ls[2]));
        }
        return endinglist;
    }
}
