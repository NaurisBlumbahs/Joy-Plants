using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float periodTime = 10;
    [SerializeField] float periodTimeDefault = 10;
    [SerializeField] float radioCost = 1;
    public bool radioIsOn;
    private bool inZone;
    private bool songHasEnded;
    private bool songIsPaused;
    private MoneyManager moneyManager;
    float wait;

    private void Start()
    {
        moneyManager = GameObject.Find("GlobalManager").GetComponent<MoneyManager>();
        audioSource.clip = GetRandomClip();
        songHasEnded = false;
    }

    private AudioClip GetRandomClip()
    {
        AudioClip music = null;
        music = clips[Random.Range(0, clips.Length)];
        
        while(music == audioSource.clip)
        {
            music = clips[Random.Range(0, clips.Length)];
        }
        return music;
    }

    void Update()
    {
        if (inZone)
        {
            if (Input.GetKeyDown(KeyCode.E) && !audioSource.isPlaying)
            {
                StartRadio();
            }

            if (!songHasEnded && !songIsPaused)
            {
                wait -= Time.deltaTime;
            }

            if (wait <= 0f)
            {
                songHasEnded = true;
            }

            if (Input.GetKeyDown(KeyCode.F) && audioSource.isPlaying)
            {
                PauseRadio();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log(audioSource.clip.name + " was skiped");
                SkipSong(); 
            }
        }

        if (songHasEnded && !songIsPaused)
        {
            Debug.Log(audioSource.clip.name + " has ended");
            StartNextSong();
        }

        if (audioSource.isPlaying)
        {
            songIsPaused = false;

            periodTime -= Time.deltaTime;

            if (periodTime <= 0)
            {
                moneyManager.UpdateMoneySpent(radioCost);

                periodTime = periodTimeDefault;
            }
        }
    }

    private void PauseRadio()
    {
        radioIsOn = false;
        Debug.Log(audioSource.clip.name + " was paused");
        audioSource.Pause();
        songIsPaused = true;
        songHasEnded = false;
    }

    private void StartRadio()
    {
        radioIsOn = true;
        wait = audioSource.clip.length;
        audioSource.Play();
        songHasEnded = false;
        Debug.Log(audioSource.clip.name + " has started");
    }

    private void StartNextSong()
    {
        radioIsOn = true;
        wait = audioSource.clip.length;
        songHasEnded = false;
        audioSource.clip = GetRandomClip();
        audioSource.Play();
        Debug.Log(audioSource.clip.name + " has started");
    }

    private void SkipSong()
    {
        radioIsOn = true;
        songHasEnded = false;
        audioSource.Stop();
        audioSource.clip = GetRandomClip();
        audioSource.Play();
        Debug.Log(audioSource.clip.name + " has started");
    }

        private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player is in zone");
            inZone = true;
        }
    }
        private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player is out of zone");
            inZone = false;
        }
    }
}
