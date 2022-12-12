using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    public Transform cameraPivot; // el blanco que la camara hara pivote
    public Transform targetTransform; //el blanco seguido por la camara
    public Transform cameraTransform; //transforma el blanco de la camara en la escena
    public LayerMask collisionLayers; //en donde la camara colisionara
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;
    public float cameraCollisionOffSet = 0.2f; // cuanto se alejara la camara al chocar con un objeto 
    public float minimumCollisionOffSet = 0.2f;
    public float maximumCollisionOffSet = 0.2f;
    public float cameraCollisionRadius = 2;
    public float cameraFollowSpeed = 0.3f;
    public float lookAngle;
    public float pivotAngle;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity,cameraFollowSpeed);

        transform.position = targetPosition;
        
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle); //le da un maximo y minimo al angulo del pivoteo

        //eje y
        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
        
        //eje x
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation; 
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if(Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius,direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = - (distance - cameraCollisionOffSet);
        }

        if(Mathf.Abs(targetPosition)<minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition; 
    }
}
