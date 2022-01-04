using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingTarget)), CanEditMultipleObjects]
public class MovingTargetEditor : Editor
{
    void OnSceneGUI()
    {
        Handles.color = Color.red;
        MovingTarget myObj = (MovingTarget)target;
        if (myObj.transform.parent != null)
        {
            Handles.DrawWireArc(myObj.transform.parent.position, myObj.transform.parent.up, -myObj.transform.parent.right, 180, 25);
            Handles.ScaleValueHandle(5, myObj.transform.parent.position + myObj.transform.parent.forward * 25, myObj.transform.parent.rotation, 1, Handles.ConeHandleCap, 1);
        }
    }
}
