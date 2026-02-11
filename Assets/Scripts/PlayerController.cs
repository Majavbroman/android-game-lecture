using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private float touchPosition = 0f;
    private Rigidbody2D _rb;

    private bool _gameGoing = false;

    [SerializeField] private float _speed = 5f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InputReader.Instance.TouchPositionEvent += HandleTouchPosition;
        GameManager.Instance.OnPlayerDeath += OnPlayerDeath;
        GameManager.Instance.OnGameStart += () => _gameGoing = true;
    }

    void FixedUpdate()
    {
        if (!_gameGoing) return;

        float positionDelta = touchPosition - transform.position.x;
        _rb.linearVelocityX = positionDelta * _speed;
    }

    private void HandleTouchPosition(Vector2 position)
    {
        touchPosition = Camera.main.ScreenToWorldPoint(position).x;
    }

    private void OnPlayerDeath()
    {
        _gameGoing = false;
        _rb.linearVelocity = Vector2.zero;
    }
}
