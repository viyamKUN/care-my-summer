using System.Collections.Generic;
using UnityEngine;

public static class FixedValue
{
    public static string SAVE_PATH = $"{Application.persistentDataPath}/{"gamedata.save"}";
    public static int MAX_LEVEL = 4;
    public static List<int> MAX_GAGE = new List<int>() { 500, 3000, 10000, 100000, 200000 };
    public static List<string> LEVEL_NAME = new List<string>() { "편여름", "초여름", "한여름", "늦여름", "완여름" };
}
