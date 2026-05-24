using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchHealthBar : MonoBehaviour
{
    Image _healthBar;

    [SerializeField]
    Color
        _normalHealthBar,
        _spiritHealthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
    }

    public void SpiritHealthBar()
    {
        _healthBar.color = _spiritHealthBar;
    }

    public void NormalHealthBar()
    {
        _healthBar.color = _normalHealthBar;
    }
}
