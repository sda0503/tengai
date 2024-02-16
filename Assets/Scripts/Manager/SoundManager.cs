using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource _audioSource;
    private GameObject[] _musics;

    public AudioClip[] musicClips;

    private ObjectPool objectPool;

    public bool isElite;
    public bool isBoss;
    public bool isShop;
    public bool isDead;
    public bool isClear;
    public bool isEnding;

    private int _currentClipIndex = -1;
    private bool _wasNotDefault = false;

    private WaitForSeconds _waitTwoSeconds = new WaitForSeconds(2f);

    private void Awake()
    {
        instance = this;

        _audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
        _musics = GameObject.FindGameObjectsWithTag("Music");

        if (_musics.Length >= 2)
        {
            Destroy(gameObject);
        }

        objectPool = GetComponent<ObjectPool>();

        DontDestroyOnLoad(transform.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if ((isElite || isBoss || isShop || isDead || isClear || isEnding) && !_wasNotDefault)
        {
            StartCoroutine(ChangeBackGroundMusic());
            _wasNotDefault = true;
        }
        else if (!(isElite || isBoss || isShop || isDead || isClear || isEnding) && _wasNotDefault)
        {
            _wasNotDefault = false;
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
        if (musicClip == _currentClipIndex)
        {
            return;
        }

        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = musicClips[musicClip];
        _audioSource.Play();

        _currentClipIndex = musicClip;
    }

    private IEnumerator ChangeBackGroundMusic()
    {
        yield return _waitTwoSeconds;

        if (isElite && _currentClipIndex != 2)
        {
            int eliteMusicIndex = 2;
            PlayMusic(eliteMusicIndex);
        }

        if(isBoss && _currentClipIndex != 3)
        {
            int bossMusicIndex = 3;
            PlayMusic(bossMusicIndex);
        }

        if(isShop && _currentClipIndex != 4)
        {
            int shopMusicIndex = 4;
            PlayMusic(shopMusicIndex);
        }

        if(isDead && _currentClipIndex != 5)
        {
            int deadMusicIndex = 5;
            PlayMusic(deadMusicIndex);
        }

        if(isClear && _currentClipIndex != 6)
        {
            int clearMusicIndex = 6;
            PlayMusic(clearMusicIndex);
        }

        if(isEnding && _currentClipIndex != 7)
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

    public static void PlayClip(AudioClip clip)
    {
        GameObject obj = instance.objectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip);
    }
}
