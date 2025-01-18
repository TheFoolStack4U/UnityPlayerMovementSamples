using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCCRotationMoveController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    private readonly float playerSpeed = 5f;
    private readonly float playerRotationSpeed = 5f;
    
    
    
    private CharacterController _characterController;
    private readonly float groundOffset = 0.3f;
    private readonly float gravity = 90.8f;
    private Vector3 verticalVelocity = Vector3.zero;
    
    
    private float scalerDistance;
    private Vector3 vectorDistance;
    private Vector3 vectorDirection;

    private void Awake()
    {
        // _characterController = GetComponent<CharacterController>();
        // verticalVelocity.y = verticalVelocity.y - groundOffset;
        // transform.position = new Vector3(transform.position.x, verticalVelocity.y, transform.position.z);
        

    }
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            Debug.Log("Ground detected: " + hit.collider.name);
        }
    }
    
    void Update()
    {
        /* scalerDistance is to check the distance between player and targetObject */
        scalerDistance = Vector3.Distance(transform.position, targetPosition.position); // Only the magnitude, not direction i.e scalar
        /* vectorDistance is to calculate distance and direction between player and targetObject */
        vectorDistance = targetPosition.position - transform.position; // Vector3 with both magnitude and direction
        /* vectorDirection is only getting the direction between player and targetObject */
        vectorDirection = vectorDistance.normalized; // vector 3 with magnitude 1 and only direction
        
        // Eliminate vertical component to ensure horizontal rotation only
        Vector3 finalDirection = new Vector3(vectorDistance.x, 0, vectorDistance.z).normalized;
        
        
        // vectorDirection.y = vectorDirection.y - groundOffset;
        // Vector3 movement = vectorDirection * (playerSpeed * Time.deltaTime);
        // _characterController.Move(movement);
        // Debug.Log(_characterController.isGrounded);
        // Debug.Break();
        
        if (_characterController.isGrounded)
        {
            Debug.Log("Grounded");
            verticalVelocity.y = verticalVelocity.y - groundOffset;
        }
        else
        {
            Debug.Log("Not grounded");
            verticalVelocity.y = (verticalVelocity.y - gravity) * Time.deltaTime;
        }
        
        if (scalerDistance > 1.5f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(finalDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
            
            Vector3 movement = vectorDirection * (playerSpeed * Time.deltaTime);
            movement += verticalVelocity * Time.deltaTime; // Add gravity effect
            _characterController.Move(movement);
        }
    }
    
    
}
