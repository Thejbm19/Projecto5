using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int points;
    public ParticleSystem explosionParticle;
    private float lifeTime = 2f;
    private GameManager gameManagerScript;
   
    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        lifeTime = gameManagerScript.spawnRate;
        Destroy(gameObject, lifeTime);
    }

   
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!gameManagerScript.isGameOver)
        {
            gameManagerScript.UpdateScore(points);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            Destroy(gameObject);

            if (gameObject.CompareTag("Bad"))
            {
                gameManagerScript.GameOver();
            }

        }
    }

    private void OnDestroy()
    {
        gameManagerScript.targetPositions.Remove(transform.position);
    }
}
