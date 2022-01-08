using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbableStoneBehavior : MonoBehaviour
{
    [SerializeField]
    private float handDistanceBeforeMove = 0.001f;

    private Vector3 stonePos;

    // Start is called before the first frame update
    void Start()
    {
        stonePos = this.gameObject.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            Debug.Log("ClimbableStone - Hand Detected");
            var handDevices = new List<UnityEngine.XR.InputDevice>();
            if (other.gameObject.transform.parent.gameObject.name.Contains("Left"))
                UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, handDevices);
            else if (other.gameObject.transform.parent.gameObject.name.Contains("Right"))
                UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, handDevices);
            
            if(handDevices.Count == 1)
            {
                bool triggerValue;
                if(handDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && triggerValue)
                {
                    Debug.Log("ClimbableStone - Grabbing");
                    other.gameObject.transform.parent.parent.parent.parent.GetComponentInChildren<ActionBasedContinuousMoveProvider>().gravityApplicationMode = ContinuousMoveProviderBase.GravityApplicationMode.AttemptingMove;

                    Vector3 handPos = other.gameObject.transform.position;

                    if (Vector3.Distance(stonePos, handPos) > handDistanceBeforeMove)
                    {
                        Vector3 diff = stonePos - handPos;
                        other.gameObject.transform.parent.parent.parent.parent.position += diff;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            other.gameObject.transform.parent.parent.parent.parent.GetComponentInChildren<ActionBasedContinuousMoveProvider>().gravityApplicationMode = ContinuousMoveProviderBase.GravityApplicationMode.Immediately;
        }
    }
}
