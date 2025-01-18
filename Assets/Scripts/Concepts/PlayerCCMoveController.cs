using UnityEngine;

public class PlayerCCMoveController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    private readonly float playerSpeed = 5f;
    private CharacterController _characterController;
    
    
    private float scalerDistance;
    private Vector3 vectorDistance;
    private Vector3 vectorDirection;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        scalerDistance = Vector3.Distance(transform.position, targetPosition.position); // Only the magnitude, not direction i.e scalar
        vectorDistance = targetPosition.position - transform.position; // Vector3 with both magnitude and direction
        vectorDirection = vectorDistance.normalized; // vector 3 with magnitude 1 and only direction

        if (scalerDistance > 1.5f)
        {
            Vector3 movement = vectorDirection * (playerSpeed * Time.deltaTime);
            _characterController.Move(movement);
        }
    }
}
