using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpwanerBehaviour : MonoBehaviour
{
    #region Public members

    /// <summary>
    /// Reference for the asteroid 
    /// </summary>
    public AsteroidBehaviour AsteroidPrefab;

    /// <summary>
    /// Rfereence for the spawne
    /// </summary>
    public int spawne = 1;

    /// <summary>
    /// Reference for the trajectory Variance
    /// </summary>
    public int trajectoryVariance = 15;

    /// <summary>
    /// Reference for the spawn Distance
    /// </summary>
    public int spawnDistance = 15;

    /// <summary>
    /// Reference for the spawne Rate
    /// </summary>
    public float spawneRate = 2.0f;

    /// <summary>
    /// Reference for the spawne Amount
    /// </summary>
    public int spawneAmount = 1;    

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawne), spawneRate, spawneRate);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Methods for the Spawne the Asteroid In Game 
    /// </summary>
    void Spawne()
    {
        for (int i = 0; i < spawne; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnePoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            AsteroidBehaviour asteroid = Instantiate(AsteroidPrefab, spawnePoint , rotation);
            asteroid.size = Random.Range(asteroid.size, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

    #endregion
}
