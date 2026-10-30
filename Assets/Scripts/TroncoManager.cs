using UnityEngine;

public class TroncoManager : MonoBehaviour
{
    [Header("Tronco")]
    [SerializeField] private GameObject troncoPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform troncosParent;

    [Header("Spawn")]
    [SerializeField] private float firstSpawnTime = 10f;
    [SerializeField] private float spawnInterval = 15f;

    [Header("References")]
    [SerializeField] private FireManager fireManager;
    [SerializeField] private Camera gameplayCamera;

    [Header("Tutorial UI")]
    [SerializeField] private RectTransform bonusText;
    [SerializeField] private Vector3 bonusTextOffset = new Vector3(0f, 0.75f, 0f);
    [SerializeField] private int tutorialTroncos = 2;

    private float spawnTimer = 0f;
    private bool firstTronco = true;
    private GameObject currentTronco;
    private int tutorialTroncosRemaining;
    private int lastSpawnPoint = -1;
    private bool showBonusText = false;

    private void OnEnable()
    {
        spawnTimer = 0f;
        firstTronco = true;
        currentTronco = null;
        tutorialTroncosRemaining = tutorialTroncos;
        lastSpawnPoint = -1;
        showBonusText = false;

        bonusText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (currentTronco != null)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        float waitTime;

        if (firstTronco)
        {
            waitTime = firstSpawnTime;
        }
        else
        {
            waitTime = spawnInterval;
        }

        if (spawnTimer >= waitTime)
        {
            SpawnTronco();

            spawnTimer = 0f;
            firstTronco = false;
        }
    }

    private void LateUpdate()
    {
        if (!showBonusText || currentTronco == null)
        {
            return;
        }

        Vector3 worldPosition = currentTronco.transform.position + bonusTextOffset;
        Vector3 screenPosition = gameplayCamera.WorldToScreenPoint(worldPosition);

        bonusText.position = screenPosition;
    }

    private void SpawnTronco()
    {
        int randomPoint = Random.Range(0, spawnPoints.Length);

        while (randomPoint == lastSpawnPoint && spawnPoints.Length > 1)
        {
            randomPoint = Random.Range(0, spawnPoints.Length);
        }

        lastSpawnPoint = randomPoint;

        currentTronco = Instantiate(troncoPrefab, spawnPoints[randomPoint].position, Quaternion.identity, troncosParent);

        Tronco tronco = currentTronco.GetComponent<Tronco>();
        tronco.Initialize(fireManager, this);

        if (tutorialTroncosRemaining > 0)
        {
            showBonusText = true;
            bonusText.gameObject.SetActive(true);

            tutorialTroncosRemaining--;
        }
        else
        {
            HideBonusText();
        }
    }

    public void TroncoCollected()
    {
        currentTronco = null;
        spawnTimer = 0f;

        HideBonusText();
    }

    public void StopTroncos()
    {
        enabled = false;

        if (currentTronco != null)
        {
            Destroy(currentTronco);
        }

        currentTronco = null;

        HideBonusText();
    }

    private void HideBonusText()
    {
        showBonusText = false;
        bonusText.gameObject.SetActive(false);
    }
}