using NAudio.Wave;

namespace Project.Music;

public static class MusicPlayer
{
    private static AudioFileReader? audioFile;
    private static WaveOutEvent? outputDevice;
    
    public static void MusicStart()
    {
        if (outputDevice == null)
        {
            string musicPath = Path.Combine(
                AppContext.BaseDirectory,
                "Music",
                "mao zedong propaganda music Red Sun in the Sky.mp3"
            );

            audioFile = new AudioFileReader(musicPath);

            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }
    }
}