using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoT.AbstractClasses;
using TMPro;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    public GameObject menu;

    public AnimationPercentSlider
        healthBar,
        magicBar,
        agilityBar,
        strengthBar,
        intelligenceBar;

    public TMP_Text
        goldTextDisplay;
}
