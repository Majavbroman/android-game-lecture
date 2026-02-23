using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private float touchPosition = 0f;
    private Rigidbody2D _rb;
    private float _speed;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _speed = _playerData.Speed;
    }

    private void Start()
    {
        InputReader.Instance.TouchPositionEvent += HandleTouchPosition;
    }

    void FixedUpdate()
    {
        float positionDiff = touchPosition - transform.position.x;
        _rb.linearVelocityX = positionDiff * _speed;
    }

    private void HandleTouchPosition(Vector2 position)
    {
        touchPosition = Camera.main.ScreenToWorldPoint(position).x;
    }
}
