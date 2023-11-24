using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _camera;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        HideAndLockScreen();
    }
    private void HideAndLockScreen()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;
        Vector3 movementDirection = new Vector3 (horizontal, 0, vertical);
        _rigidBody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }
}
