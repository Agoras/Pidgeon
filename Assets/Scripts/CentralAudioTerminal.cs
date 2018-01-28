using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CentralAudioTerminal : Singleton<CentralAudioTerminal>
{
    // tunable REP values for driving transitions UP and DOWN
    public int repZero = 1;
    public int repLow = 20;
    public int repMid = 30;
    public int repHigh = 40;
    public int repUltra = 50;

    int previousRep = 0;
    int currentRep = 0;

    GameManagerThing gmt;

    enum Ferocity
    {
        ZERO = 0,
        LOW = 1,
        MID = 2,
        HIGH = 3,
        ULTRA = 4
    };
    Ferocity currentFerocity = Ferocity.ZERO;

    int minFerocityIndex = (int)Ferocity.ZERO;
    int maxFerocityIndex = (int)Ferocity.ULTRA;

    //Object[] myMusic;
    public AudioClip intro;
    //public AudioClip rampUp;
    public AudioClip[] ferocityLevel;
    public AudioClip[] transitionType;
    
    //public AudioClip[] breaks;

    private AudioClip currentClip;
    private AudioClip nextClip;

    public AudioSource[] audioChannel;
    private AudioSource currentChannel;
    private AudioSource nextChannel;
    private AudioSource previousChannel;

    //public bool loop = true;

    private float timer;
    private float currentClipLength;
    private float nextClipLength;

    //private int iterator;
    public bool gameOver = false;

    public override void Awake()
    {
        gmt = GameObject.FindObjectOfType<GameManagerThing>();
        if (gmt == null)
        {
            Debug.Log("Shit's on Fire, yo...");
        }

        currentChannel = audioChannel[0];
        currentChannel.loop = true;
        nextChannel = audioChannel[1];
        nextChannel.loop = true;

        currentChannel.clip = intro;
        currentClipLength = currentChannel.clip.length;
        currentClip = currentChannel.clip;

        //nextChannel.clip = GetNextClip(currentClip); //rampUp;
        nextChannel.clip = transitionType[0]; //rampUp;
        nextClipLength = nextChannel.clip.length;
        nextClip = nextChannel.clip; //currentChannel.clip;

        base.Awake();
    }

    void Start()
    {
        //audio.Play();
        audioChannel[0].Play();
    }

    void Update()
    {
        //currentRep = gmt.pigeonRep;
        if (!gameOver && ferocityLevel.Length > 0 && transitionType.Length > 0) // double bagged
        {
            // Increase timer with the time difference between this and the previous frame:
            timer += Time.deltaTime;
            currentRep = gmt.pigeonRep;

            if (timer >= currentClipLength) // * 2.0f)
            {
                currentChannel.Stop();
                nextChannel.Play();
                currentClipLength = nextClipLength;
                timer = 0;

                //JuggleChannels prev/current/next
                previousChannel = currentChannel;

                currentChannel = nextChannel;
                currentClip = nextClip;
                //currentAudioClipLength = nextAudioClipLength;

                nextChannel = previousChannel;
                SetNextClip();
                nextChannel.clip = nextClip;
                nextClipLength = nextClip.length;
            }

            previousRep = currentRep;
            Debug.Log("current: " + currentRep.ToString() + ", previous: " + previousRep.ToString());
        }

        if (gameOver)
        {
            StopAllChannels();
            GameObject.Destroy(this.gameObject);
        }
    }

    // TODO : change this from RANDOM selection to some systematic hookup to the thing
    void SetNextClip()
    {
        //Debug.Log("SetNextClip Begin");
        nextClip = GetNextClip(currentClip); //chunks[Random.Range(0, chunks.Length)] as AudioClip;
    }

    AudioClip GetNextClip(AudioClip curClip)
    {
        //Debug.Log("GetNextClip Begin");
        AudioClip tempClip = new AudioClip();
        if (curClip != null)
        {
            switch (currentFerocity)
            {
                case Ferocity.ZERO:
                    Debug.Log("ZERO FEROCITY DETECTED");
                    tempClip = CheckedTransitionStateClip(curClip, repZero);
                    break;

                case Ferocity.LOW:
                    Debug.Log("LOW FEROCITY DETECTED");
                    tempClip = CheckedTransitionStateClip(curClip, repLow);
                    break;

                case Ferocity.MID:
                    Debug.Log("MID FEROCITY DETECTED");
                    tempClip = CheckedTransitionStateClip(curClip, repMid);
                    break;

                case Ferocity.HIGH:
                    Debug.Log("HIGH FEROCITY DETECTED");
                    tempClip = CheckedTransitionStateClip(curClip, repHigh);
                    break;

                case Ferocity.ULTRA:
                    Debug.Log("ULTRA FEROCITY DETECTED");
                    tempClip = CheckedTransitionStateClip(curClip, repUltra);
                    break;

                default:
                    Debug.Log("Fuck Yo Couch");
                    break;
            }
        }
        else
        {
            Debug.Log("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFUCK");
        }
        return tempClip;
    }

    AudioClip CheckedTransitionStateClip(AudioClip curClip, int rep)
    {
        int tempIndex = (int)currentFerocity;

        if (ThisClipIsTransition(curClip))
        {
            if (currentRep > previousRep && currentRep >= rep)
            {
                tempIndex = tempIndex++;
                if (tempIndex > maxFerocityIndex)
                    tempIndex = maxFerocityIndex;
                //Debug.Log("Transition To GREATER FEROCITY");
                return ferocityLevel[tempIndex];
            }
            else if (currentRep < previousRep && currentRep <= rep)
            {
                Debug.Log("Transition To LOWER Ferocity");
                tempIndex = tempIndex--;
                if (tempIndex < minFerocityIndex)
                    tempIndex = minFerocityIndex;
                //Debug.Log("Transition To GREATER FEROCITY");
                return ferocityLevel[tempIndex];
            }
            else
            {
                return ferocityLevel[tempIndex];
                //Debug.Log("Remain At Current Ferocity");
            }
        }
        else
        {
            Debug.Log("Playing TRANSITION_ZERO for DEBUG");
            //tempIndex = (int)currentFerocity;
            
        }
        return transitionType[tempIndex];
    }

    bool ThisClipIsTransition(AudioClip clip)
    {
        Debug.Log("We're looking at:" + clip.name);
        return clip.name.Contains(("TRANSITION").ToLower());
    }

    public void StopAllChannels()
    {
        currentChannel.Stop();
        nextChannel.Stop();
        if (previousChannel != null)
        {
            previousChannel.Stop();
        }
    }
}