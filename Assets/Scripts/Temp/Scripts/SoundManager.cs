using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    private GameObject[] musics;

    public AudioClip[] musicClips;

    public bool isElite;
    public bool isBoss;
    public bool isShop;
    public bool isDead;
    public bool isClear;
    public bool isEnding;
    private int currentClipIndex = -1;
    private bool wasNotDefault = false;

    private WaitForSeconds waitTwoSeconds = new WaitForSeconds(2f);

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
        musics = GameObject.FindGameObjectsWithTag("Music");

        if (musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if ((isElite || isBoss || isShop || isDead || isClear || isEnding) && !wasNotDefault)
        {
            StartCoroutine(ChangeBackGroundMusic());
            wasNotDefault = true;
        }
        else if (!(isElite || isBoss || isShop || isDead || isClear || isEnding) && wasNotDefault)
        {
            //StopAllCoroutines();
            wasNotDefault = false;
            PlayDefaultMusic();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        if (sceneIndex == 0)  // 인트로
        {
            PlayMusic(0);
        }
        else if (sceneIndex == 1) // 스테이지1
        {
            PlayMusic(1);
        }
    }

    private void PlayMusic(int musicClip)
    {
        if (musicClip == currentClipIndex)
        {
            return;
        }

        //AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = musicClips[musicClip];
        audioSource.Play();

        currentClipIndex = musicClip;
    }

    private IEnumerator ChangeBackGroundMusic()
    {
        yield return waitTwoSeconds;

        if (isElite && currentClipIndex != 2)
        {
            int eliteMusicIndex = 2;
            PlayMusic(eliteMusicIndex);
        }

        if(isBoss &&  currentClipIndex != 3)
        {
            int bossMusicIndex = 3;
            PlayMusic(bossMusicIndex);
        }

        if(isShop && currentClipIndex != 4)
        {
            int shopMusicIndex = 4;
            PlayMusic(shopMusicIndex);
        }

        if(isDead && currentClipIndex != 5)
        {
            int deadMusicIndex = 5;
            PlayMusic(deadMusicIndex);
        }

        if(isClear && currentClipIndex != 6)
        {
            int clearMusicIndex = 6;
            PlayMusic(clearMusicIndex);
        }

        if(isEnding && currentClipIndex != 7)
        {
            int endingMusicIndex = 7;
            PlayMusic(endingMusicIndex);
        }
    }

    private void PlayDefaultMusic()
    {
        int defaultMusicIndex = 1;
        PlayMusic(defaultMusicIndex);
    }
}
