using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region Private Members 

    /// Reference for the Bullet
    [SerializeField]
    private BulletBehaviour bulletPrefabs;

    /// <summary>
    /// Reference for the Rigidbody2D of the player
    /// </summary>
    private Rigidbody2D _rigidbody2D;

    /// <summary>
    /// Reference for the thurstSpeed
    /// </summary>
    [SerializeField]
    private float thurstSpeed = 1f ;

    /// <summary>
    /// Reference for the thursting
    /// </summary>
    private bool _thursting;

    /// <summary>
    /// Reference for the turnSpeed
    /// </summary>
    [SerializeField]
    private float _turnSpeed = 1.0f;

    /// <summary>
    /// Reference for the turn direction
    /// </summary>
    private float _turnDirection;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Awake Methods 
    /// </summary>
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    /// Methods for the Update 
    /// </summary>
    private void Update()
    {
        _thursting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    /// <summary>
    /// Methods for the Fixed Update 
    /// </summary>
    private void FixedUpdate()
    {
        if (_thursting)
        {
            _rigidbody2D.AddForce(transform.up * thurstSpeed);
        }

        if(_turnDirection != 0.0f){
            _rigidbody2D.AddTorque(_turnDirection * thurstSpeed);
        }
    }

    #endregion

    #region Private Methods 

    /// <summary>
    /// Methods for the shoot bullet 
    /// </summary>
    void ShootBullet()
    {
        BulletBehaviour bullet = Instantiate(bulletPrefabs, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

  
    /// <summary>
    /// reference for the collision with Asteroid
    /// </summary>
    /// <param name="collision">reference for the collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0.0f;
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDies();
        }
    }

    #endregion

}
