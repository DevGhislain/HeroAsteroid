using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Members 

    /// <summary>
    /// Reference for the :player
    /// </summary>
    public PlayerBehaviour player;

    public ParticleSystem ExplosionPlayer;

    /// <summary>
    /// Reference for the respawn Invulnerabily Time
    /// </summary>
    public float respawnInvulnerabilyTime = 4f;

    /// <summary>
    /// Referenec for the respan 
    /// </summary>
    public float respawnfloat = 3.0f;

    /// <summary>
    /// reference for the live of the player 
    /// </summary>
    public int live = 3;

    /// <summary>
    /// reference for the Score of the player 
    /// </summary>
    public int Score = 0;

    #endregion

    #region Private Methods

    /// <summary>
    /// Methods for the Respawn
    /// </summary>
    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), 3f);
    }

    /// <summary>
    /// Methods for the active the collision
    /// </summary>
    void TurnOnCollision()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Methods for the player Dies 
    /// </summary>
    public void PlayerDies()
    {
        ExplosionPlayer.transform.position = player.transform.position;
        ExplosionPlayer.Play();
        live--;
        if (live > 0)
        {
            Invoke(nameof(Respawn), respawnInvulnerabilyTime);
        }
        else
        {
            GameOver();
        }
    }

    /// <summary>
    /// Methods for the Asteroid Destroye
    /// </summary>
    public void AsteroidDestroye(AsteroidBehaviour asteroid)
    {
        ExplosionPlayer.transform.position = asteroid.transform.position;
        ExplosionPlayer.Play();

        // The Score 
        if (asteroid.size < 0.75f)
        {
            this.Score += 100;
        }
        else if (asteroid.size < 1.0f)
        {
            this.Score += 50;
        }
        else
        {
            this.Score += 25;
        }
    }

    /// <summary>
    /// Methods for the Game over
    /// </summary>
    public void GameOver()
    {
        ExplosionPlayer.loop =  true;
    }

    #endregion

}
