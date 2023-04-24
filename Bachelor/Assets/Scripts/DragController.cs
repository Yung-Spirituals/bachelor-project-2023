using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged;
    
    private bool _isDragActive;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;


    private void Update()
    {
        if (PauseManager.Instance.isPaused || !PauseManager.Instance.canPause) return;
        
        if (_isDragActive && (Input.GetMouseButtonUp(0) ||
                              (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0) _screenPosition = Input.GetTouch(0).position;
        else return;
        

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        
        if (_isDragActive) Drag();
        
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    private void InitDrag() { UpdateDragStatus(true); }

    private void Drag() { _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y); }

    private void Drop() { UpdateDragStatus(false); }

    private void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.isDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }
}
