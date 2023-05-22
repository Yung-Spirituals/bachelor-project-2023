using TMPro;
using UnityEngine;

// Class responsible for setting the text content of simple popups.
public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI textTextMeshProUGUI;

    // Set the displayed title and text body of the popup.
    public void Display(string title, string text)
    {
        titleTextMeshProUGUI.text = title;
        textTextMeshProUGUI.text = text;
        gameObject.SetActive(true);
    }
}