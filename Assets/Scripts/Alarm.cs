using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangingStep = 0.1f;
    [SerializeField] private float _defaultAlarmVolume = 0f;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private bool _isThiefSpotted = false;

    private void Awake()
    {
        _audioSource.volume = _defaultAlarmVolume;
    }

    private void Update()
    {
        if (_audioSource.isPlaying)
        {
            ChangeVolume();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            _isThiefSpotted = true;

            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            _isThiefSpotted = false;
        }
    }

    private void ChangeVolume()
    {       
        if (_isThiefSpotted)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeChangingStep * Time.deltaTime);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeChangingStep * Time.deltaTime);
        }

        if (_audioSource.volume <= _minVolume)
        {
            _audioSource.Stop();
        }
    }
}
