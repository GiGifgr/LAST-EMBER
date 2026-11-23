using TMPro;
using UnityEngine;

public class EnemyTapText : MonoBehaviour
{
    [SerializeField] private TMP_Text tapText;
    [SerializeField] private RectTransform rectTransform;

    private Transform target;
    private Camera gameplayCamera;
    private Vector3 worldOffset;

    public void Initialize(Transform newTarget, Camera newGameplayCamera, string newText, Vector3 newWorldOffset)
    {
        target = newTarget;
        gameplayCamera = newGameplayCamera;
        worldOffset = newWorldOffset;
        tapText.text = newText;

        UpdatePosition();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 worldPosition = target.position + worldOffset;
        Vector3 screenPosition = gameplayCamera.WorldToScreenPoint(worldPosition);
        rectTransform.position = screenPosition;
    }
}