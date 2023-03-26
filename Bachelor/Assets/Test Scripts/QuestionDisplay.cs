using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDisplay : MonoBehaviour
{
    public GameObject screenQuestion;
    public GameObject answerA;
    public GameObject answerB;
    public GameObject answerC;
    public GameObject answerD;
    public static string newQuestion;
    public static string newA;
    public static string newB;
    public static string newC;
    public static string newD;

    public static bool updateQuestion = false;
    
    // Update is called once per frame
    void Update()
    {
        if (updateQuestion == false)
        {
            updateQuestion = true;
            StartCoroutine(PushTextOnScreen());
        }
    }

    IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.01f);
        screenQuestion.GetComponent<TMPro.TextMeshProUGUI>().text = newQuestion;
        answerA.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = newA;
        answerB.GetComponent<TMPro.TextMeshProUGUI>().text = newB;
        answerC.GetComponent<TMPro.TextMeshProUGUI>().text = newC;
        answerD.GetComponent<TMPro.TextMeshProUGUI>().text = newD;
    }
}
