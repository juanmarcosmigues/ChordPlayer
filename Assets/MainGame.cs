using UnityEngine;
using Game;

public class MainGame : MonoBehaviour
{
    public static MainGame instance {get; private set;}
    public float bpm = 100f;
    
    protected float beatDuration = 0f;

    protected int currentBeat;
    protected float currentBeatTime;
    public Chord currentChord {get; private set;}

    public event System.Action<Chord> onChordChange;

    void Awake () {
        instance = this;
        beatDuration = 60f/bpm;
        currentBeat = 1;
    }

    void Update () {
        currentBeatTime += Time.deltaTime;
        if (currentBeatTime > beatDuration) {
            currentBeatTime -= beatDuration;
            currentBeat++;
            if (currentBeat > 4) {
                currentBeat = 1;
                Bar();
            }
        }
    }

    public void Bar () {
        UpdateChord(ChordBuilder.PickRandomChord());
        Debug.Log(currentChord.name);
    }

    public void UpdateChord (Chord newChord) {
        currentChord = newChord;
        onChordChange?.Invoke(newChord);
    }
}
