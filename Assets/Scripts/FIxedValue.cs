using System.Collections.Generic;
using UnityEngine;

public static class FixedValue
{
    public static string SAVE_PATH = $"{Application.persistentDataPath}/{"gamedata.save"}";
    public static int MAX_LEVEL = 4;
    public static List<int> MAX_GAGE = new List<int>() { 500, 3000, 10000, 100000, 200000 };
}
