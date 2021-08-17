using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{

    [Header("OPTION")]
    [SerializeField] private GameObject _optionPanel = null;

    public void ClckOption()
    {
        _optionPanel.SetActive(!_optionPanel.activeSelf);
    }
    public void GoMain()
    {
        SceneControll.MoveScene(SceneName.LOBBY);
    }
    public void GoGame()
    {
        SceneControll.MoveScene(SceneName.GAME);
    }
    public void GoBlog()
    {
        Application.OpenURL("https://viyamkun.github.io");
    }

}
