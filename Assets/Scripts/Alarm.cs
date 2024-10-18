using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangingStep = 0.1f;
    [SerializeField] private float _defaultAlarmVolume = 0f;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource.volume = _defaultAlarmVolume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
            
            InitCoroutine(_maxVolume);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            InitCoroutine(_minVolume);
        }
    }

    private void InitCoroutine(float volumeLevel)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeVolume(volumeLevel));
    }

    private IEnumerator ChangeVolume(float targetVolumeLevel)
    {
        while (_audioSource.volume != targetVolumeLevel)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolumeLevel, _volumeChangingStep * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }
}
