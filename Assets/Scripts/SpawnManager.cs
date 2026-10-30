using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject enemyType1;
    [SerializeField] private GameObject enemyType2;
    [SerializeField] private GameObject enemyType3;

    [Header("Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private float startingSpawnTime = 3f;
    [SerializeField] private float minimumSpawnTime = 0.90f;
    [SerializeField] private float spawnTimeReduction = 0.03f;

    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private FireManager fireManager;

    private float spawnTimer = 0f;
    private float gameTime = 0f;
    private float currentSpawnTime;
    private int lastSpawnPoint = -1;

    private void OnEnable()
    {
        gameTime = 0f;
        spawnTimer = 0f;
        currentSpawnTime = startingSpawnTime;
        lastSpawnPoint = -1;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        ReduceSpawnTime();

        if (spawnTimer >= currentSpawnTime)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void ReduceSpawnTime()
    {
        currentSpawnTime -= spawnTimeReduction * Time.deltaTime;

        if (currentSpawnTime < minimumSpawnTime)
        {
            currentSpawnTime = minimumSpawnTime;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = ChooseEnemy();
        int randomPoint = Random.Range(0, spawnPoints.Length);

        while (randomPoint == lastSpawnPoint)
        {
            randomPoint = Random.Range(0, spawnPoints.Length);
        }

        lastSpawnPoint = randomPoint;

        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoints[randomPoint].position, Quaternion.identity, enemiesParent);

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.Initialize(target, fireManager);
    }

    private GameObject ChooseEnemy()
    {
        int randomNumber = Random.Range(0, 100);

        if (gameTime < 6f)
        {
            return enemyType1;
        }

        if (gameTime < 16f)
        {
            if (randomNumber < 70)
            {
                return enemyType1;
            }

            return enemyType2;
        }

        if (gameTime < 30f)
        {
            if (randomNumber < 55)
            {
                return enemyType1;
            }

            if (randomNumber < 90)
            {
                return enemyType2;
            }

            return enemyType3;
        }

        if (gameTime < 45f)
        {
            if (randomNumber < 40)
            {
                return enemyType1;
            }

            if (randomNumber < 75)
            {
                return enemyType2;
            }

            return enemyType3;
        }

        if (gameTime < 60f)
        {
            if (randomNumber < 25)
            {
                return enemyType1;
            }

            if (randomNumber < 60)
            {
                return enemyType2;
            }

            return enemyType3;
        }

        if (gameTime < 75f)
        {
            if (randomNumber < 10)
            {
                return enemyType1;
            }

            if (randomNumber < 35)
            {
                return enemyType2;
            }

            return enemyType3;
        }

        return enemyType3;
    }

    public void StopSpawning()
    {
        enabled = false;

        for (int i = enemiesParent.childCount - 1; i >= 0; i--)
        {
            Destroy(enemiesParent.GetChild(i).gameObject);
        }
    }
}