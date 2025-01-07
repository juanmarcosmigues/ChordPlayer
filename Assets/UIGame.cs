using UnityEngine;
using UnityEngine.UI;
using Game;
using TMPro;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI tmpChordName;
    public TextMeshProUGUI tmpBeat;
    public TextMeshProUGUI tmpNextChord;
    public Transform axisBeat;
    public Transform axisBar;

    void Start () {
        MainGame.instance.onChordChange += UpdateChord;
        MainGame.instance.onBeat += UpdateBeat;
        MainGame.instance.onNextChordChange += UpdateNextChord; 
    }

    void Update ()
    {
        axisBeat.rotation = Quaternion.Euler(0f, 0f, 360f * MainGame.instance.currentBeatTimeNormalized);
        axisBar.rotation = Quaternion.Euler(0f, 0f, 360f * MainGame.instance.currentBarTimeNormalized);
    }

    public void UpdateBeat (int beat)
    {
        tmpBeat.text = beat + "/4";
    }
    public void UpdateChord (Chord chord) {
        tmpChordName.text = chord.name;
    }
    public void UpdateNextChord(Chord chord)
    {
        tmpNextChord.text = "NEXT: " + chord.name;
    }
}
