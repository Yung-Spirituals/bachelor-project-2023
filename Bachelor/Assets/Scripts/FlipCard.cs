using System.Collections;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    public float x, y, z;
    public GameObject cardBack;
    public bool cardBackIsActive;
    public bool mayBeFlipped = true;

    public void StartFlip() { StartCoroutine(CalculateFlip()); }

    private void Flip()
    {
        if (cardBackIsActive)
        {
            cardBack.SetActive(false);
            cardBackIsActive = false;
        }
        else
        {
            cardBack.SetActive(true);
            cardBackIsActive = true;
        }
    }

    private IEnumerator CalculateFlip()
    {
        if (!mayBeFlipped) yield break;
        
        mayBeFlipped = false;
        if (cardBackIsActive)
        {
            for (int i = 0; i <= 180; i += 10)
            {
                transform.rotation = Quaternion.Euler(0, i, 0);
                if (i is 90 or -90)
                {
                    Flip();
                }

                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 180; i >= 0; i -= 10)
            {
                transform.rotation = Quaternion.Euler(0, i, 0);
                if (i is 90 or -90)
                {
                    Flip();
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        mayBeFlipped = true;
    }
}
