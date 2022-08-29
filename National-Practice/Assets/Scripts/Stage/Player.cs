using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")] 
    // [SerializeField] private float normalMoveSpeed; // 걷는 속도
    // [SerializeField] private float runMoveSpeed; // 달리는 속도
    [SerializeField] private float moveSpeed; // 실제 적용 속도
    [SerializeField] private float jumpPower; // 점프 강도

    private Rigidbody _rigidbody;
    private bool _isJumping; // 점프중인지 확인
    private bool _isWall; // 벽에 붙어있는지 확인

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isJumping = false;
    }

    private void Update()
    {
        Move();
        Jump();
        WallDash();
        foreach (var key in Stage.LimitedInputs)
        {
            if (Input.GetKeyUp(key))
            {
                Stage.Instance.CountInputKey(key);
            }
        }
    }

    private void WallDash() // 벽타기
    {
        if (!_isWall) return;
        _rigidbody.velocity = Vector3.zero;
        if (!Input.GetKeyDown(KeyCode.Space) || _isJumping) return;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    private void Move()
    {
        // Todo : 조작키 별로 키 세기
        // moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runMoveSpeed : normalMoveSpeed;
        if (_isWall) return;
        
        if (Input.GetKey(KeyCode.DownArrow)&&Stage.Instance.CanInputKey(KeyCode.DownArrow))
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        if (Input.GetKey(KeyCode.UpArrow)&&Stage.Instance.CanInputKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        if (Input.GetKey(KeyCode.RightArrow)&&Stage.Instance.CanInputKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        if (Input.GetKey(KeyCode.LeftArrow)&&Stage.Instance.CanInputKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space)|| _isJumping || _isWall) return;

        _isJumping = true;
        _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _isJumping = false;
        if (collision.gameObject.CompareTag("Wall")) // 벽타기
        {
            _isJumping = false;
            _isWall = true;
        }
            
    }
}