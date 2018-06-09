using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Transform Enemy;
    [Header("Wave Properties")]

    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = 0.25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    [Header("User Interface")]
    private int Score = 0;
    private int WaveNumber = 0;

    public Text ScoreText;
    public Text WaveText;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
	}

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBeforeSpawning);

        while(true)
        {
            if(currentNumberOfEnemies<=0)
            {
                WaveNumber++;
                WaveText.text = "Wave : " + WaveNumber;

                for(int i=0;i<enemiesPerWave;i++)
                {
                    float RandDistance = Random.Range(10, 25);

                    Vector2 RandDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;

                    enemyPos.x += RandDirection.x * RandDistance;
                    enemyPos.y += RandDirection.y * RandDistance;

                    Instantiate(Enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }

            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }
	
    public void IncreaseScore(int Increase)
    {
        Score += Increase;
        ScoreText.text = "Score : " + Score;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
