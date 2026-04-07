using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDataSaver
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
        InputReader.TouchPositionEvent += HandleTouchPosition;
        DataHandler.Instance.Register(this);
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

    public Task SaveData(ref SaveData saveData)
    {
        saveData.GameData.Position = transform.position.x;
        return Task.CompletedTask;
    }

    public void OnGameResume(GameData gameData)
    {
        transform.position = new Vector3(gameData.Position, transform.position.y, transform.position.z);
    }
}
