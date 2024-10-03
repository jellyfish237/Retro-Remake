using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public int asteroidCount = 0;
    public int level = 0;
    public int current_score = 0;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text HiScoreText;
    public powerup powerupPrefab;
    public enemy enemyPrefab;
    public bool double_point_bool = false;
    public GameObject player;


    private void Start()
    {
        SpawnEnemy();
        SpawnPowerUp();
        HiScoreText.SetText("HI SCORE: " + PlayerPrefs.GetInt("HiScore").ToString());
    }
    public void Update()
    {
        scoreText.SetText("Score: " + current_score.ToString());
        if (current_score > PlayerPrefs.GetInt("HiScore"))
        {
            HiScoreText.SetText("HI SCORE: " + current_score.ToString());
        }

        if (asteroidCount == 0)
        {
            level++;
            int numberOfAsteroids = 2 + level;
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                SpawnAsteroid();
            }
        }
    }
    private void SpawnEnemy()
    {
        int enemy_chance = Random.Range(1, 250 / (1+level));
        if (enemy_chance == 1)
        {
            float offset = Random.Range(0f, 1f);
            Vector2 viewportSpawnPosition = Vector2.zero;
            int edge = Random.Range(0, 4);

            if (edge == 0)
            {
                viewportSpawnPosition = new Vector2(offset, 0);
            }
            else if (edge == 1)
            {
                viewportSpawnPosition = new Vector2(offset, 1);
            }
            else if (edge == 2)
            {
                viewportSpawnPosition = new Vector2(0, offset);
            }
            else if (edge == 3)
            {
                viewportSpawnPosition = new Vector2(1, offset);
            }
            Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
            enemy new_enemy = Instantiate(enemyPrefab, worldSpawnPosition, Quaternion.identity);
            new_enemy.player = player;
            int type = Random.Range(0, 2);
            Debug.Log(type);
            new_enemy.type = type;
            new_enemy.gameManager = this;
        }

            StartCoroutine(WaitOneSecond_Enemy());
    }
    private IEnumerator WaitOneSecond_Enemy()
    {
        yield return new WaitForSeconds(1.0f);
        SpawnEnemy();
    }
    private void SpawnPowerUp()
    {
        int powerup_chance = Random.Range(1, 40);
        if (powerup_chance == 1)
        {
            float offset = Random.Range(0f, 1f);
            Vector2 viewportSpawnPosition = Vector2.zero;
            int edge = Random.Range(0, 4);

            if (edge == 0)
            {
                viewportSpawnPosition = new Vector2(offset, 0);
            }
            else if (edge == 1)
            {
                viewportSpawnPosition = new Vector2(offset, 1);
            }
            else if (edge == 2)
            {
                viewportSpawnPosition = new Vector2(0, offset);
            }
            else if (edge == 3)
            {
                viewportSpawnPosition = new Vector2(1, offset);
            }
            Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
            powerup new_powerup = Instantiate(powerupPrefab, worldSpawnPosition, Quaternion.identity);
            new_powerup.gameManager = this;
        }
            StartCoroutine(WaitOneSecond_PowerUp());

    }
    private IEnumerator WaitOneSecond_PowerUp()
    {
        yield return new WaitForSeconds(1.0f);
        SpawnPowerUp();
    }

    private void SpawnAsteroid()
        {
           float offset = Random.Range(0f, 1f);
            Vector2 viewportSpawnPosition = Vector2.zero;
            int edge = Random.Range(0, 4);

            if (edge == 0) {
                viewportSpawnPosition = new Vector2(offset, 0);
            } else if (edge == 1) {
                viewportSpawnPosition = new Vector2(offset, 1);
            } else if (edge == 2) {
                viewportSpawnPosition = new Vector2(0, offset);
            } else if (edge == 3) {
                viewportSpawnPosition = new Vector2(1, offset);
            }
            Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
            Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
            asteroid.gameManager = this;
        }
        public void GameOver()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("title_screen");

        yield return null;
    }
    public void StartCoroutine()
    {
        StartCoroutine(DoublePoints());
    }
    public IEnumerator DoublePoints()
    {
        double_point_bool = true;
        yield return new WaitForSeconds(15.0f);
        double_point_bool = false;
    }
}
