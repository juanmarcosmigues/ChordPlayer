using UnityEngine;
using Game;

public class PianoKey : MonoBehaviour
{
    public Note note;
    public PianoRollKeySettings settings;
    public PianoKeyMarker marker;

    public int state;

    protected MeshRenderer mesh;
    protected Transform keyTransform;
    protected Color originalColor;

    protected bool pressed;
    protected float pressedValue;

    protected static readonly Quaternion pressedRotation = new Quaternion(-0.0305385f, 0, 0, 0.9995336f);

    private void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        keyTransform = mesh.transform;
        originalColor = mesh.material.GetColor("_Color");
    }

    private void Start()
    {
        marker.HideMarker();
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
        mesh.material.SetColor("_Color", Color.Lerp(originalColor
            , state == 0 ? settings.InteractiveColor : state == 1 ? settings.positiveColor : settings.negativeColor
            , 1));
    }

    public void Press ()
    {
        pressed = true;
    }
    public void Release ()
    {
        pressed = false;
    }

    public void ShowMarker (string text, bool halfColor = false)=>
        marker.ShowMarker(text, halfColor);
    public void HideMarker()=>
        marker.HideMarker();
}
