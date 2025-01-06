﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiInput : MonoBehaviour
{
    public static MidiInput instance { get; private set; }

    public bool inputEnabled = true;

    public System.Action<Chords.Note, int> onNotePressedDown, onNoteReleased;

    protected InputMidi midiInput;
    //ia = input action
    protected InputAction iaActionC;
    protected InputAction iaActionDb;
    protected InputAction iaActionD;
    protected InputAction iaActionEb;
    protected InputAction iaActionE;
    protected InputAction iaActionF;
    protected InputAction iaActionGb;
    protected InputAction iaActionG;
    protected InputAction iaActionAb;
    protected InputAction iaActionA;
    protected InputAction iaActionBb;
    protected InputAction iaActionB;

    private void Awake()
    {
        instance = this;
        midiInput = new InputMidi();
    }
    protected virtual void OnEnable()
    {
        iaActionC = midiInput.Notes.C;
        iaActionDb = midiInput.Notes.Db;
        iaActionD = midiInput.Notes.D;
        iaActionEb = midiInput.Notes.Eb;
        iaActionE = midiInput.Notes.E;
        iaActionF = midiInput.Notes.F;
        iaActionGb = midiInput.Notes.Gb;
        iaActionG = midiInput.Notes.G;
        iaActionAb = midiInput.Notes.Ab;
        iaActionA = midiInput.Notes.A;
        iaActionBb = midiInput.Notes.Bb;
        iaActionB = midiInput.Notes.B;


        iaActionC.Enable();
        iaActionDb.Enable();
        iaActionD.Enable();
        iaActionEb.Enable();
        iaActionE.Enable();
        iaActionF.Enable();
        iaActionGb.Enable();
        iaActionG.Enable();
        iaActionAb.Enable();
        iaActionA.Enable();
        iaActionBb.Enable();
        iaActionB.Enable();
    }
    protected virtual void OnDisable()
    {
        midiInput.Disable();
    }
    protected virtual void Start()
    {
        iaActionC.started += 
            _ => onNotePressedDown?.Invoke(Chords.Note.C, _.action.GetBindingIndexForControl(_.control));
        iaActionC.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.C, _.action.GetBindingIndexForControl(_.control));

        iaActionDb.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.Db, _.action.GetBindingIndexForControl(_.control));
        iaActionDb.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.Db, _.action.GetBindingIndexForControl(_.control));

        iaActionD.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.D, _.action.GetBindingIndexForControl(_.control));
        iaActionD.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.D, _.action.GetBindingIndexForControl(_.control));

        iaActionEb.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.Eb, _.action.GetBindingIndexForControl(_.control));
        iaActionEb.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.Eb, _.action.GetBindingIndexForControl(_.control));

        iaActionE.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.E, _.action.GetBindingIndexForControl(_.control));
        iaActionE.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.E, _.action.GetBindingIndexForControl(_.control));

        iaActionF.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.F, _.action.GetBindingIndexForControl(_.control));
        iaActionF.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.F, _.action.GetBindingIndexForControl(_.control));

        iaActionGb.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.Gb, _.action.GetBindingIndexForControl(_.control));
        iaActionGb.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.Gb, _.action.GetBindingIndexForControl(_.control));

        iaActionG.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.G, _.action.GetBindingIndexForControl(_.control));
        iaActionG.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.G, _.action.GetBindingIndexForControl(_.control));

        iaActionAb.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.Ab, _.action.GetBindingIndexForControl(_.control));
        iaActionAb.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.Ab, _.action.GetBindingIndexForControl(_.control));

        iaActionA.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.A, _.action.GetBindingIndexForControl(_.control));
        iaActionA.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.A, _.action.GetBindingIndexForControl(_.control));

        iaActionBb.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.Bb, _.action.GetBindingIndexForControl(_.control));
        iaActionBb.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.Bb, _.action.GetBindingIndexForControl(_.control));

        iaActionB.started +=
            _ => onNotePressedDown?.Invoke(Chords.Note.B, _.action.GetBindingIndexForControl(_.control));
        iaActionB.canceled +=
            _ => onNoteReleased?.Invoke(Chords.Note.B, _.action.GetBindingIndexForControl(_.control));
    }
}
