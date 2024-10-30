using UnityEngine;
using TMPro;

public class TargetScoring : MonoBehaviour
{
    public Transform targetCenter; // Reference to the center point of the target
    private float maxDistance; // Maximum distance based on target size
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro component for score display

    private float score = 0f; // To keep track of the total score

    private void Start()
    {
        if (targetCenter == null)
        {
            // Set targetCenter to the target's own transform if no specific center is set
            targetCenter = transform;
        }

        // Calculate maxDistance based on the target's size
        // Assuming the target's collider bounds can approximate the target size
        Collider targetCollider = GetComponent<Collider>();
        if (targetCollider != null)
        {
            maxDistance = targetCollider.bounds.extents.magnitude; // Using bounds for an approximate radius
        }
        else
        {
            maxDistance = 1.0f; // Default value if no collider is found
        }

        // Initialize the score display
        UpdateScoreDisplay(0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get the point of contact
        Vector3 contactPoint = collision.GetContact(0).point;

        // Calculate the distance from the contact point to the target center
        float distance = Vector3.Distance(contactPoint, targetCenter.position);

        // Calculate the score out of 100, based on proximity to the center
        float score = Mathf.Clamp(100 - (distance / maxDistance) * 100, 0, 100);

        // Update the score display
        UpdateScoreDisplay(score);
    }

    private void UpdateScoreDisplay(float score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        }
    }
}
