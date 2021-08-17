using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class EndingListPanel : MonoBehaviour
{
    [SerializeField] private GameObject _content = null;
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObjectManaegr _gameObjectManager = null;

    Image[] _endingImage;
    public void UpdatePanel()
    {
        _endingImage = _content.GetComponentsInChildren<Image>();
        int i = 0;
        foreach (var e in _gameObjectManager.Endings)
        {
            _endingImage[i].sprite = _gameObjectManager.GetEndingImage(e.Value.ID);
            _endingImage[i].color = _gameManager.IsEndingContain(e.Value.ID) ? Color.white : new Color(0.2f, 0.2f, 0.2f);
            i++;
        }
    }
}
