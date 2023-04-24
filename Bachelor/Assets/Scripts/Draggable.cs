using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool isDragging;
    
    private float _movementTime = 10f;
    private Vector3? _movementDestination;

    private void FixedUpdate()
    {
        if (PauseManager.Instance.isPaused || !PauseManager.Instance.canPause) return;
        if (!_movementDestination.HasValue) return;
            
        if (isDragging)
        {
            _movementDestination = null;
            return;
        }

        if (transform.position == _movementDestination)
        {
            gameObject.layer = Layer.Default;
            _movementDestination = null;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _movementDestination.Value,
                _movementTime * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag(gameObject.tag)) return;
        bool correct = QuestionManager.Instance.Answer(col.gameObject.GetComponent<DragSlot>().number, false);
        _movementDestination = correct ? col.transform.position : new Vector3(0, -1, 0);
    }
}