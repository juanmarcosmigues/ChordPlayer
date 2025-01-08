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
        // int d = (int)c.rootNote + c.notesSpan * pianoOctavesSpan;

        evaluationKeyIndex.Clear();

        int chordOctavesLength = c.notesIntervals[c.notesIntervals.Length-1] > 12 ? 2 : 1;

        for (int k = 0; k < keys.Length; k++)
        {
            if (keys[k].note == c.rootNote)
            {
                //if (chordOctavesLength) working here
                for (int i = 0; i < c.notesIntervals.Length; i++)
                    evaluationKeyIndex.Add(k + c.notesIntervals[i]);
            }
        }

        // for (int n = 0; n < keys.Length; n++)
        // {
        //     keys[0].state = 0;
        // }
        // foreach (var item in evaluationKeyIndex)
        // {
        //     keys[item].state = 1;
        // }
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
                keys[n].ShowMarker(chord.notesDegrees[currentChordNote], repeatingChord);
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

