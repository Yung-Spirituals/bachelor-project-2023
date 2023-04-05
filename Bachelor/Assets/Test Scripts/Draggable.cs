using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    //public Vector3 LastPosition;
    private float _movementTime = 10f;
    private System.Nullable<Vector3> _movementDestination;

    private void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            if (IsDragging)
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag(gameObject.tag))
        {
            _movementDestination = col.transform.position;
            CategoryQuestionGenerator.UpdateQuestion();
        }
        else
        {
            _movementDestination = Vector3.zero;
        }
    }
}