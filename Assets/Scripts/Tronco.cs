using UnityEngine;
using UnityEngine.EventSystems;

public class Tronco : MonoBehaviour, IPointerDownHandler
{
    [Header("Fire")]
    [SerializeField] private float fireAmount = 25f;

    private FireManager fireManager;
    private TroncoManager troncoManager;

    public void Initialize(FireManager newFireManager, TroncoManager newTroncoManager)
    {
        fireManager = newFireManager;
        troncoManager = newTroncoManager;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        CollectTronco();
    }

    private void CollectTronco()
    {
        fireManager.AddFire(fireAmount);
        troncoManager.TroncoCollected();

        Destroy(gameObject);
    }
}