using UnityEngine;

public class PlayerRotationRbMoveController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float playerSpeed;
    private Rigidbody _rigidbody;

    private float scalerDistance;
    private Vector3 vectorDirection;
    private bool isFirstFrame = true;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        if (isFirstFrame)
        {
            isFirstFrame = false; // Skip the first frame
            return;
        }
        
        // float cappedDeltaTime = Mathf.Min(Time.fixedDeltaTime, 0.05f); // Cap delta time to 0.05 seconds
        scalerDistance = Vector3.Distance(transform.position, targetPosition.position); // Only the magnitude, not direction i.e scalar
        vectorDirection = (targetPosition.position - transform.position).normalized; 
        

        if (scalerDistance > 1.5f)
        {
            Vector3 movement = vectorDirection * playerSpeed * Time.fixedDeltaTime ; // Movement
            Quaternion targetRotation = Quaternion.LookRotation(vectorDirection); // Rotation

            _rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime)); // Rotate towards target rotation
            _rigidbody.MovePosition(transform.position + movement);
        }   
        else
        {
            // Stop any unwanted rotation
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
