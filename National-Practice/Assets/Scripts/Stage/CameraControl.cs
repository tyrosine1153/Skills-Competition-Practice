using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed; // 카메라 회전 속도
    private Player _player;

    private float _rotationX; // X축 회전값(카메라에 적용)
    private float _rotationY; // Y축 회전값(플레이어에 적용)

    private void Update()
    {
        _player = FindObjectOfType<Player>();
        MouseMove();
    }

    private void MouseMove()
    {
        var mx = Input.GetAxis("Mouse X");
        var my = Input.GetAxis("Mouse Y");

        _rotationX += rotationSpeed * my * Time.deltaTime;
        _rotationY += rotationSpeed * mx * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, -30, 30); // 카메라 x축 회전량에 제한 걸기(-30부터 30까지만 회전 가능)

        _player.transform.eulerAngles = new Vector3(0, _rotationY, 0); // 플레이어에 roty를 넣어 회전
        transform.eulerAngles = new Vector3(-_rotationX, _rotationY, 0); // 카메라에 제한을 건 rotx를 넣어 회전
    }
}
