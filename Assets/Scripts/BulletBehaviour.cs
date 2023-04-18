using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    
    #region Private Members 

    /// <summary>
    /// Reference for speed bullet
    /// </summary>
    private float _speedBullet = 500.0f;

    /// <summary>
    /// Reference for max Life Bullet
    /// </summary>
    private float _maxLifeBullet = 1.0f;

    /// <summary>
    /// Reference for the rigidbody2D bullet
    /// </summary>
    private  Rigidbody2D _rigidbody2D;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Reference for the Project of the bullet 
    /// </summary>
    public void Project(Vector2 _direction)
    {
         _rigidbody2D.AddForce( _direction * _speedBullet);

        Destroy(gameObject, _maxLifeBullet);
    }

    #endregion
}
