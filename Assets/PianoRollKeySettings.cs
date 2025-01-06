using UnityEngine;

[CreateAssetMenu(fileName = "PianoRollKeySettings", menuName = "Scriptable Objects/PianoRollKeySettings")]
public class PianoRollKeySettings : ScriptableObject
{
    public Color InteractiveColor;
    public float pressFeedbackSpeed;
    public float releaseFeedbackSpeed;
}
