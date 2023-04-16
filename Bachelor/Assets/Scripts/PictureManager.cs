using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector2 StartPosition = new (-2.15f, 3.62f);
    public List<Picture> PictureList;
    public bool allowNewFlip = true;
    
    private Vector2 _offset = new (1.5f, 1.52f);
    private int cards;
    private int currentFlipped;
    private int matchedAmount = 0;
    private Picture flip1;
    
    public static PictureManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(PictureManager)) as PictureManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static PictureManager instance;

    private void Start()
    {
        int rows = CardSettings.Instance.GetNumberOfCardRows();
        int columns = CardSettings.Instance.GetNumberOfCardColumns();
        cards = rows * columns;
        PictureList = new List<Picture>();
        SpawnPictureMesh(columns, rows, StartPosition, _offset, false);
        ScramblePictureListOrder();
        MovePicture(columns, rows, StartPosition, _offset);
    }

    private void ScramblePictureListOrder()
    {
        System.Random rand = new System.Random();
        PictureList = PictureList.OrderBy(_ => rand.Next()).ToList();
    }

    private void SpawnPictureMesh(int rows, int columns, Vector2 pos, Vector2 offset, bool scaleDown)
    {
        int count = 0;
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                Picture tempPicture = Instantiate(PicturePrefab, PicSpawnPosition.position,
                    PicSpawnPosition.transform.rotation);

                tempPicture.name = tempPicture.name + "c" + col + "r" + row;
                SetPictureType(tempPicture, count);
                PictureList.Add(tempPicture);
                count++;
                if (count >= CardSettings.Instance.GetNumberOfTypes() * 2) count = 0;
            }
        }
    }

    private void SetPictureType(Picture picture, int count)
    {
        switch (count)
        {
            case < 2:
                picture.SetPointsAndSprite(CardType.Kiwi);
                break;
            case < 4:
                picture.SetPointsAndSprite(CardType.Grape);
                break;
            case < 6:
                picture.SetPointsAndSprite(CardType.Melon);
                break;
            case < 8:
                picture.SetPointsAndSprite(CardType.Apple);
                break;
            case < 10:
                picture.SetPointsAndSprite(CardType.Lemon);
                break;
            case < 12:
                picture.SetPointsAndSprite(CardType.Orange);
                break;
            case < 14:
                picture.SetPointsAndSprite(CardType.Watermelon);
                break;
        }
    }

    private void MovePicture(int rows, int columns, Vector2 pos, Vector2 offset)
    {
        int index = 0;
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                Vector2 targetPosition = new ((pos.x + (offset.x * row)), (pos.y - (offset.y * col)));
                StartCoroutine(MoveToPosition(targetPosition, PictureList[index]));
                index++;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        const int randomDistance = 7;

        while (obj.transform.position != target)
        {
            obj.transform.position =
                Vector2.MoveTowards(obj.transform.position, target, randomDistance * Time.deltaTime);
            yield return null;
        }
    }
    
    public void Flip(Picture picture)
    {
        if (flip1 == null)
        {
            flip1 = picture;
            return;
        }

        if (flip1.type == picture.type)
        {
            ScoreManager.Instance.ChangeScore(2);
            matchedAmount += 2;
            flip1.matched = true;
            picture.matched = true;
            flip1 = null;
            if (matchedAmount == cards)
            {
                LevelManager.Instance.EndGame(true);
            }
        }
        else StartCoroutine(FlipBack(picture));
    }

    private IEnumerator FlipBack(Picture picture)
    {
        allowNewFlip = false;
        yield return new WaitForSeconds(0.5f);
        flip1.flipCard.StartFlip();
        picture.flipCard.StartFlip();
        flip1 = null;
        allowNewFlip = true;
    }
}
