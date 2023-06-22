using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class ARSessionManager : MonoBehaviour
{
    [SerializeField] DataHandler arPlacementInteractable;
    [SerializeField] private ARPlaneManager aRPlaneManager;
    [SerializeField] private Button togglePlaneButton;
    [SerializeField] private Button ClearButton; 
    [SerializeField] private Sprite[] sprite; 
    public void ClearAllObjects()
    {
        arPlacementInteractable.DestroyPlacementObject();
    }
    public void TogglePlanes()
    {
        //enable代表為true 但是加了!則為false;
        aRPlaneManager.enabled = !aRPlaneManager.enabled;
        ChangeStateOfPlanes(aRPlaneManager.enabled);
        /*if(aRPlaneManager.enabled)
        {
            togglePlaneButton.image.sprite = sprite[1];
        }
        else
        {
            togglePlaneButton.image.sprite = sprite[0];
        }*/
        togglePlaneButton.image.sprite = aRPlaneManager.enabled ? sprite[1] : sprite[0];
    }

    private void ChangeStateOfPlanes(bool state)
    {
        var planes = aRPlaneManager.trackables;
        foreach(ARPlane arPlane in planes)
        {
            arPlane.gameObject.SetActive(state);
        }
    }
}
