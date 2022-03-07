using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    public bool isGameOver;
    public List<Vector3> targetPositions;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;

    private float minX = -3.75f;
    private float minY = -3.75f;
    private float distanceBetweenSquares = 2.5f;

    public float spawnRate = 2f;
    private Vector3 randomPos;

    private int score = 0;

    void Start()
    {
        //scoreText.text = $"Score: {score}"; //actualitza es texte cuan se clica a nes objectes
        mainMenuPanel.SetActive(true);

    }


    void Update()
    {
        
    }

    private Vector3 RandomSpawnPosition()
    {
        int SaltosX = Random.Range(0, 4);
        int SaltosY = Random.Range(0, 4);

        float spawnPosX = minX + SaltosX * distanceBetweenSquares;
        float spawnPosY = minY + SaltosY * distanceBetweenSquares;

        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    private IEnumerator SpawnRandomTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targetPrefabs.Length);
            randomPos = RandomSpawnPosition();
            while (targetPositions.Contains(randomPos))
            {
                randomPos = RandomSpawnPosition();
            }

            Instantiate(targetPrefabs[randomIndex], randomPos, targetPrefabs[randomIndex].transform.rotation);
            targetPositions.Add(randomPos);

        }


    }

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd; // li diu que sumi punts de sa variable score a pointsToAdd
        scoreText.text = $"Score: {score}"; //actualitza es texte cuan se clica a nes objectes
    }


    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame(int difficulty)
    {
        mainMenuPanel.SetActive(false);

        isGameOver = false;
        gameOverPanel.SetActive(false);
        
        score = 0;
        UpdateScore(0);

        spawnRate = 2f;
        spawnRate /= difficulty; // divideix es valor enter que te asignat es buttons
        StartCoroutine(SpawnRandomTarget());
    }
}
