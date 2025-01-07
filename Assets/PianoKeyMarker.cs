using UnityEngine;
using TMPro;

public class PianoKeyMarker : MonoBehaviour
{
    public GameObject root;
    public SpriteRenderer background;
    public TextMeshProUGUI markerText;

    [Header("Colors")]
    public Color defaultColor;
    public Color halfColor;

    public void ShowMarker (string text, bool halfColor = false)
    {
        root.SetActive (true);
        background.color = halfColor ? this.halfColor : defaultColor ;
        markerText.text = text;
    }
    public void HideMarker()
    {
        root.SetActive(false);
    }
}
