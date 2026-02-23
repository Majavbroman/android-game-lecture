using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 250f;

    void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Damagable")) return;

        if (collider.TryGetComponent(out IHealth health))
        {
            health?.Damage(1);
        }
        else
        {
            GameManager.Instance.ChangeScore(1);   
        }
        
        Destroy(gameObject);
    }

    public void SetData(int amount)
    {
        float size = 1f - (amount - 1) * 0.15f;
        _speed = 250f + (amount - 1) * 50f;
        transform.localScale = new Vector3(size, size, 1);
    }

    private void Rotate()
    {
        float rotationSpeed = _speed;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
