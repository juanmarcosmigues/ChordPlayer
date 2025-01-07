using UnityEngine;
using Game;

public class MainGame : MonoBehaviour
{
    public static MainGame instance {get; private set;}
    public float bpm = 100f;
    
    protected float beatDuration = 0f;
    protected float barDuration = 0f;

    protected bool first;
    protected int currentBeat;
    protected float currentBarTime;
    protected float currentBeatTime;

    public Chord nextChord {  get; private set; }
    public Chord currentChord {get; private set;}
    public float currentBeatTimeNormalized => currentBeatTime / beatDuration;
    public float currentBarTimeNormalized => currentBarTime / barDuration;

    public event System.Action<Chord> onChordChange;
    public event System.Action<Chord> onNextChordChange;
    public event System.Action onBar;
    public event System.Action<int> onBeat;

    void Awake () {
        instance = this;
        beatDuration = 60f/bpm;
        barDuration = beatDuration * 4f;
        currentBeat = 1;
        first = true;
    }

    private void Start()
    {
        UpdateNextChord(ChordBuilder.PickRandomChord());
    }
    void Update () {
        currentBeatTime += Time.deltaTime;
        currentBarTime += Time.deltaTime;

        if (currentBeatTime > beatDuration) {
            currentBeatTime -= beatDuration;
            Beat();
        }
    }

    public void Beat ()
    {
        currentBeat++;
   
        if (currentBeat > 4)
        {
            currentBeat = 1;
            Bar();
        }

        onBeat?.Invoke(currentBeat);
    }
    public void Bar () 
    {
        currentBarTime = currentBeatTime;

        UpdateChord(ChordBuilder.PickRandomChord());
        onBar?.Invoke();
    }

    public void UpdateNextChord (Chord newChord)
    {
        nextChord = newChord;
        onNextChordChange?.Invoke(newChord);
    }
    public void UpdateChord (Chord newChord) 
    {
        currentChord = nextChord;

        UpdateNextChord(newChord);

        PianoRoll.instance.HideAllMarkers();
        PianoRoll.instance.ShowChordMarkers(currentChord);
        PianoRoll.instance.GetEvaluationKeyGroup(currentChord);
        
        onChordChange?.Invoke(currentChord);
    }
}
