using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 3f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    void Start()
    {
        // Attempt to find the active player character if the target isn't assigned
        if (target == null)
        {
            FindPlayerTarget();
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    // Dynamically find the active player character
    private void FindPlayerTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("No player object found with tag 'Player'. Make sure your player has the 'Player' tag.");
        }
    }

    // Public method to manually set the target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
