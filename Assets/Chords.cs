using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chords : MonoBehaviour
{
    public enum Note {C, Db, D, Eb, E, F, Gb, G, Ab, A, Bb, B}
      
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

        ("Major 6th", new string[]{"1", "3", "5", "6"}),
        ("Minor 6th", new string[]{"1", "b3", "5", "6"}),
        ("Dominant 7th", new string[]{"1", "3", "5", "b7"}),
        ("Major 7th", new string[]{"1", "3", "5", "7"}),
        ("Minor 7th", new string[]{"1", "b3", "5", "b7"}),
        ("Half Diminished 7th", new string[]{"1", "b3", "b5", "b7"}),
        ("Diminished 7th", new string[]{"1", "b3", "b5", "6"}),

        ("Dominant 9th", new string[]{"1", "3", "5", "b7", "9"}),
        ("Major 9th", new string[]{"1", "3", "5", "7", "9"}),
        ("Minor 9th", new string[]{"1", "b3", "5", "b7", "9"}),

        ("Dominant 11th", new string[]{"1", "3", "5", "b7", "9", "11"}),
        ("Major 11th", new string[]{"1", "3", "5", "7", "9", "11"}),
        ("Minor 11th", new string[]{"1", "b3", "5", "b7", "9", "11"}),

        ("Dominant 13th", new string[]{"1", "3", "5", "b7", "9", "11", "13"}),
        ("Major 13th", new string[]{"1", "3", "5", "7", "9", "11", "13"}),
        ("Minor 13th", new string[]{"1", "b3", "5", "b7", "9", "11", "13"})
    };

    public int notesAmount => System.Enum.GetNames(typeof(Note)).Length;
    public int clampNote (int interval) => 
    interval >= notesAmount 
    ? 
    interval - notesAmount
    : 
    interval;
    public int degree2Interval (string degree) {
        for (int d = 0; d < Degree.Length; d++)
            if (Degree[d] == degree) return d;
        
        return -1;
    }
    public int[] degrees2Intervals (params string[] degrees) {
        int[] intervals = new int[degrees.Length];
        for (int i = 0; i < intervals.Length; i++)
            intervals[i] = degree2Interval(degrees[i]);
        
        return intervals;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            (string name, string[] degrees) chordType = ChordTypes[Random.Range(0, ChordTypes.Length)];
            Note rootNote = (Note)Random.Range(0, 12);
            var chord = GetChord(rootNote, chordType.degrees);
            string c = rootNote.ToString() + " " + chordType.name + " -> ";
            for (int n = 0; n < chord.Length; n++)
            c += chord[n] + " ";
            
            Debug.Log(c);
        }
    } 
    public Note[] GetChord (Note root, params string[] degrees) {
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
