using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //How loud should wave sounds be?
    public float waveSoundVolume = 0.33f;

    //What sound should play if we lost the wave
    public AudioClip waveLoseSound = null;

    //What sound should play when a wave starts
    public AudioClip waveStartSound = null;

    //Waht sound should play when a wave ends
    public AudioClip waveEndSound = null;

    //What soundtrack should play while in a pause
    public AudioClip pauseSoundtrackSound = null;

    //What soundtrack should play when in the game
    public AudioClip gameSoundtrackSound = null;

    //How much HP should someone get after a Wave
    public int HealthBonus = 10;

    //How much Mana should they get after a Wave
    public int ManaBonus = 10;

    //How much Cash should they get after a wave
    public int CashBonus = 10;


    //How many waves do we have
    public int Amount = 10;

    //How long is each wave in seconds
    public int LengthInSeconds = 90;

    //How long is each pause in seconds
    public int PauseInSeconds = 30;

    //How much more pause seconds does the player get per wave
    public int PauseAddInSeconds = 10;

    //Are we in a pause?
    public bool InPause = false;

    //Did we enable pause in this or last frame?
    public int JustPaused = 0;

    //Which wave are we at right now
    public int CurWave = 1;

    //When should this wave end in seconds?
    private double WaveEndSeconds = 0;

    //When should this pause end in seconds?
    private double PauseEndSeconds = 0;

    //Did we lose?
    private bool lostGame = false;

    //This audio source is used by the wave controller to play the background music and the wave start/end sounds
    AudioSource waveSounds = null;

    //Remove this when going into production, debug wave data display
    void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;

        if(InPause)
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.Label(transform.position, "Wave: " + CurWave + "| Pause: " + (PauseEndSeconds - Time.time), style);
        }
        else
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.Label(transform.position, "Wave: " + CurWave + "| Timeleft: " + (WaveEndSeconds - Time.time), style);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waveSounds = gameObject.AddComponent<AudioSource>();
        waveSounds.volume = waveSoundVolume;

        WaveEndSeconds = Time.time + LengthInSeconds;

        if(waveStartSound != null)
        {
            PlayAudio(waveStartSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //todo: add support for game pausing and not having pressed start at settings

        if(InPause)
        {
            PauseHandler();
        }
        else
        {
            WaveHandler();
        }


    }

    //We call this to play sounds
    void PlayAudio(AudioClip clip)
    {
        waveSounds.clip = clip;
        waveSounds.Play();


        StartCoroutine(StartMethod(clip));
    }

    //This is responsible for playing the next sound if needed (e.g. gametrack after wavestart) and autolooping the soundtracks
    private IEnumerator StartMethod(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);

        if(clip == waveStartSound)
        {
            if(gameSoundtrackSound != null)
                PlayAudio(gameSoundtrackSound);
        }

        if(clip == waveEndSound)
        {
            if (pauseSoundtrackSound != null)
                PlayAudio(pauseSoundtrackSound);
        }

        //autoloop
        if (clip == gameSoundtrackSound || clip == pauseSoundtrackSound)
        {
            PlayAudio(clip);
        }

    }


    //We call this to stop our wave sounds
    void StopSounds()
    {
        waveSounds.Stop();
    }

    //This handler is called when transitioning to a wave
    void TransitionToWave()
    {
        CurWave = CurWave + 1;
        WaveEndSeconds = Time.time + LengthInSeconds;
        InPause = false;

        StopSounds();

        Debug.Log("Wave just started");

        if (waveStartSound != null)
        {
            PlayAudio(waveStartSound);
        }
        else
        {
            if (gameSoundtrackSound != null)
                PlayAudio(gameSoundtrackSound);
        }
    }

    //This handler is called when transitioning to a pause
    void TransitionToPause()
    {
        Debug.Log("Pause just started");
        PauseEndSeconds = Time.time + PauseInSeconds + (CurWave * PauseAddInSeconds);
        InPause = true;
        JustPaused = 2;

        //give extra HP, extra Mana, extra Cash

        StopSounds();



        if (waveEndSound != null)
        {

            PlayAudio(waveEndSound);
        }
        else
        {
            if (pauseSoundtrackSound != null)
                PlayAudio(pauseSoundtrackSound);
        }
    }

    //This handler is called every frame while in a pause
    void PauseHandler()
    {

        if(JustPaused> 0)
        {
            JustPaused = JustPaused - 1;
        }

        //Transition to new Wave
        if(Time.time > PauseEndSeconds)
        {
            TransitionToWave();
            return;
        }


    }

    //This handler is called every frame while in a wave
    void WaveHandler()
    {
        //Transition to pause
        if (Time.time > WaveEndSeconds)
        {
            TransitionToPause();
            return;
        }



    }

    //This handler is called when we lose ( our door is ded )
    void LoseHandler()
    {
        StopSounds();
        PlayAudio(waveLoseSound);

        lostGame = true;
        Debug.Log("You lost!");
        //some overlay to signalize that you've lost, gameover screen or w.e.

    }
}
