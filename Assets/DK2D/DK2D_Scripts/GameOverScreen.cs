using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public bool activeScreen {  get; set; }

    private async void OnEnable()
    {
        PlayerController player = PlayerController.Instance;

        // moves game over screen over player
        transform.position = player.transform.position;

        await Task.Delay(3000);

        activeScreen = true;
    }
}
