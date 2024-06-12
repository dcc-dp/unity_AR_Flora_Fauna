using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class descSound : MonoBehaviour
{
    public AudioSource audioSource; // Audio source yang akan dimainkan
    public Button playPauseButton;  // Tombol untuk mengendalikan play/pause
    public Text buttonText;         // Teks pada tombol untuk menunjukkan status

    private bool isPlaying = false;

    void Start()
    {
        // Pastikan audio tidak bermain saat awal
        audioSource.Stop();

        // Tambahkan listener ke tombol play/pause
        playPauseButton.onClick.AddListener(ToggleAudio);
    }

    void ToggleAudio()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            buttonText.text = "Play";
        }
        else
        {
            audioSource.Play();
            buttonText.text = "Pause";
        }
        isPlaying = !isPlaying;
    }
}
