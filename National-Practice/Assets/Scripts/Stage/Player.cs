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

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isJumping = false;
    }

    private void Update()
    {
        Move();
        Jump();

        foreach (var key in Stage.LimitedInputs.Where(Input.GetKeyUp))
        {
            Stage.Instance.CountInputKey(key);
        }
    }

    private void Move()
    {
        // Todo : 조작키 별로 키 세기
        // moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runMoveSpeed : normalMoveSpeed;

        var dir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0,
            Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        transform.Translate(dir);
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (_isJumping) return;

        _isJumping = true;
        _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _isJumping = false;
    }
}