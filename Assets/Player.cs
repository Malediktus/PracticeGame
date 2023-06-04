using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>();
    }
}
