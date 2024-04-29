using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedUp;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationSensetive;
    [SerializeField] private float _jumpForce;

    private Rigidbody _rb;

    private float _normalizeRotX;
    private float _normalizeDirZ;
    private float _normalizeDirX;
    private bool isJumping;
    private bool isSpeedUp;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        isJumping = false;
    }

    private void Update()
    {
        _normalizeRotX = Input.GetAxis("Mouse X");
        _normalizeDirZ = Input.GetAxisRaw("Vertical");
        _normalizeDirX = Input.GetAxisRaw("Horizontal");
        transform.Rotate(Rotation(_normalizeRotX, _rotationSensetive) * _rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Jump") > 0 && !isJumping)
        {
            Jump();
        }

        if (Input.GetAxisRaw("Run") > 0)
        {
            isSpeedUp = true;
        }
        else isSpeedUp = false;

        Vector3 gravity = new Vector3(0, _rb.velocity.y, 0);
        _rb.velocity = gravity + DirectionMove(_normalizeDirZ, _normalizeDirX) * _speed * speedUp(_speedUp) * Time.fixedDeltaTime;
    }

    private Vector3 DirectionMove(float inputZ, float inputX)
    {
        Vector3 direction = Vector3.zero;
        return direction += transform.forward * (inputZ) + transform.right * (inputX);
    }

    private Vector3 Rotation(float input, float sensitive)
    {
        return transform.up * (input * sensitive);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private float speedUp(float speedUp)
    {
        if (isSpeedUp)
        {
            return speedUp;
        }
        else return 1;
    }


    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }
}
