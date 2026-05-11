using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public enum FacingDirection
    {
        N,
        S,
        E,
        W,
        NE,
        NW,
        SE,
        SW
    }

    public FacingDirection currentFacingDirection;

    public void SetAnimation()
    {
        switch (currentFacingDirection)
        {
            case FacingDirection.N:
                _animator.Play("PlayerWalkingUp");
                break;

            case FacingDirection.S:
                _animator.Play("PlayerWalkingDown");
                break;

            case FacingDirection.E:
                _animator.Play("PlayerWalkingRight");
                break;

            case FacingDirection.W:
                _animator.Play("PlayerWalkingLeft");
                break;

            case FacingDirection.NE:
                _animator.Play("PlayerWalkingUpRight");
                break;

            case FacingDirection.NW:
                _animator.Play("PlayerWalkingUpLeft");
                break;

            case FacingDirection.SE:
                _animator.Play("PlayerWalkingDownRight");
                break;

            case FacingDirection.SW:
                _animator.Play("PlayerWalkingDownLeft");
                break;
        }
    }
}
