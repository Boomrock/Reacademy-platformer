using UnityEngine;
using Zenject;

namespace Sounds
{
    public class SoundView : MonoBehaviour
    {
        public AudioSource AudioSource => audioSource;

        [SerializeField] private AudioSource audioSource;

        public class Pool : MemoryPool<SoundProtocol,SoundView>
        {
            private SoundConfig _soundConfig = Resources.Load<SoundConfig>(ResourcesConst.SoundConfig);

            protected override void OnDespawned(SoundView soundView)
            {
                soundView.AudioSource.clip = null;
                soundView.gameObject.SetActive(false);
            }

            protected override void Reinitialize(SoundProtocol protocol, SoundView soundView)
            {
                soundView.gameObject.SetActive(true);
                soundView.AudioSource.clip = _soundConfig.Get(protocol.SoundName);
                soundView.AudioSource.volume = protocol.Volume;
                soundView.AudioSource.loop = protocol.Loop;
            }
        }
    }
}