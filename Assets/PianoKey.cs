using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public Chords.Note note;
    public PianoRollKeySettings settings;

    protected MeshRenderer mesh;
    protected Transform keyTransform;
    protected Timestamp pressedTime;
    protected Timestamp releaseTime;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        keyTransform = mesh.transform;
    }

    private void Update()
    {
        
    }

    public void Press ()
    {
        //keyTransform.rotation
    }
}
