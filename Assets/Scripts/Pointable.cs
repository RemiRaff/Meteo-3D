using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Pointable : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    [SerializeField] private GameObject clickPoint;
    [SerializeField] private GameObject pointableObject;

    private bool pointDone;
    private Camera myCamera;
    private Vector2 mousePos;
    private float longitude, latitude;
    
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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.gameObject == pointableObject)
            {
                clickPoint.transform.position = hit.point;
                Vector3 lPos = transform.InverseTransformPoint(clickPoint.transform.position); // Vector3 wPos = transform.TransformPoint(lPos);
                longitude = Mathf.Atan(lPos.z/lPos.x)*180/Mathf.PI; // convertion en degrès, les axes sont à modifier
                latitude = 90 - Mathf.Acos(lPos.y/Mathf.Sqrt(lPos.x*lPos.x + lPos.y*lPos.y + lPos.z*lPos.z))*180/Mathf.PI;

                print(latitude + " : " + longitude);
            }
            yield return null;
        }
    }
}
