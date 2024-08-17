using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource flipAudioSource;
    [SerializeField] private AudioSource otherAudioSource;
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
        PlayAudio(flipAudioSource, audioClips[0]);
    }

    private void PlayCorrectMatchAudio()
    {
        PlayAudio(otherAudioSource, audioClips[1]);
    }

    private void PlayIncorrectMatchAudio()
    {
        PlayAudio(otherAudioSource, audioClips[2]);
    }

    private void PlayGameOverAudio(int _)
    {
        PlayAudio(otherAudioSource, audioClips[3]);
    }

    private void PlayAudio(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
