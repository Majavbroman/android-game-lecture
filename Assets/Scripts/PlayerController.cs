using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private float touchPosition = 0f;
    private Rigidbody2D _rb;
    private float _speed;

    private bool _gameGoing = false;

    private Health _health;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _speed = _playerData.Speed;
    }

    private void Start()
    {
        _health = new Health(_playerData.MaxHealth);

        InputReader.Instance.TouchPositionEvent += HandleTouchPosition;

        GameManager gameManager = GameManager.Instance;
        gameManager.OnGameEnd += OnPlayerDeath;
        gameManager.OnGameStart += OnGameStart;
    }

    void FixedUpdate()
    {
        if (!_gameGoing) return;

        float positionDiff = touchPosition - transform.position.x;
        _rb.linearVelocityX = positionDiff * _speed;
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

    private void OnGameStart()
    {
        _gameGoing = true;
        _health.ChangeHealth(int.MaxValue);
    }
}
