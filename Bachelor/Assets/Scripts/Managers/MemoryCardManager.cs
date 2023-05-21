using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryCardManager : MonoBehaviour
{
    public List<MemoryCard> cardList;
    public bool allowNewFlip = true;
    private static int _cards;
    private int _currentFlipped;
    private int _matchedAmount;
    private MemoryCard _flip1;
    private List<Question> _questions;
    private readonly List<string> _imageUrls = new ();
    private readonly List<Sprite> _images = new ();

    public static MemoryCardManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(MemoryCardManager)) as MemoryCardManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static MemoryCardManager _instance;

    private IEnumerator Start()
    {
        LevelManager.Instance.SetLoading(true);
        _questions = GameDataManager.Instance.GetGameData().ActiveLevel.Questions;
        if (_questions.Count == 0)
        {
            LevelManager.Instance.EndGame(0);
            yield break;
        }
        yield return GetImages();
        _cards = _questions.Count * 2;
        if (_cards > 12) _cards = 12;
        ScrambleListOrder();
        SetQuestionsToCards();
        LevelManager.Instance.SetLoading(false);
    }
    
    private IEnumerator GetImages()
    {
        foreach (Question question in _questions)
        {
            _imageUrls.Add(question.Option2);
            _imageUrls.Add(question.Option3);
        }
        yield return WebCommunicationUtil.GetImagesFromUrls(_images, _imageUrls);
    }

    private void ScrambleListOrder()
    {
        System.Random rand = new System.Random();
        cardList = cardList.OrderBy(_ => rand.Next()).ToList();
    }

    private void SetQuestionsToCards()
    {
        int count = 0;
        foreach (Question question in _questions)
        {
            if (_images[count] == null) cardList[count].SetTextAndQuestion(question, question.Option0);
            else cardList[count].SetImageAndQuestion(question, _images[count]);

            cardList[count].gameObject.SetActive(true);
            count++;
            
            if (_images[count] == null) cardList[count].SetTextAndQuestion(question, question.Option1);
            else cardList[count].SetImageAndQuestion(question, _images[count]);
            
            cardList[count].gameObject.SetActive(true);
            count++;
        }
    }

    public void Flip(MemoryCard card)
    {
        if (_flip1 == null)
        {
            _flip1 = card;
            return;
        }

        if (_flip1.cardQuestion == card.cardQuestion)
        {
            ScoreManager.Instance.ChangeScore(2);
            _matchedAmount += 2;
            _flip1.matched = true;
            card.matched = true;
            _flip1 = null;
            if (_matchedAmount == _cards) StartCoroutine(EndGame());
        }
        else StartCoroutine(FlipBack(card));
    }

    private static IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.EndGame(_cards);
    }

    private IEnumerator FlipBack(MemoryCard card)
    {
        allowNewFlip = false;
        yield return new WaitForSeconds(1.25f);
        _flip1.flipCard.StartFlip();
        card.flipCard.StartFlip();
        _flip1 = null;
        allowNewFlip = true;
    }
}
