using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen_Manager : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public int asteroidCount;
    public TMPro.TMP_Text HiScoreText;
    public GameObject Press_Spacebar;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("game");
        }
    }
    private void Start()
    {
        StartCoroutine(WaitOneSecond_TextBlink());
        HiScoreText.SetText("HI SCORE: " + PlayerPrefs.GetInt("HiScore").ToString());
        int numberOfAsteroids = 8;
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            SpawnAsteroid();
        }

    }
    private void SpawnAsteroid()
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
        Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
    }

    private IEnumerator WaitOneSecond_TextBlink()
    {
        while (true)
        {

            Debug.Log("wow");
            yield return new WaitForSeconds(0.5f);
            Press_Spacebar.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Press_Spacebar.SetActive(false);
        }
    }
}
