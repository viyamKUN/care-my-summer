using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmSource = null;
    [SerializeField] private AudioSource _effectSource = null;
    [Space]
    [SerializeField] private AudioClip _sunny = null;
    [SerializeField] private AudioClip _rainny = null;
    [SerializeField] private AudioClip _button = null;

    public static SoundManager Instance;
    string _currentPlaying = "";

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeAudio(GameObjectSystem.WeatherStat weather)
    {
        if (weather.Equals(GameObjectSystem.WeatherStat.NONE))
        {
            if (_currentPlaying.Equals(_sunny.name)) return;
            _currentPlaying = _sunny.name;
            _bgmSource.clip = _sunny;
            _bgmSource.Play();
        }
        else
        {
            if (_currentPlaying.Equals(_rainny.name)) return;
            _currentPlaying = _rainny.name;
            _bgmSource.clip = _rainny;
            _bgmSource.Play();
        }
    }
    public void PlayButtonSound()
    {
        _effectSource.PlayOneShot(_button);
    }
}
