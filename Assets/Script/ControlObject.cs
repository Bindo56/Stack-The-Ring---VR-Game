using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlObject : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform originalParent;
    public float desiredDistance = 0.5f; // Adjust as needed

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalParent = transform.parent;
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Transform interactor = args.interactorObject.transform;
        transform.position = interactor.position + interactor.forward * desiredDistance;
    }
}
