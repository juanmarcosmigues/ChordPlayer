using UnityEngine;
using UnityEngine.UI;
using Game;
using TMPro;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI tmpChordName;

    void Start () {
        MainGame.instance.onChordChange += UpdateChord;
    }

    public void UpdateChord (Chord chord) {
        tmpChordName.text = chord.name;
    }
}
