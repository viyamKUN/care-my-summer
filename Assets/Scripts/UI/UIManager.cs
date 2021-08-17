using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _levelText = null;
    [SerializeField] private Image _waterdropGage = null;
    [SerializeField] private Image _temperGage = null;
    [SerializeField] private Image _waterGage = null;

    public void SetGageUI(int level, int waterdrop)
    {
        _levelText.text = FixedValue.LEVEL_NAME[level];
        _waterdropGage.fillAmount = (float)waterdrop / FixedValue.MAX_GAGE[level];
    }
    public void SetEnvStatUI(int temper, float water)
    {
        _waterGage.fillAmount = water;
        _temperGage.fillAmount = (float)temper / FixedValue.MAX_TEMPER;
    }
}
