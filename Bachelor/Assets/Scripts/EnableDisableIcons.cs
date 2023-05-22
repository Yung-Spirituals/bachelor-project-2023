using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for switching between two icons.
 */
public class EnableDisableIcons : MonoBehaviour
{
    public bool isEnabled;
    public Image buttonIcon;
    public Sprite enableIcon;
    public Sprite disableIcon;

    // Switches between which sprite is shown. 
    public void ButtonClicked()
    {
        isEnabled = !isEnabled;
        buttonIcon.sprite = isEnabled ? enableIcon : disableIcon;
    }

    // Selects which sprite is shown.
    public void SetActive(bool active)
    {
        isEnabled = active;
        buttonIcon.sprite = isEnabled ? enableIcon : disableIcon;
    }

    // Sets the active state of the game object that holds the buttonIcon
    public void SetGameObjectActive(bool active)
    {
        buttonIcon.gameObject.SetActive(active);
    }
}
