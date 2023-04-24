using System;
using UnityEngine;

public class Picture : MonoBehaviour
{
    public CardType type = CardType.Melon;
    public int pointValue = 1;
    public Sprite typePicture;
    public bool matched;
    public FlipCard flipCard;

    private void Start()
    {
        Application.targetFrameRate = 60;
        flipCard = GetComponent<FlipCard>();
        SetPointsAndSprite(type);
    }

    public Picture(CardType cardType) { SetPointsAndSprite(cardType); }

    public void SetPointsAndSprite(CardType cardType)
    {
        type = cardType;
        switch (type)
        {
            case CardType.Grape:
                pointValue = CardSettings.Instance.GetGrapePointValue();
                typePicture = CardSettings.Instance.GetGrapeSprite();
                break;
            case CardType.Apple:
                pointValue = CardSettings.Instance.GetApplePointValue();
                typePicture = CardSettings.Instance.GetAppleSprite();
                break;
            case CardType.Kiwi:
                pointValue = CardSettings.Instance.GetKiwiPointValue();
                typePicture = CardSettings.Instance.GetKiwiSprite();
                break;
            case CardType.Lemon:
                pointValue = CardSettings.Instance.GetLemonPointValue();
                typePicture = CardSettings.Instance.GetLemonSprite();
                break;
            case CardType.Melon:
                pointValue = CardSettings.Instance.GetMelonPointValue();
                typePicture = CardSettings.Instance.GetMelonSprite();
                break;
            case CardType.Orange:
                pointValue = CardSettings.Instance.GetOrangePointValue();
                typePicture = CardSettings.Instance.GetOrangeSprite();
                break;
            case CardType.Watermelon:
                pointValue = CardSettings.Instance.GetWatermelonPointValue();
                typePicture = CardSettings.Instance.GetWatermelonSprite();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        SetSprite();
    }

    private void SetSprite() { GetComponent<SpriteRenderer>().sprite = typePicture; }

    private void OnMouseDown()
    {
        if (PauseManager.Instance.isPaused) return;
        if (matched || !PictureManager.Instance.allowNewFlip) return;
        if (!flipCard.mayBeFlipped || !flipCard.cardBackIsActive) return;
        flipCard.StartFlip();
        PictureManager.Instance.Flip(this);
    }
}

public enum CardType
{
    Grape,
    Apple,
    Kiwi,
    Lemon,
    Melon,
    Orange,
    Watermelon
}