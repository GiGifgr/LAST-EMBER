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
    [SerializeField] private float minimumSpawnTime = 1.4f;
    [SerializeField] private float spawnTimeReduction = 0.03f;

    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private FireManager fireManager;
    [SerializeField] private GameStatsManager gameStatsManager;

    [Header("Tutorial UI")]
    [SerializeField] private EnemyTapText tapTextType1;
    [SerializeField] private EnemyTapText tapTextType2;
    [SerializeField] private EnemyTapText tapTextType3;
    [SerializeField] private Transform tapTextsParent;
    [SerializeField] private Camera gameplayCamera;
    [SerializeField] private Vector3 tapTextOffset = new Vector3(0f, 0.8f, 0f);
    [SerializeField] private int tutorialEnemiesPerType = 3;

    private float spawnTimer = 0f;
    private float gameTime = 0f;
    private float currentSpawnTime;
    private int lastSpawnPoint = -1;

    private int type1TutorialCount = 0;
    private int type2TutorialCount = 0;
    private int type3TutorialCount = 0;

    private void OnEnable()
    {
        gameTime = 0f;
        spawnTimer = 0f;
        currentSpawnTime = startingSpawnTime;
        lastSpawnPoint = -1;

        type1TutorialCount = 0;
        type2TutorialCount = 0;
        type3TutorialCount = 0;
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
        enemy.Initialize(target, fireManager, gameStatsManager);

        ShowTapTutorial(newEnemy.transform, enemyToSpawn);
    }

    private void ShowTapTutorial(Transform enemyTransform, GameObject enemyType)
    {
        EnemyTapText tapTextTemplate = null;
        string tutorialText = "";

        if (enemyType == enemyType1 && type1TutorialCount < tutorialEnemiesPerType)
        {
            tapTextTemplate = tapTextType1;
            tutorialText = "1 TAP";
            type1TutorialCount++;
        }
        else if (enemyType == enemyType2 && type2TutorialCount < tutorialEnemiesPerType)
        {
            tapTextTemplate = tapTextType2;
            tutorialText = "2 TAP";
            type2TutorialCount++;
        }
        else if (enemyType == enemyType3 && type3TutorialCount < tutorialEnemiesPerType)
        {
            tapTextTemplate = tapTextType3;
            tutorialText = "3 TAP";
            type3TutorialCount++;
        }

        if (tapTextTemplate == null)
        {
            return;
        }

        EnemyTapText newTapText = Instantiate(tapTextTemplate, tapTextsParent);

        newTapText.Initialize(enemyTransform, gameplayCamera, tutorialText, tapTextOffset);
        newTapText.gameObject.SetActive(true);
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