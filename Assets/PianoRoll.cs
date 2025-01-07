using UnityEngine;
using Game;

public class PianoRoll: MonoBehaviour 
{
    protected PianoKey[] keys;
    protected int pianoOctavesSpan;
    protected int bottomOctave;

    private void Awake()
    {
        bottomOctave = -100;
    }

    private void Start()
    {
        keys = GetComponentsInChildren<PianoKey>();
        MidiInput.instance.onNotePressedDown += PressNote;
        MidiInput.instance.onNoteReleased += ReleaseNote;

        pianoOctavesSpan = Mathf.FloorToInt(keys.Length / 12f);
    }

    public PianoKey GetKey(Note note, int octave = 0) 
    {
        int currentOctave = 0;
        int currentKey = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            if (i >= 12)
            {
                i = 0;
                currentOctave += 1;
            }

            if (currentOctave < octave) continue;

            currentKey = i + (12 * currentOctave);

            if (currentKey >= keys.Length) break;

            if (keys[currentKey].note == note) return keys[currentKey];
        }

        return null;
    }
    public int GetProjectedOctave (int octave)
    {

        if (bottomOctave < -1)
        {
            bottomOctave = octave;
        }
        else if (bottomOctave > octave)
        {
            bottomOctave = octave;
        }

        if (octave - bottomOctave >= pianoOctavesSpan)
        {
            bottomOctave = octave - pianoOctavesSpan;
            bottomOctave += 1;
        }

        return octave - bottomOctave;
    }
    public void PressNote (Note note, int octave)
    {
        GetKey(note, GetProjectedOctave(octave)).Press();
        Debug.Log(octave);
    }
    public void ReleaseNote(Note note, int octave)
    {
        GetKey(note, GetProjectedOctave(octave)).Release();
        Debug.Log(octave);
    }
}

