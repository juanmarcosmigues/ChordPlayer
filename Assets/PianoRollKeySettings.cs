using UnityEngine;

[CreateAssetMenu(fileName = "PianoRollKeySettings", menuName = "Scriptable Objects/PianoRollKeySettings")]
public class PianoRollKeySettings : ScriptableObject
{
    public Color naturalInteractiveColor;
    public Color accidentalInteractiveColor;
    public AnimationCurve pressCurve;
    public float pressTime;
    public float releaseTime;
}
