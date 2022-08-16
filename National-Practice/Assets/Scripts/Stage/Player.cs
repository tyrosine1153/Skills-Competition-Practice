using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private float normalMoveSpeed; // 걷는 속도
    [SerializeField] private float runMoveSpeed; // 달리는 속도
    [SerializeField] private float moveSpeed; // 실제 적용 속도
    [SerializeField] private float jumpPower; // 점프 강도 
    [Header("Gun Fire")] 
    [SerializeField] private Transform gunPointTransform; // 총구 오브젝트나 총 오브젝트
    [SerializeField] private GameObject bulletPrefab; // 총알 오브젝트
    [SerializeField] private float fireTime; // 발사 딜레이 시간

    private Rigidbody _rigidbody;
    private float _currentFireTime; // 현재 발사 시간
    private bool _isJumping; // 점프중인지 확인

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isJumping = false;
    }

    private void Update()
    {
        _currentFireTime += Time.deltaTime;

        Move();
        Jump();
        // SingleShoot();  // 단발사격
        // FullAutoShoot();  //연발 사격
    }

    private void Move()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runMoveSpeed : normalMoveSpeed;

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

    public void Shoot()
    {
        // Add Effect or Sound here
        Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
        _currentFireTime = 0;
    }

    private void SingleShoot() // 단발사격
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        if (!(_currentFireTime > fireTime)) return;
        Shoot();
    }

    private void FullAutoShoot() // 연발사격
    {
        if (!Input.GetKey(KeyCode.Mouse1)) return;
        if (!(_currentFireTime > fireTime)) return;
        Shoot();
    }
}