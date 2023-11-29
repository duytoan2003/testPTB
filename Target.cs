using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float maxTorque = 10f;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    public ParticleSystem explosionParticle;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }
    Vector2 RandomForce()
    {
        return Vector2.up * Random.Range(minSpeed, maxSpeed);
    }

    Vector2 RandomSpawnPos()
    {
        return new Vector2(Random.Range(-xRange, xRange), ySpawnPos);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void OnMouseDown()
    {
        
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy (gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            
            if (gameManager.UpdateLives(0) > 0)
            {
                gameManager.UpdateLives(-1);
            }
            if (gameManager.UpdateLives(0) == 0 )
            {
                gameManager.GameOver();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position,
            explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }

    }
}
