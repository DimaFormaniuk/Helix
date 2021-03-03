using UnityEngine;

namespace Managers
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;

        [SerializeField] private AudioSource _uiClick;
        [SerializeField] private AudioSource _touchFloor;
        [SerializeField] private AudioSource _levelCompleted;
        [SerializeField] private AudioSource _dead;

        [SerializeField] private float _currentPitch = 1;
        [SerializeField] private float _pitchStep = 0.02f;
        [SerializeField] private AudioSource _destroyPlatform;

        void Awake()
        {
            Instance = this;
        }

        public void UiClick() => Play(_uiClick);
        public void TouchFloor() => Play(_touchFloor);
        public void LevelCompleted() => Play(_levelCompleted);
        public void Dead() => Play(_dead);

        void Play(AudioSource source)
        {
            var tmp = source.pitch;
            source.pitch = source.pitch + Random.Range(-0.1f, 0.1f);
            source.Play();
            source.pitch = tmp;
        }

        public void ResetPitch()
        {
            _currentPitch = 1;
        }

        public void DestroyPlatform()
        {
            _destroyPlatform.pitch = _currentPitch;
            _destroyPlatform.Play();
            _currentPitch += _pitchStep;
        }

        public void DestroyPlatform(int value)
        {
            _destroyPlatform.pitch = 1f + (float)value * _pitchStep;
            _destroyPlatform.Play();
        }
    }
}
