using UnityEngine;

namespace AutoMovement
{
    namespace Rotation
    {
        public class PlayerVector3Movement : MonoBehaviour
        {
            [SerializeField] private Transform targetPosition;
            private float scalerDistance;
            private Vector3 vectorDistance;
            private float playerSpeed = 0.03f;
            private readonly float playerRotationSpeed = 1f;
            private bool stopRotation = false;
            private bool isFirstFrame = true;
            
            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Start()
            {   
                Debug.Log($"My position {transform.position}");
                Debug.Log($"Target position {targetPosition.position}");
            }

            // Update is called once per frame
            void Update()
            {
                if (isFirstFrame)
                {
                    isFirstFrame = false; // Skip the first frame
                    return;
                }
                
                scalerDistance = Vector3.Distance(transform.position, targetPosition.position); // Only the magnitude, not direction
                vectorDistance = targetPosition.position - transform.position; // Both magnitude and direction

                Vector3 finalDirection = new Vector3(vectorDistance.x, 0, vectorDistance.z).normalized;

                if (!stopRotation)
                {
                    // vectorDirection is a vector with no magnitude and only direction i.e. direction towards the target object
                    Quaternion lookRotation = Quaternion.LookRotation(finalDirection); // It calculated the direction of the target object
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, playerRotationSpeed * Time.deltaTime);
                }
                
                Vector3 targetPositionFixedY = new Vector3(targetPosition.position.x, transform.position.y, targetPosition.position.z);
                
                if (scalerDistance > 2.2f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPositionFixedY, playerSpeed + Time.deltaTime);
                }
                else
                {
                    stopRotation = true;
                }
            }
        }
    }
}

