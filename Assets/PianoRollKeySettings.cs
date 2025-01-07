using UnityEngine;

[CreateAssetMenu(fileName = "PianoRollKeySettings", menuName = "Scriptable Objects/PianoRollKeySettings")]
public class PianoRollKeySettings : ScriptableObject
{
    public Color InteractiveColor;
    public Color positiveColor;
    public Color negativeColor;
    public float pressFeedbackSpeed;
    public float releaseFeedbackSpeed;
}
