using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FireManager : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField] private float maxFire = 100f;
    [SerializeField] private float currentFire = 100f;
    [SerializeField] private float fireLossPerSecond = 1f;

    [Header("Campfire Visuals")]
    [SerializeField] private GameObject baseCampfire;
    [SerializeField] private GameObject flame;
    [SerializeField] private GameObject extinguishedCampfire;

    [Header("Flame Size")]
    [SerializeField] private float minimumFlameScale = 0.35f;
    [SerializeField] private float maximumFlameScale = 1f;

    [Header("UI")]
    [SerializeField] private Image fireBarFill;
    [SerializeField] private TMP_Text fireText;

    [Header("Game")]
    [SerializeField] private GameManager gameManager;

    public float CurrentFire
    {
        get { return currentFire; }
    }

    public float MaxFire
    {
        get { return maxFire; }
    }

    private void Start()
    {
        ResetFire();
    }

    private void Update()
    {
        ReduceFireOverTime();
    }

    private void ReduceFireOverTime()
    {
        if (currentFire <= 0)
        {
            return;
        }

        currentFire -= fireLossPerSecond * Time.deltaTime;

        if (currentFire <= 0)
        {
            currentFire = 0;
            FireExtinguished();
        }

        UpdateFireVisuals();
    }

    public void TakeDamage(float amount)
    {
        currentFire -= amount;

        if (currentFire <= 0)
        {
            currentFire = 0;
            FireExtinguished();
        }

        UpdateFireVisuals();
    }

    public void AddFire(float amount)
    {
        currentFire += amount;

        if (currentFire > maxFire)
        {
            currentFire = maxFire;
        }

        UpdateFireVisuals();
    }

    private void UpdateFireVisuals()
    {
        float firePercentage = currentFire / maxFire;
        float flameScale = Mathf.Lerp(minimumFlameScale, maximumFlameScale, firePercentage);

        flame.transform.localScale = new Vector3(flameScale, flameScale, 1f);
        fireBarFill.fillAmount = firePercentage;
        fireText.text = "CAMPFIRE " + Mathf.CeilToInt(currentFire) + "%";
    }

    private void FireExtinguished()
    {
        baseCampfire.SetActive(false);
        flame.SetActive(false);
        extinguishedCampfire.SetActive(true);

        gameManager.GameOver();
    }

    public void ResetFire()
    {
        currentFire = maxFire;

        baseCampfire.SetActive(true);
        flame.SetActive(true);
        extinguishedCampfire.SetActive(false);

        UpdateFireVisuals();
    }
}