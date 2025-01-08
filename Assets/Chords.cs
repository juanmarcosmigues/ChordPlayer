using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game;

namespace Game 
{
    public enum Note 
    {C, Db, D, Eb, E, F, Gb, G, Ab, A, Bb, B}

    [System.Serializable]
    public struct Chord 
    {
        public Note rootNote;
        public string type;
        public Note[] notes;
        public string[] degrees;
        public int[] intervals;
        public string name;
        public int inversion;
        
        public Chord(Note rootNote, int type, int inversion) 
        {
            (string type, string[] degrees) 
                chordTypeData = ChordBuilder.ChordTypes[type];

            this.notes = ChordBuilder.GetChordNotes(rootNote, chordTypeData.degrees);
            this.type = chordTypeData.type;
            this.degrees = chordTypeData.degrees.Copy();
            this.intervals = ChordBuilder.degrees2Intervals(degrees);
            this.name = ChordBuilder.ChordToString(this.notes, chordTypeData.type);
            this.inversion = type < 13 ? inversion : 0;

            this.rootNote = notes[0];

            if (inversion > 0)
                ApplyInversion(inversion);
        }
        public void ApplyInversion (int note)
        {
            notes = notes.StartFrom(note);

            for (int i = 1; i < notes.Length; i++)
            {
                intervals[i] = ChordBuilder.getForwardInterval(notes[0], notes[i]);
                degrees[i] = ChordBuilder.Degree[intervals[i]];
            }

            name += "/" + notes[0];
        }
        public bool Contains (Note n)
        {
            for (int i = 0; i < notes.Length; i++)
                if (notes[i] == n) return true;

            return false;
        }
    }
}

public static class ChordBuilder
{      
    public static readonly string[] Degree = new string[] 
    {
        "1",
        "b2", "2",
        "b3", "3",
        "4",
        "b5", "5",
        "b6", "6",
        "b7", "7",
        "8",
        "b9", "9", //Equivalent to the 2nd
        "b10", "10",
        "11",  //Equivalent to the 4th
        "#11", "12",
        "b13", "13" //Equivalent to the 6th
    };

    public static readonly (string name, string[] degrees)[] ChordTypes = new (string, string[])[] 
    {
        ("Power", new string[]{"1", "5"}),

        ("Major", new string[]{"1", "3", "5"}),
        ("Minor", new string[]{"1", "b3", "5"}),
        ("Sus2", new string[]{"1", "2", "5"}),
        ("Sus4", new string[]{"1", "4", "5"}),
        ("Diminished", new string[]{"1", "b3", "b5"}),
        ("Augmented", new string[]{"1", "3", "b6"}), 
        
        //TRIAD CHORDS INDEX END = 6

        ("Major 6th", new string[]{"1", "3", "5", "6"}),
        ("Minor 6th", new string[]{"1", "b3", "5", "6"}),
        ("Dominant 7th", new string[]{"1", "3", "5", "b7"}),
        ("Major 7th", new string[]{"1", "3", "5", "7"}),
        ("Minor 7th", new string[]{"1", "b3", "5", "b7"}),
        ("Half Diminished 7th", new string[]{"1", "b3", "b5", "b7"}),
        ("Diminished 7th", new string[]{"1", "b3", "b5", "6"}),

        //SEVENTH CHORDS INDEX END = 13

        ("Dominant 9th", new string[]{"1", "3", "5", "b7", "9"}),
        ("Major 9th", new string[]{"1", "3", "5", "7", "9"}),
        ("Minor 9th", new string[]{"1", "b3", "5", "b7", "9"}),

        //9TH CHORDS INDEX END = 16

        ("Dominant 11th", new string[]{"1", "3", "5", "b7", "9", "11"}),
        ("Major 11th", new string[]{"1", "3", "5", "7", "9", "11"}),
        ("Minor 11th", new string[]{"1", "b3", "5", "b7", "9", "11"}),

        //11TH CHORDS INDEX END = 19

        ("Dominant 13th", new string[]{"1", "3", "5", "b7", "9", "11", "13"}),
        ("Major 13th", new string[]{"1", "3", "5", "7", "9", "11", "13"}),
        ("Minor 13th", new string[]{"1", "b3", "5", "b7", "9", "11", "13"})
    };

    public static int notesAmount => System.Enum.GetNames(typeof(Note)).Length;
    public static int clampNote (int interval) => 
    interval >= notesAmount 
    ? 
    interval - notesAmount
    : 
    interval;

    public static int degree2Interval (string degree) {
        for (int i = 0; i < Degree.Length; i++)
            if (Degree[i] == degree) return i;
        
        return -1;
    }
    public static int[] degrees2Intervals (params string[] degrees) {
        int[] intervals = new int[degrees.Length];
        for (int i = 0; i < intervals.Length; i++)
            intervals[i] = degree2Interval(degrees[i]);
        
        return intervals;
    }
    public static string[] getChordTypeDegrees(string type)
    {
        for (int i = 0; i < ChordTypes.Length; i++)
        {
            if (ChordTypes[i].name == type) return ChordTypes[i].degrees;
        }

        return null;
    }
    public static int getForwardInterval (Note from, Note to) {
        return (int)to-(int)from >= 0 ? (int)to-(int)from : (int)to-(int)from + notesAmount;
    }

    public static Chord PickRandomChord (int maxType, float inversionChance) 
    {
        Note rootNote = (Note)Random.Range(0, 12);
        int typeIndex = Random.Range(0, maxType+1);
        int inversion =
            Random.Range(0f, 1f) < inversionChance
            ?
            Random.Range(1, ChordTypes[typeIndex].degrees.Length)
            :
            0;

        return GetChord(rootNote, typeIndex, inversion);
    }

    public static Chord GetChord (Note note, int type, int inversion)
    {
        return new Chord(note, type, type < 13 ? inversion : 0);
    }

    public static string ChordToString (Note[] notes, string type) {
        string c = notes[0].ToString() + " " + type;
        //for (int n = 0; n < notes.Length; n++) c += notes[n] + " ";
        return c;
    }
    public static Note[] GetChordNotes (Note root, params string[] degrees) {
        int[] intervals = degrees2Intervals(degrees);
        Note[] chord = new Note[intervals.Length];
        
        for (int i = 0; i < chord.Length; i++) 
        {
            int currentInterval = clampNote(intervals[i]);

            chord[i] = (Note)clampNote((int)root + currentInterval);

            if (i >= intervals.Length) break;
        }

        return chord;
    }
}
