using UnityEngine;

/// <summary>
/// Makes the player move with the physics engine.
/// </summary>
public class PlayerMovement : MonoBehaviour, IMove
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    [Header("Parameters")]
    [SerializeField] private float _speed = 5f;

    [Header("Animations")]
    [SerializeField] private PlayerAnimations _animations;

    //Ajout Getter,set pour les tests
    public PlayerAnimations Animations
    {
        get { return _animations; }
        set { _animations = value; }
    }

    private Vector2 _movement;

    public Rigidbody rigidBody
    {
        get { return _rb; }
        set { _rb = value; }
    }

    public PlayerAnimations animations
    {
        //get { return _animations; }
        set { _animations = value; }
    }

    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }


    /// <summary>
    /// Moves the player with the physics engine based on the given movement.
    /// The x component moves the player on the x axis, the y component moves the player on the z axis.
    /// </summary>
    /// <param name="movement">The input movement (for example, from an input controller)</param>
    public void Move(Vector2 movement)
    {
        _movement = movement;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_movement.x * _speed, _rb.velocity.y, _movement.y * _speed);
        _animations.Move(_movement.sqrMagnitude);

        // Rotate the player to the direction of _movement
        if (_movement != Vector2.zero)
        {
            _rb.rotation = Quaternion.LookRotation(new Vector3(_movement.x, 0f, _movement.y));
        }
    }
}
