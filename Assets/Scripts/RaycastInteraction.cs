using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    private RaycastReceiver lastHit;
    private Ray ray;
    public LayerMask layerMask;
    public float maxDistance = 5;

    private void Update()
    {
        RaycastHit raycast;
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out raycast, maxDistance, layerMask))
        {
            lastHit = raycast.transform.gameObject.GetComponent<RaycastReceiver>();
            lastHit.selected = true;
        }else if (lastHit != null)
        {
            lastHit.selected = false;
            Debug.Log(lastHit.gameObject.name);
        }
    }
}
