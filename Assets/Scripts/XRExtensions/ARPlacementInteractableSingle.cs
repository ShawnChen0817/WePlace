using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementInteractableSingle : ARBaseGestureInteractable
{
    [SerializeField]
    private GameObject cabinet;

    [SerializeField]
    private ARObjectPlacementEvent onObjectPlaced;

    private GameObject placementObject;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static GameObject trackableObject;

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if(gesture.targetObject == null)
        {
            return true;
        }
        return false;
    }

    public void DestroyPlacementObject()
    {
        Destroy(placementObject);
    }
    protected override void OnEndManipulation(TapGesture gesture)
    {
        if(gesture.isCanceled)
        {
            return;
        }

        if(gesture.targetObject != null)
        {
            return;
        }

        if(GestureTransformationUtility.Raycast(gesture.startPosition,hits,TrackableType.PlaneWithinPolygon))
        {
            var hit = hits[0];

            if(Vector3.Dot(Camera.main.transform.position - hit.pose.position, hit.pose.rotation * Vector3.up ) < 0)
            {
                return;
            }
            //allowing a new game object for AR placement object
            if(placementObject == null)
            {
                placementObject = Instantiate(placementObject, hit.pose.position, hit.pose.rotation);
                var anchorObject = new GameObject("PlacementAnchor");
                anchorObject.transform.position = hit.pose.position;
                anchorObject.transform.rotation = hit.pose.rotation;
                
                if(trackableObject == null)
                {
                    trackableObject = GameObject.Find("Trackables");
                }

                if(trackableObject != null)
                {
                    anchorObject.transform.parent = trackableObject.transform;
                }

                //onObjectPlaced?.Invoke(this,placementObject);
            }
        }
    }
}