using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Sounds
{
    GearClicked,
    GearReleased,
    CloseMiniGame,
    OpenMiniGame,
    WinSound
}

[System.Serializable]
public class SoundAudioClip
{
    public Sounds Sound;
    public AudioClip Clip;
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<SoundAudioClip> soundsList;
    [SerializeField] private AudioSource audioSource;
    
    
    /// <summary>
    /// Проигрываем звук из списка доступных
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(Sounds sound)
    {
        audioSource.PlayOneShot(GetAudioClipByName(sound));
    }

    /// <summary>
    /// Находим звук по списку
    /// </summary>
    /// <param name="sound"></param>
    /// <returns></returns>
    private AudioClip GetAudioClipByName(Sounds sound)
    {
        return soundsList.Find(x => x.Sound == sound).Clip;
    }
}
