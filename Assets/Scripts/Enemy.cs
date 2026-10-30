using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerDownHandler
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform target;

    [Header("Visual")]
    [SerializeField] private Transform visual;

    [Header("Fire")]
    [SerializeField] private FireManager fireManager;

    [Header("Attack")]
    [SerializeField] private float attackDistance = 0.2f;

    [Header("Hits")]
    [SerializeField] private int hitsToKill = 1;

    private int currentHits = 0;

    public void Initialize(Transform newTarget, FireManager newFireManager)
    {
        target = newTarget;
        fireManager = newFireManager;
    }

    private void Update()
    {
        LookAtTarget();
        MoveToTarget();
    }

    private void LookAtTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        visual.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackDistance)
        {
            ReachCampfire();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        TakeHit();
    }

    private void TakeHit()
    {
        currentHits++;

        if (currentHits >= hitsToKill)
        {
            Die();
        }
    }

    private void ReachCampfire()
    {
        fireManager.TakeDamage(25f);
        Destroy(gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}