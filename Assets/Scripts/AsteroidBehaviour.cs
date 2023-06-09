using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    #region  public Members

    /// <summary>
    /// Reference for  sprite Renderer
    /// </summary>
    public Sprite[] sprites;

    /// <summary>
    /// Reference for size Asteroid 
    /// </summary>
    public float size = 1.0f;

    /// <summary>
    /// Reference for Asteroid speed 
    /// </summary>
    public float asteroidSpeed = 50f;

    /// <summary>
    /// Reference for  minSize Asteroid 
    /// </summary>
    public float minSize = 0.5f;

    /// <summary>
    /// Reference for  maxSize Asteroid 
    /// </summary>
    public float maxSize = 1.5f;
     
    /// <summary>
    /// Reference for  max life Asteroid 
    /// </summary>
    public float maxLifeTime = 1.5f;

    #endregion

    #region  Private Members

    /// <summary>
    /// Reference for  sprite Renderer
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Reference for rigidbody Asteroid
    /// </summary>
    private Rigidbody2D _rigidbodyAsteroid;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbodyAsteroid = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * size;
        _rigidbodyAsteroid.mass = size;
    }

    #endregion

    #region public Methods

    /// <summary>
    /// Methods for the Set Trajectory
    /// </summary>
    /// <param name="direction"> reference of the direction</param>
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbodyAsteroid.AddForce(direction * asteroidSpeed);
        Destroy(gameObject, maxLifeTime); 
    }

    /// <summary>
    /// Methods for the create split
    /// </summary>
    void CreateTheSpilt()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        var halfCreate = Instantiate(this, position, transform.rotation);
        halfCreate.size = size * 0.5f;
        halfCreate.SetTrajectory(Random.insideUnitCircle.normalized * asteroidSpeed);
    }

    #endregion

    #region Private memebers 

    /// <summary>
    /// reference for the collision with bullet
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if ((size * 0.5f) >= minSize)
            {
                CreateTheSpilt();
            }
            FindObjectOfType<GameManager>().AsteroidDestroye(this);
            Destroy(gameObject);
        }     
    }

    #endregion
}