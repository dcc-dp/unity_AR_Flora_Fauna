using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setBackgroundMusic : MonoBehaviour
{
    private static setBackgroundMusic instance = null;
    private AudioSource audioSource;

    public AudioClip scene1Music;
    public AudioClip otherScenesMusic;
    public string myScene;
    public static setBackgroundMusic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<setBackgroundMusic>();
                if (instance == null)
                {
                    GameObject go = new GameObject("setBackgroundMusic");
                    instance = go.AddComponent<setBackgroundMusic>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //audioSource = gameObject.AddComponent<AudioSource>();
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        // Logika untuk mengatur musik berdasarkan nama scene
        if (scene.name == "Home")
        {
            if (audioSource.clip != scene1Music)
            {
                audioSource.clip = scene1Music;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != otherScenesMusic)
            {
                audioSource.clip = otherScenesMusic;
                audioSource.Play();
            }
        }

        // Menghentikan musik di scene tertentu
        if (scene.name == "Kuis" || scene.name == "ARSceneBangkai" || scene.name == "ARSceneSemar" || scene.name == "ARSceneRaflesia" || scene.name == "ARSceneAqui" || scene.name == "ARSceneBadak" || scene.name == "ARSceneCendrawasih" || scene.name == "ARSceneGajah" || scene.name == "ARSceneKomodo" || scene.name == "ARSceneOrangutan" || scene.name == "ARSceneTiger")
        {
            if (audioSource.clip == otherScenesMusic)
            {
                audioSource.Stop(); // Hentikan musik
            }
        }

        if (scene.name == "Start" || scene.name == "MenuFlora" || scene.name == "MenuFauna")
        {
            Debug.Log("Scene Loaded: " + scene.name);
            Debug.Log("Current Clip: " + audioSource.clip);
            if (audioSource.clip == otherScenesMusic)
            {
                audioSource.clip = otherScenesMusic;
                audioSource.Play(); 
            }
        }
        /*else
        {
            // Pastikan untuk memutar musik yang benar di scene lain
            if (audioSource.clip != otherScenesMusic && !audioSource.isPlaying)
            {
                audioSource.clip = otherScenesMusic;
                audioSource.Play();
            }
        }*/
        myScene = scene.name;
    }

    public void ToggleMusic()
    {
        Debug.Log(audioSource.isPlaying);
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            
            audioSource.Play();
        }
    }
}
