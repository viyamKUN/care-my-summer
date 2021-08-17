using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPanel : MonoBehaviour
{
    public void EndEndingAnim()
    {
        SceneControll.MoveScene(SceneName.LOBBY);
    }
}
