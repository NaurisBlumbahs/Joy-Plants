using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public bool interactableInSight;
    public Camera cam;

    private void Update()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction*100, Color.red);
        }
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                interactableInSight = true;
            }
            else
            {
                interactableInSight = false;
            }
        }
    }
}
