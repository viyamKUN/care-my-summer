using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualManage : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    [SerializeField] private Animator _faceAnim = null;
    int _thisLev = 0;

    public void SetLevel(uint level)
    {
        this._thisLev = (int)level;
        _anim.SetFloat("level", level * 0.25f);
        _faceAnim.SetLayerWeight((int)level, 1);
        _faceAnim.SetFloat("temper", 0);
        _faceAnim.SetFloat("water", 0);
    }
}
