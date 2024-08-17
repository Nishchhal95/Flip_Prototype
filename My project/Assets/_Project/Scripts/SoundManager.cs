using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private void OnEnable()
    {
        GameController.CardFlipped += PlayCardFlipAudio;
        GameController.CorrectMatch += PlayCorrectMatchAudio;
        GameController.IncorrectMatch += PlayIncorrectMatchAudio;
        GameController.GameOver += PlayGameOverAudio;
    }

    private void OnDisable()
    {
        GameController.CardFlipped -= PlayCardFlipAudio;
        GameController.CorrectMatch -= PlayCorrectMatchAudio;
        GameController.IncorrectMatch -= PlayIncorrectMatchAudio;
        GameController.GameOver -= PlayGameOverAudio;
    }

    private void PlayCardFlipAudio()
    {
        PlayAudio(audioClips[0]);
    }

    private void PlayCorrectMatchAudio()
    {
        PlayAudio(audioClips[1]);
    }

    private void PlayIncorrectMatchAudio()
    {
        PlayAudio(audioClips[2]);
    }

    private void PlayGameOverAudio(int _)
    {
        PlayAudio(audioClips[3]);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        if (mainAudioSource.isPlaying)
        {
            mainAudioSource.Stop();
        }

        mainAudioSource.clip = audioClip;
        mainAudioSource.Play();
    }
}
