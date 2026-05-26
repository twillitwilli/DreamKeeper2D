using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;

    public float
        musicVolume,
        sfxVolume;

    public AudioClip[] musicTracks;

    [SerializeField]
    AudioSource _musicPlayer;

    public void AdjustMusicVolume()
    {
        _musicPlayer.volume = musicVolume;
    }

    public void PlayGameOverMusic()
    {
        // sets to game over music and turns looping track off
        _musicPlayer.clip = musicTracks[2];
        _musicPlayer.pitch = 1f;
        _musicPlayer.loop = false;
    }

    public void PlayMusic()
    {
        switch (_gameManager.currentScene)
        {
            case SceneSelector.SceneName.TitleScreen:
                PlayTitleScreenMusic();
                break;

            case SceneSelector.SceneName.NightmareNamikVillage:
                PlayNightmareMusic();
                break;

            case SceneSelector.SceneName.NamikVillage:
                PlayNamikVillageMusic();
                break;
        }
    }

    void PlayTitleScreenMusic()
    {
        // sets to default track for title screen
        _musicPlayer.clip = musicTracks[0];
        _musicPlayer.pitch = 1f;
        _musicPlayer.loop = true;
        _musicPlayer.Play();
    }

    void PlayNightmareMusic()
    {
        // sets to nightmare music track & changes pitch of track
        _musicPlayer.clip = musicTracks[0];
        _musicPlayer.pitch = -1f;
    }

    void PlayNamikVillageMusic()
    {
        // sets to default Namik Village Music
        _musicPlayer.clip = musicTracks[0];
        _musicPlayer.pitch = 0.35f;
    }
}
