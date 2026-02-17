using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 250f;

    void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player"))
        {
            collider.TryGetComponent<Health>(out var health);

            if (health != null)
            {
                health.ChangeHealth(-1);
            }
        }
        else
        {
            GameManager.Instance.AddScore(1);   
        }
        
        Destroy(gameObject);
    }

    public void SetData(int amount)
    {
        float size = 0.65f - (amount - 1) * 0.1f;
        _speed = 250f - (amount - 1) * 50f;
        transform.localScale = new Vector3(size, size, 1);
    }

    private void Rotate()
    {
        float rotationSpeed = _speed;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
