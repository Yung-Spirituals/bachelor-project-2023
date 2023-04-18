using UnityEngine;

public class CardSettings : MonoBehaviour
{
    [Range(1,7)][SerializeField] private int numberOfTypes = 1;
    [SerializeField] private int rowsOfCards = 1;
    [SerializeField] private int columnsOfCards = 1;
    
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite typeOneSprite;
    [SerializeField] private Sprite typeTwoSprite;
    [SerializeField] private Sprite typeThreeSprite;
    [SerializeField] private Sprite typeFourSprite;
    [SerializeField] private Sprite typeFiveSprite;
    [SerializeField] private Sprite typeSixSprite;
    [SerializeField] private Sprite typeSevenSprite;
    
    private int typeOnePointValue = 1;
    private int typeTwoPointValue = 1;
    private int typeThreePointValue = 1;
    private int typeFourPointValue = 1;
    private int typeFivePointValue = 1;
    private int typeSixPointValue = 1;
    private int typeSevenPointValue = 1;

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

    public Sprite GetCardBackSprite() { return cardBackSprite; }
    public Sprite GetGrapeSprite() { return typeOneSprite; }
    public Sprite GetAppleSprite() { return typeTwoSprite; }
    public Sprite GetKiwiSprite() { return typeThreeSprite; }
    public Sprite GetLemonSprite() { return typeFourSprite; }
    public Sprite GetMelonSprite() { return typeFiveSprite; }
    public Sprite GetOrangeSprite() { return typeSixSprite; }
    public Sprite GetWatermelonSprite() { return typeSevenSprite; }

    public int GetGrapePointValue() { return typeOnePointValue; }
    public int GetApplePointValue() { return typeTwoPointValue; }
    public int GetKiwiPointValue() { return typeThreePointValue; }
    public int GetLemonPointValue() { return typeFourPointValue; }
    public int GetMelonPointValue() { return typeFivePointValue; }
    public int GetOrangePointValue() { return typeSixPointValue; }
    public int GetWatermelonPointValue() { return typeSevenPointValue; }
}
