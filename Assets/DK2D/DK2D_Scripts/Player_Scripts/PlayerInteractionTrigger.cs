using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractionTrigger : MonoBehaviour
{
    PlayerController _player;

    public Interactable currentInteractable;

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // sets reference to current interactable if interactable enters trigger
        Interactable interactable;
        if (collision.TryGetComponent<Interactable>(out interactable))
            currentInteractable = interactable;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // sets interactable to null if it leaves trigger
        Interactable interactable;
        if (collision.TryGetComponent<Interactable>(out interactable))
            currentInteractable = null;
    }

    public void Interact()
    {
        // interacts with current interactable
        currentInteractable.Interact(_player);

    }
}
