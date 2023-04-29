using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ListDragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI numeration;
    [SerializeField] private TextMeshProUGUI textEntry;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;

    public RectTransform currentTransform;
    
    private GameObject mainContent;
    private Vector3 currentPossition;
    private Canvas _canvas;

    private bool dragging;
    private int totalChild;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        numeration.text = transform.GetSiblingIndex() + 1 + ".";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentPossition = currentTransform.position;
        mainContent = currentTransform.parent.gameObject;
        totalChild = mainContent.transform.childCount;
        gameObject.layer = Layer.Dragging;
        _canvas.sortingOrder = 2;
        Debug.Log("Pressed");
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragging = true;
        currentTransform.position =
            new Vector3(currentTransform.position.x, eventData.position.y, currentTransform.position.z);

        for (int i = 0; i < totalChild; i++)
        {
            if (i == currentTransform.GetSiblingIndex()) continue;
            Transform otherTransform = mainContent.transform.GetChild(i);
            int distance = (int) Vector3.Distance(currentTransform.position,
                otherTransform.position);
            if (distance > 10) continue;
            Vector3 otherTransformOldPosition = otherTransform.position;
            otherTransform.position = new Vector3(otherTransform.position.x, currentPossition.y,
                otherTransform.position.z);
            currentTransform.position = new Vector3(currentTransform.position.x, otherTransformOldPosition.y,
                currentTransform.position.z);
            currentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
            numeration.text = transform.GetSiblingIndex() + 1 + ".";
            currentPossition = currentTransform.position;
            CheckPosition();
        }

        dragging = false;
    }

    private void Update()
    {
        numeration.text = transform.GetSiblingIndex() + 1 + ".";
        CheckPosition();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentTransform.position = currentPossition;
        gameObject.layer = Layer.UI;
        _canvas.sortingOrder = 1;
        Debug.Log("Released");
    }

    public void MoveUp()
    {
        if (dragging) return;
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index - 1);
        CheckPosition();
        Debug.Log("MoveUp");
    }

    public void MoveDown()
    {
        if (dragging) return;
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index + 1);
        CheckPosition();
        Debug.Log("MoveDown");
    }

    private void CheckPosition()
    {
        int index = transform.GetSiblingIndex() + 1;
        upButton.interactable = !(index <= 1);
        downButton.interactable = index != transform.parent.childCount;
    }
}