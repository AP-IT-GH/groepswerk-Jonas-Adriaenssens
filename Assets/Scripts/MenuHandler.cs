using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private Transform leftHand, rightHand;
    [SerializeField]
    private GameObject handMenu;
    [SerializeField]
    private Vector3 localPosition;
    [SerializeField]
    private Vector3 localRotation;

    bool oldLeftButtonState = false;
    bool oldRightButtonState = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count == 1)
        {
            if(handleDevice(leftHandDevices[0], ref oldLeftButtonState))
            {
                handMenu.transform.SetParent(leftHand);
                handMenu.transform.localPosition = localPosition;
                handMenu.transform.localRotation = Quaternion.Euler(localRotation);
            }
        }

        if(rightHandDevices.Count == 1)
        {
            if(handleDevice(rightHandDevices[0], ref oldRightButtonState))
            {
                handMenu.transform.SetParent(rightHand);
                handMenu.transform.localPosition = localPosition;

                handMenu.transform.localRotation = Quaternion.Euler(-localRotation);
                
            }
        }
    }

    bool handleDevice(UnityEngine.XR.InputDevice device, ref bool oldButtonState)
    {
        bool triggerValue = false;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue && oldButtonState != triggerValue)
        {
            Debug.Log("Menu Button Pressed");

            handMenu.SetActive(!handMenu.activeSelf);
            
        }
        oldButtonState = triggerValue;

        return triggerValue;
    }
}
