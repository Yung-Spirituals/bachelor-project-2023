using UnityEngine;
using UnityEngine.UI;

public class EnableDisableIcons : MonoBehaviour
{
    public bool isEnabled;
    public Image buttonIcon;
    public Sprite enableIcon;
    public Sprite disableIcon;

    public void ButtonClicked()
    {
        isEnabled = !isEnabled;
        buttonIcon.sprite = isEnabled ? enableIcon : disableIcon;
    }
}
