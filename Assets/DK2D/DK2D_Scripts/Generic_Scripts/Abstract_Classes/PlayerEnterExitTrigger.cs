using UnityEngine;

public abstract class PlayerEnterExitTrigger : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player;
        if (collision.TryGetComponent<PlayerController>(out player))
            PlayerEnteredTrigger(player);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player;
        if (collision.TryGetComponent<PlayerController>(out player))
            PlayerExitedTrigger(player);
    }

    public virtual void PlayerEnteredTrigger(PlayerController player)
    {

    }

    public virtual void PlayerExitedTrigger(PlayerController player)
    {

    }
}
