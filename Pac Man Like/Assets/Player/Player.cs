using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private float _powerupDuration;

    private Rigidbody _rigidBody;
    private Coroutine _powerupCoroutine;
    private bool _isPowerUpActive = false;
    [SerializeField]
    private Transform _respawnPoint;
    [SerializeField]
    private int _health;
    [SerializeField]
    private TMP_Text _healthText;

    public void Dead()
    {
        _health -=1;
        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            Debug.Log("Lose");
        }
        UpdateUI();
    }
    public void PickPowerUp()
    {
        if(_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }
    
    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        if (OnPowerUpStart != null)
        {
          
            OnPowerUpStart();
        }
        
        yield return new WaitForSeconds(_powerupDuration);
        _isPowerUpActive = false;
        if (OnPowerUpStop != null)
        {
           
            OnPowerUpStop();
        }
    }

    
    private void Awake()
    {
        UpdateUI();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }
   private void UpdateUI()
    {
        _healthText.text = "Health : " + _health;
    }
}
