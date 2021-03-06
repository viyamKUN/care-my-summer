using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualManage : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    [SerializeField] private Animator _faceAnim = null;
    int _thisLev = 0;

    public void SetLevel(int level)
    {
        this._thisLev = level;
        _anim.SetFloat("level", level * 0.25f);
        _faceAnim.SetLayerWeight(level, 1);
    }

    public void SetStats(float temper, float water)
    {
        _faceAnim.SetFloat("temper", temper < 20 ? -1 : (temper > 30 ? 1 : 0));
        _faceAnim.SetFloat("water", water * 2 - 1);
    }
}
