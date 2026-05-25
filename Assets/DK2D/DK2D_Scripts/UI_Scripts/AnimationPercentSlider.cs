using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimationPercentSlider : MonoBehaviour
{
    public TMP_Text text;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        // Freezes animator so it stays on that frame
        _animator.speed = 0;
    }

    public void SetAnimationFrame(float percent, string displayText = "No Text Found")
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (!_animator.enabled)
            return;

        // Converts 0-100 into 0-1
        float normalizedTime = percent / 101f;

        // Plays the animation at that exact frame/time
        _animator.Play("PercentageSlider", 0, -normalizedTime);

        // Freezes animator so it stays on that frame
        _animator.speed = 0;

        if (text != null)
            ChangeDisplayText(displayText);
    }

    void ChangeDisplayText(string displayText)
    {
        text.text = displayText;
    }
}
