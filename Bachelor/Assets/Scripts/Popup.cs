using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI textTextMeshProUGUI;

    public void Display(string title, string text)
    {
        titleTextMeshProUGUI.text = title;
        textTextMeshProUGUI.text = text;
        gameObject.SetActive(true);
    }
}