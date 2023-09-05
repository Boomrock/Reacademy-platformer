namespace Sounds
{
    public class SoundController
    {
        private SoundPool _soundPool;

        public SoundController()
        {
            _soundPool = new SoundPool();
        }
        
        public void Play(SoundName soundName, float volume = 1, bool loop = false)
        {
            SwitchOff();
            
            var sound = _soundPool.TakeFromPool(soundName, volume, loop);
            sound.AudioSource.Play();
        }

        public void SwitchOff()
        {
            _soundPool.DisableCompletedSounds();
        }

        public void Stop()
        {
            _soundPool.MuteSound();
        }
    }
}