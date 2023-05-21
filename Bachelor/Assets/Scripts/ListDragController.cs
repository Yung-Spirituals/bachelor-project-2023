using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ListDragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public TextMeshProUGUI textEntry;
    [SerializeField] private TextMeshProUGUI numeration;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;

    public RectTransform currentTransform;
    
    private GameObject _mainContent;
    private Vector3 _currentPosition;
    private Canvas _canvas;

    private bool _dragging;
    private bool _canDrag;
    private int _totalChild;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        numeration.text = transform.GetSiblingIndex() + 1 + ".";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        if (_canDrag == false) _canDrag = true;
        _currentPosition = currentTransform.position;
        _mainContent = currentTransform.parent.gameObject;
        _totalChild = _mainContent.transform.childCount;
        gameObject.layer = Layer.Dragging;
        _canvas.sortingOrder = 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (PauseManager.Instance.isPaused || !_canDrag || QuestionManager.Instance.locked) return;
        _dragging = true;
        currentTransform.position =
            new Vector3(currentTransform.position.x, eventData.position.y, currentTransform.position.z);

        for (int i = 0; i < _totalChild; i++)
        {
            if (i == currentTransform.GetSiblingIndex()) continue;
            Transform otherTransform = _mainContent.transform.GetChild(i);
            int distance = (int) Vector3.Distance(currentTransform.position,
                otherTransform.position);
            if (distance > 10) continue;
            Vector3 otherTransformOldPosition = otherTransform.position;
            otherTransform.position = new Vector3(otherTransform.position.x, _currentPosition.y,
                otherTransform.position.z);
            currentTransform.position = new Vector3(currentTransform.position.x, otherTransformOldPosition.y,
                currentTransform.position.z);
            currentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
            numeration.text = transform.GetSiblingIndex() + 1 + ".";
            _currentPosition = currentTransform.position;
            CheckPosition();
        }

        _dragging = false;
    }

    private void Update()
    {
        numeration.text = transform.GetSiblingIndex() + 1 + ".";
        CheckPosition();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        _canDrag = false;
        currentTransform.position = _currentPosition;
        gameObject.layer = Layer.UI;
        _canvas.sortingOrder = 1;
    }

    public void MoveUp()
    {
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        if (_dragging) return;
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index - 1);
        CheckPosition();
    }

    public void MoveDown()
    {
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        if (_dragging) return;
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index + 1);
        CheckPosition();
    }

    private void CheckPosition()
    {
        int index = transform.GetSiblingIndex() + 1;
        upButton.interactable = !(index <= 1);
        downButton.interactable = index != transform.parent.childCount;
    }
}