using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public bool activeScreen {  get; set; }

    private async void OnEnable()
    {
        // turns on game over music
        GameManager.Instance.audioController.PlayGameOverMusic();

        // sets player refernce
        PlayerController player = PlayerController.Instance;

        // moves game over screen over player
        transform.position = player.transform.position;

        // delays game over screen being active for 3 seconds
        await Task.Delay(3000);

        // turns active screen on so player can restart
        activeScreen = true;
    }
}
