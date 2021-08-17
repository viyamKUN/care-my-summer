using System.Collections;
using System.Collections.Generic;

using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine;

public class ClickDetect : MonoBehaviour
{
    [System.Serializable] public class ClickedEvent : UnityEvent { }
    public ClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

    [FormerlySerializedAs("onClick")]
    [SerializeField] private ClickedEvent m_OnClick = new ClickedEvent();

    private void OnMouseUp()
    {
        m_OnClick.Invoke();
    }
}
