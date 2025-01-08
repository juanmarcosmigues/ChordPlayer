using UnityEngine;
using Game;
using System.Collections.Generic;

public class PianoRoll: MonoBehaviour 
{
    public static PianoRoll instance { get; private set; }


    protected PianoKey[] keys;
    protected int pianoOctavesSpan;
    protected int bottomOctave;

    protected List<int> evaluationKeyIndex = new List<int>();

    private void Awake()
    {
        instance = this;
        bottomOctave = -100;
    }

    private void Start()
    {
        keys = GetComponentsInChildren<PianoKey>();
        MidiInput.instance.onNotePressedDown += PressNote;
        MidiInput.instance.onNoteReleased += ReleaseNote;

        pianoOctavesSpan = Mathf.FloorToInt(keys.Length / 12f);
    }

    public void GetEvaluationKeyGroup (Chord c)
    {
        evaluationKeyIndex.Clear();

        for (int n = 0; n < keys.Length; n++)
        {
            keys[n].state = -1;
        }

        for (int k = 0; k < keys.Length; k++)
        {
            if (keys[k].note == c.notes[0])
            {
                for (int i = 0; i < c.intervals.Length; i++)
                {
                    if (k + c.intervals[i] >= keys.Length) break;

                    evaluationKeyIndex.Add(k + c.intervals[i]);
                    keys[k + c.intervals[i]].state = 1;
                }

                k += c.intervals[c.intervals.Length - 1];
            }
        }
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

    public void ShowChordMarkers (Chord chord)
    {
        int currentChordNote = 0;
        bool repeatingChord = false;

        for (int n = 0;n < keys.Length; n++)
        {
            if (keys[n].note == chord.notes[currentChordNote])
            {
                keys[n].ShowMarker(chord.degrees[currentChordNote], repeatingChord);
                currentChordNote++;

                if (currentChordNote >= chord.notes.Length)
                {
                    currentChordNote = 0;
                    repeatingChord = true;
                }
            }
        }
    }
    public void HideAllMarkers()
    {
        for (int n = 0; n < keys.Length; n++)
        {
            keys[n].HideMarker();
        }
    }
}

