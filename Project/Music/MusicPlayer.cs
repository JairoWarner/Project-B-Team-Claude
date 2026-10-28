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
            audioFile = new AudioFileReader(@"C:\Users\jairo\Project-B-Team-Merch\Project\Music\mao zedong propaganda music Red Sun in the Sky.mp3");

            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }
    }
}