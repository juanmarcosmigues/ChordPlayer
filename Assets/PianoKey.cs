using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public Chords.Note note;
    public PianoRollKeySettings settings;

    protected MeshRenderer mesh;
    protected Transform keyTransform;
    protected Color originalColor;

    protected bool pressed;
    protected float pressedValue;

    protected static readonly Quaternion pressedRotation = new Quaternion(-0.0305385f, 0, 0, 0.9995336f);

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        keyTransform = mesh.transform;
        originalColor = mesh.material.GetColor("_Color");
    }

    private void Update()
    {
        if (pressed)
        {
            pressedValue = Mathf.Clamp01(pressedValue + settings.pressFeedbackSpeed * Time.deltaTime);
        }
        else
        {
            pressedValue = Mathf.Clamp01(pressedValue - settings.releaseFeedbackSpeed * Time.deltaTime);
        }

        keyTransform.rotation = Quaternion.Lerp(Quaternion.identity, pressedRotation, pressedValue);
        mesh.material.SetColor("_Color", Color.Lerp(originalColor, settings.InteractiveColor, pressedValue));
    }

    public void Press ()
    {
        pressed = true;
    }
    public void Release ()
    {
        pressed = false;
    }
}
