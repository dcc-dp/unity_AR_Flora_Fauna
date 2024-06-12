using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioApp : MonoBehaviour
{
    public AudioSource src;
    public AudioClip backsound;
    public Button toggleButton;
    private bool isPlaying = true;

    private void Start()
    {
        GameObject musicPlayer = GameObject.Find("MusicPlayer");
        if (musicPlayer != null)
        {
            src = musicPlayer.GetComponent<AudioSource>();
        }

        toggleButton.onClick.AddListener(ToggleMusic);
        
    }

    void ToggleMusic()
    {
        if (src == null)
            return;

        if (isPlaying)
        {
            src.Pause();
        }
        else
        {
            src.Play();
        }

        isPlaying = !isPlaying;
    }
    public void btnOn()
    {
        src.clip = backsound;
        src.Play();
    }

    public void btnStop()
    {
        src.clip = backsound;
        src.Stop();
    }

}