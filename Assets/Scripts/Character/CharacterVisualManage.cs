using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualManage : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    int _thisLev = 0;

    public void SetLevel(uint level)
    {
        this._thisLev = (int)level;
        _anim.SetFloat("level", level * 0.25f);
    }
}
