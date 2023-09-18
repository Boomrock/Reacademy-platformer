namespace Sounds
{
    public class SoundProtocol
    {
        public readonly SoundName SoundName;
        public readonly float Volume;
        public readonly bool Loop;

        public SoundProtocol(SoundName soundName, float volume = 1, bool loop = false)
        {
            SoundName = soundName;
            Volume = volume;
            Loop = loop;
        }
    }
}