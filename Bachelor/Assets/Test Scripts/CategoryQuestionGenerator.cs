using System;
using System.Collections;
using Test_Scripts;
using TMPro;
using UnityEngine;

public class CategoryQuestionGenerator : MonoBehaviour
{
    public Question[] Questions =
    {
        new("Meat", 1),
        new("Fish", 0),
        new("Vegetable", 3),
        new("Fruit", 2)
    };
    public GameObject item;

    public TextMeshProUGUI questionText;
    public TMP_Text[] categories;
    private Question currentQuestion;
    private static bool updateQuestion = true;
    private static String[] currentCategories = { "Fish", "Meat", "Fruit", "Vegetable" };

    // Start is called before the first frame update
    void Start()
    {
        NewQuestion();
        for (int i = 0; i < categories.Length; i++)
        {
            categories[i].GetComponent<TMP_Text>().text = currentCategories[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (updateQuestion) return;
        updateQuestion = true;
        StartCoroutine(PushTextOnScreen());
    }

    IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.50f);
        NewQuestion();
    }

    public static void UpdateQuestion()
    {
        updateQuestion = false;
    }

    private int nextQuestionIndex;

    private void NewQuestion()
    {
        currentQuestion = Questions[nextQuestionIndex];
        nextQuestionIndex++;
        questionText.text = currentQuestion.GetQuestion();
        item.transform.position = Vector3.zero;
        SetTag(currentQuestion);
    }

    private void SetTag(Question question)
    {
        if (question.GetCorrectCategory() == 0)
        {
            item.gameObject.tag = "Category0";
        }
        else if (question.GetCorrectCategory() == 1)
        {
            item.gameObject.tag = "Category1";
        }
        else if (question.GetCorrectCategory() == 2)
        {
            item.gameObject.tag = "Category2";
        }
        else if (question.GetCorrectCategory() == 3)
        {
            item.gameObject.tag = "Category3";
        }
    }

    public static void SetCategories(String[] categories)
    {
        currentCategories = categories;
    }
}