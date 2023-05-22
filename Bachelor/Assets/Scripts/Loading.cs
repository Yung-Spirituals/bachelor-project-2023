using TMPro;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private RectTransform icon;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string loadingText;
    [SerializeField] private float timeStepIcon;
    [SerializeField] private float timeStepText;
    [SerializeField] private float oneStepAngle;

    private float _iconStartTime;
    private float _textStartTime;
    private int _textCount;

    private readonly string[] _dots = { ".", "..", "..." };

    private void Start()
    {
        _iconStartTime = Time.time;
        _textStartTime = Time.time;
        _textCount = 0;
    }

    // Animates the loading screen when the game object is active.
    private void Update()
    {
        if (!gameObject.activeSelf) return;
        if (Time.time - _iconStartTime >= timeStepIcon)
        {
            Vector3 iconAngle = icon.localEulerAngles;
            iconAngle.z -= oneStepAngle;
            icon.localEulerAngles = iconAngle;

            _iconStartTime = Time.time;
        }

        if (!(Time.time - _textStartTime >= timeStepText)) return;
        if (_textCount >= 3) _textCount = 0;
        text.text = loadingText + _dots[_textCount];
        _textCount++;

        _textStartTime = Time.time;
    }
}
