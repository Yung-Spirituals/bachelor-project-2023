using UnityEngine;

public class CardSettings : MonoBehaviour
{
    [Range(1,7)][SerializeField] private int numberOfTypes = 1;
    [SerializeField] private int rowsOfCards = 1;
    [SerializeField] private int columnsOfCards = 1;
    
    [SerializeField] private Sprite grapeSprite;
    [SerializeField] private Sprite appleSprite;
    [SerializeField] private Sprite kiwiSprite;
    [SerializeField] private Sprite lemonSprite;
    [SerializeField] private Sprite melonSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite watermelonSprite;
    
    [SerializeField] private int grapePointValue = 1;
    [SerializeField] private int applePointValue = 1;
    [SerializeField] private int kiwiPointValue = 1;
    [SerializeField] private int lemonPointValue = 1;
    [SerializeField] private int melonPointValue = 1;
    [SerializeField] private int orangePointValue = 1;
    [SerializeField] private int watermelonPointValue = 1;

    public static CardSettings Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(CardSettings)) as CardSettings;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static CardSettings instance;

    public int GetNumberOfTypes() { return numberOfTypes; }
    public int GetNumberOfCardRows() { return rowsOfCards; }
    public int GetNumberOfCardColumns() { return columnsOfCards; }

    public Sprite GetGrapeSprite() { return grapeSprite; }
    public Sprite GetAppleSprite() { return appleSprite; }
    public Sprite GetKiwiSprite() { return kiwiSprite; }
    public Sprite GetLemonSprite() { return lemonSprite; }
    public Sprite GetMelonSprite() { return melonSprite; }
    public Sprite GetOrangeSprite() { return orangeSprite; }
    public Sprite GetWatermelonSprite() { return watermelonSprite; }

    public int GetGrapePointValue() { return grapePointValue; }
    public int GetApplePointValue() { return applePointValue; }
    public int GetKiwiPointValue() { return kiwiPointValue; }
    public int GetLemonPointValue() { return lemonPointValue; }
    public int GetMelonPointValue() { return melonPointValue; }
    public int GetOrangePointValue() { return orangePointValue; }
    public int GetWatermelonPointValue() { return watermelonPointValue; }
}
