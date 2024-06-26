using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private float moveSpeed; // 실제 적용 속도
    [SerializeField] private float jumpPower; // 점프 강도

    private Rigidbody _rigidbody;
    private bool _isJumping; // 점프중인지 확인
    private bool _isWall; // 벽에 붙어있는지 확인

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Stage.Instance.CanInputKey(KeyCode.RightArrow))
        {
            _rigidbody.AddForce(Vector3.right * moveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Stage.Instance.CanInputKey(KeyCode.LeftArrow))
        {
            _rigidbody.AddForce(Vector3.left * moveSpeed);
        }

        _isWall = Physics.Raycast(transform.position, Vector3.right, 0.2f);
        _rigidbody.useGravity = !_isWall;
        if (!_isWall)
        {
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Stage.Instance.CanInputKey(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        
        foreach (var key in Stage.LimitedInputs)
        {
            if (Input.GetKeyUp(key))
            {
                Stage.Instance.CountInputKey(key);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
        else if (collision.gameObject.CompareTag("Wall")) // 벽타기
        {
            _isJumping = false;
            _isWall = true;
        }
    }
}