using System.IO;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{

    [Header("OPTION")]
    [SerializeField] private GameObject _optionPanel = null;

    public void ClckOption()
    {
        _optionPanel.SetActive(!_optionPanel.activeSelf);
        SoundManager.Instance.PlayButtonSound();
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
        Application.OpenURL("https://viyamkun.github.io/posts/Summer-GameJam-2021/");
        SoundManager.Instance.PlayButtonSound();
    }
    public void DeleteData()
    {
        var filename = FixedValue.SAVE_PATH;
        File.Delete(filename);
        SoundManager.Instance.PlayButtonSound();
    }
}
