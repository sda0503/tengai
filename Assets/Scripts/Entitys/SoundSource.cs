using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
        _audioSource.pitch = 1f;

        Invoke("Disable", clip.length + 2);
    }

    public void Disable()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}
