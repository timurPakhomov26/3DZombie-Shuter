using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _mouseSensivity;

    private float _xRotation;

    private void Start() 
    {
       Cursor.lockState = CursorLockMode.Locked;    
    }


    private void Update() 
    {
       RotatePlayer();    
    }


    private void RotatePlayer()
    {
        var mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation,-90f,85f);

        transform.localRotation = Quaternion.Euler(_xRotation,0f,0f);
        _playerBody.Rotate(Vector3.up * mouseX);


    }
}
