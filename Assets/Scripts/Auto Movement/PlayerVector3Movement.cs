using UnityEngine;


namespace AutoMovement
{
    public class PlayerVector3Movement : MonoBehaviour
    {
        [SerializeField] private Transform targetPosition;
        private float scalerDistance;
        private Vector3 vectorDistance;
        private float playerSpeed = 0.03f;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {   
            Debug.Log($"My position {transform.position}");
            Debug.Log($"Target position {targetPosition.position}");
        }

        // Update is called once per frame
        void Update()
        {
            scalerDistance = Vector3.Distance(transform.position, targetPosition.position); // Only the magnitude, not direction
            vectorDistance = targetPosition.position - transform.position; // Both magnitude and direction

            Vector3 targetPositionFixedY = new Vector3(targetPosition.position.x, transform.position.y, targetPosition.position.z);
            
            if (scalerDistance > 2.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionFixedY, playerSpeed + Time.deltaTime);
            }
        }
    }
}

