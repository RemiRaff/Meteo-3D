using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private float speed = 1.0f;

    private bool rotateAllowed;
    private Vector2 rotation;
    private Transform myCamera;


    private void Awake()
    {
        pressed.Enable();
        axis.Enable();
        myCamera = Camera.main.transform;
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

 
    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        
        while (rotateAllowed)
        {
            rotation *= speed;
            transform.Rotate(-Vector3.up, rotation.x, Space.World);
            transform.Rotate(myCamera.right, rotation.y, Space.World);
            yield return null;
        }
    }
}
