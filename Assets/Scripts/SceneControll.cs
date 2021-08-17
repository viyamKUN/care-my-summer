using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    public static void MoveScene(SceneName name)
    {
        SceneManager.LoadScene(FixedValue.SCENE_NAME[name]);
    }
}
