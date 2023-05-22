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

    // Triggers when the player presses down on the game object.
    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the player is allowed to drag the game object at this time.
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        if (_canDrag == false) _canDrag = true;
        
        // Begin dragging the object.
        _currentPosition = currentTransform.position;
        _mainContent = currentTransform.parent.gameObject;
        _totalChild = _mainContent.transform.childCount;
        
        // Keep the game object in a separate layer whilst dragging.
        gameObject.layer = Layer.Dragging;
        _canvas.sortingOrder = 2;
    }

    // Is called while the player is dragging th game object.
    public void OnDrag(PointerEventData eventData)
    {
        // Check if the player is allowed to drag the game object at this time.
        if (PauseManager.Instance.isPaused || !_canDrag || QuestionManager.Instance.locked) return;
        _dragging = true;
        currentTransform.position =
            new Vector3(currentTransform.position.x, eventData.position.y, currentTransform.position.z);

        // Drag the game object
        for (int i = 0; i < _totalChild; i++)
        {
            if (i == currentTransform.GetSiblingIndex()) continue;
            
            // Update the sibling index for each sibling depending on where the dragged element is positioned.
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
    
    // Triggered when the player releases the game object.
    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the player is allowed to drag the game object at this time.
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        
        // Release the game object.
        _canDrag = false;
        currentTransform.position = _currentPosition;
        
        // Return the game object to its correct layer.
        gameObject.layer = Layer.UI;
        _canvas.sortingOrder = 1;
    }

    // Updates the numeration and checks if the buttons can be used.
    private void Update()
    {
        numeration.text = transform.GetSiblingIndex() + 1 + ".";
        CheckPosition();
    }

    // Switch places with the game object above.
    public void MoveUp()
    {
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        if (_dragging) return;
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index - 1);
        CheckPosition();
    }

    // Switch places with the game object below.
    public void MoveDown()
    {
        if (PauseManager.Instance.isPaused || QuestionManager.Instance.locked) return;
        if (_dragging) return;
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index + 1);
        CheckPosition();
    }

    // Update sibling index based on position.
    private void CheckPosition()
    {
        int index = transform.GetSiblingIndex() + 1;
        upButton.interactable = !(index <= 1);
        downButton.interactable = index != transform.parent.childCount;
    }
}