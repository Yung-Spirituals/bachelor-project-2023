using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{ 
    [SerializeField] private TMPro.TextMeshProUGUI cardText;
    [SerializeField] private Image cardImage;
    public bool matched;
    public FlipCard flipCard;
    public Question cardQuestion;

    public void SetTextAndQuestion(Question question, string text)
    {
        cardQuestion = question;
        cardText.text = text;
        cardImage.gameObject.SetActive(false);
        cardText.gameObject.SetActive(true);
    }
    
    public void SetImageAndQuestion(Question question, Sprite sprite)
    {
        cardQuestion = question;
        cardImage.sprite = sprite;
        cardImage.gameObject.SetActive(true);
        cardText.gameObject.SetActive(false);
    }

    public void StartFlip()
    {
        if (PauseManager.Instance.isPaused) return;
        if (matched || !MemoryCardManager.Instance.allowNewFlip) return;
        if (!flipCard.mayBeFlipped || !flipCard.cardBackIsActive) return;
        flipCard.StartFlip();
        MemoryCardManager.Instance.Flip(this);
    }
}