using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundPool
    {
        private SoundView _soundView;
        private List<SoundView> _listSoundViews;
        private SoundConfig _soundConfig;
        private GameObject _soundPool;

        public SoundPool()
        {
            _soundPool = new GameObject("SoundPool");
            _listSoundViews = new List<SoundView>();
            _soundView = Resources.Load<SoundView>(ResourcesConst.SoundView);
            _soundConfig = Resources.Load<SoundConfig>(ResourcesConst.SoundConfig);

            InitSoundPool();
        }

        private void InitSoundPool()
        {
            var soundViewCount = 5;
            
            for (int i = 0; i < soundViewCount; i++)
            {
                CreateSoundView();
            }
        }

        public SoundView TakeFromPool(SoundName soundName, float volume, bool loop)
        {
            var sound = _listSoundViews[SearchEmptySoundView()];

            sound.gameObject.SetActive(true);
            sound.AudioSource.clip = _soundConfig.Get(soundName);
            sound.AudioSource.volume = volume;
            sound.AudioSource.loop = loop;

            return sound;
        }

        public void DisableCompletedSounds()
        {
            foreach (var soundView in _listSoundViews)
            {
                if (!soundView.AudioSource.isPlaying)
                {
                    soundView.AudioSource.clip = null;
                    soundView.gameObject.SetActive(false);
                }
            }
        }

        public void MuteSound()
        {
            foreach (var soundView in _listSoundViews)
            {
                soundView.AudioSource.clip = null;
                soundView.gameObject.SetActive(false);
            }
        }

        private void CreateSoundView()
        {
            _listSoundViews.Add(Object.Instantiate(_soundView, _soundPool.transform));
        }

        private int SearchEmptySoundView()
        {
            for (int i = 0; i < _listSoundViews.Count; i++)
            {
                if (!_listSoundViews[i].AudioSource.isPlaying)
                {
                    return i;
                }
            }

            CreateSoundView();

            return _listSoundViews.Count - 1;
        }
    }
}