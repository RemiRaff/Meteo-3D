using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Pointable : MonoBehaviour
{
    [SerializeField] private InputAction pressed;

    private bool pointDone;
    private Camera myCamera;
    private Vector2 mousePos;
    
    private void Awake()
    {
        pressed.Enable();
        myCamera = Camera.main;

        pressed.performed += _ => { StartCoroutine(Pointer()); };
        pressed.canceled += _ => { pointDone = false; };
    }


    private IEnumerator Pointer()
    {
        pointDone = true;
        
        while (pointDone)
        {
            mousePos = Mouse.current.position.ReadValue();
            // foward sur Z, Debug.DrawRay(myCamera.position, Vector3.right * 30, Color.red);
            // Cast ray for Z position
            Ray ray = myCamera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, myCamera.nearClipPlane));
            // print("Bouton pressed, ray casted");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.gameObject == gameObject)
            {
                print(hit.point);
            }
            yield return null;
        }
    }
}
