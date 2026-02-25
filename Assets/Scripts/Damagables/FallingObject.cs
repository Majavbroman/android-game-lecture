using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingObject : MonoBehaviour
{
    [SerializeField] private protected float _minGravity;
    [SerializeField] private protected float _maxGravity;
    [SerializeField] private protected float _rotationSpeed;

    [SerializeField] private protected float _sizePerObjectMult;

    private protected virtual void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Random.Range(_minGravity, _maxGravity);
    }

    private protected virtual void Update()
    {
        if (_rotationSpeed != 0)
        {
            Rotate();
        }
    }

    public void SetObjectAmount(int amount)
    {
        float size = 1f + (amount - 1) * _sizePerObjectMult;
        transform.localScale = new Vector3(size, size, 1);
    }

    private protected void Rotate()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
