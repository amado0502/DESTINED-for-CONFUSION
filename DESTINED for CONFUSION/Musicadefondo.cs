using NAudio.Wave;
using System;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Emit;

public class MusicaDeFondo
{
    private static WaveOutEvent? waveOut;
    private static AudioFileReader? audioFile;
    private static bool isPlaying = false;
    private static ManualResetEventSlim gameEnded = new ManualResetEventSlim(false);
    private static string? rutaArchivo;
    private static bool wasManualStop = false;

    public MusicaDeFondo()
    {
        waveOut = null;
        audioFile = null;
    }

    public static void IniciarMusica()
    {
        rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "Resources/Pan's Labyrinth.mp3");
        IniciarMusica(rutaArchivo);
    }
    public static void IniciarMusica(string rutaArchivo)
    {
        if (!isPlaying)
    {
        isPlaying = true;
        waveOut = new WaveOutEvent();
        audioFile = new AudioFileReader(rutaArchivo);
        waveOut.Init(audioFile);
        waveOut.Play();

        waveOut.PlaybackStopped += (sender, e) =>
        {
            if(!wasManualStop)
            {
                waveOut.Stop();
                isPlaying = false;
                wasManualStop = false;
                IniciarMusica(rutaArchivo);   
            }
            else
            {
                waveOut.Stop();
                isPlaying = false;
            }
            
        };
    }
    }


    public static void DetenerMusica()
    {
        if(waveOut != null)
        {
            wasManualStop = true;
            waveOut.Stop();
            isPlaying = false;
        }
    }

    public static void JuegoTerminado()
    {
        gameEnded.Set();
    }
}

internal class Assembly
{
}