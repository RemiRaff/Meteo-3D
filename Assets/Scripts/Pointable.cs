using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointable : MonoBehaviour
{
    [SerializeField] private InputAction pressed;

    private bool pointDone;
    private Transform myCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        pressed.Enable();
        myCamera = Camera.main.transform;
        pressed.performed += _ => { StartCoroutine(Pointer()); };
        pressed.canceled += _ => { pointDone = false; };
    }

    // Update is called once per frame
    private IEnumerator Pointer()
    {
        pointDone = true;
        
        while (pointDone)
        {
            // foward sur Z
            Debug.DrawRay(myCamera.position, Vector3.right * 30, Color.red);

            Ray ray = new Ray(myCamera.position, Vector3.right);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                print(hit.point);
            }
            yield return null;
        }
    }
}
