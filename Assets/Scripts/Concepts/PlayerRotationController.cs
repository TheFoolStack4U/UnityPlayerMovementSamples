using UnityEngine;

namespace Concepts
{
    public class PlayerRotationController : MonoBehaviour
    {
        [SerializeField] private Transform targetPosition;
        private float scalerDistance;
        private Vector3 vectorDistance;
        private Vector3 vectorDirection;
        
        private readonly float playerSpeed = 5f;
        private readonly float playerRotationSpeed = 1f;
        
        private void Start()
        {
            Debug.Log($"My position {transform.position}");
            Debug.Log($"Target position {targetPosition.position}");
        }

        private void Update()
        {
            scalerDistance = Vector3.Distance(transform.position, targetPosition.position); // Only the magnitude, not direction
            vectorDistance = targetPosition.position - transform.position; // Both magnitude and direction
            vectorDirection = vectorDistance.normalized; // Vector with no magnitude but a direction // Unit Vector

            Vector3 finalDirection = new Vector3(vectorDistance.x, 0, vectorDistance.z).normalized;
            
            // vectorDirection is a vector with no magnitude and only direction i.e. direction towards the target object
            Quaternion lookRotation = Quaternion.LookRotation(finalDirection); // It calculated the direction of the target object
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, playerRotationSpeed * Time.deltaTime);
            // Debug.Log(vectorDirection);
            // Debug.Log(lookRotation);

            Vector3 targetPositionFixedY = new Vector3(targetPosition.position.x, transform.position.y, targetPosition.position.z);
            if (scalerDistance > 1.8f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionFixedY, playerSpeed * Time.deltaTime);
                // Vector3 movement = vectorDirection * playerSpeed * Time.deltaTime;
                // transform.position = transform.position + movement;
            }
        }
    }
}
