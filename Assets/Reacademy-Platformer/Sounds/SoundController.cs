using System.Collections.Generic;

namespace Sounds
{
    public class SoundController
    {
        private SoundView.Pool _soundPool;
        private Stack<SoundView> _soundViews = new();

        public SoundController(SoundView.Pool soundPool)
        {
            _soundPool = soundPool;
        }
        
        public void Play(SoundProtocol protocol)
        {
            SwitchOff();
            
            var sound = _soundPool.Spawn(protocol);
            sound.AudioSource.Play();
            _soundViews.Push(sound);
        }

        public void SwitchOff()
        {
            foreach (var sound in _soundViews)
            {
                if (!sound.AudioSource.isPlaying)
                {
                    _soundPool.Despawn(sound);
                }
            }
        }

        public void Stop()
        {
            foreach (var sound in _soundViews)
            {
                _soundPool.Despawn(sound);
            }
        }
    }
}