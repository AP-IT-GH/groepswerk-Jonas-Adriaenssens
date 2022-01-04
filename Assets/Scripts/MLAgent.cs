using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using UnityEngine.InputSystem;

public class MLAgent : Agent
{
    private Shoot shoot;

    public TextMeshPro ScoreBoard;

    public float RotationSpeed;
    public float ArmRotationSpeed;

    private ScoreHelper scoreHelper;
    private int oldScore = 0;
    private int oldPunishment = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!scoreHelper)
            scoreHelper = gameObject.GetComponentInParent<ScoreHelper>();

        shoot = gameObject.GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreBoard != null)
            ScoreBoard.text = GetCumulativeReward().ToString("f4");
    }

    public override void OnEpisodeBegin()
    {
        if(scoreHelper != null)
        {
            scoreHelper.resetScore();
            scoreHelper.resetPunishment();
        }    

        oldScore = 0;
        oldPunishment = 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("OnActionReceived");

        var vectorAction = actions.DiscreteActions;

        Vector3 rotation = Vector3.zero;

        // horizontal rotation arm - Y
        if(vectorAction[0] != 0)
        {
            rotation.y = ArmRotationSpeed * (vectorAction[0] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Arm Horizontal - " + vectorAction[0] + " | " + rotation.y);
        }

        // vertical rotation arm    - X
        if(vectorAction[1] != 0)
        {
            rotation.z = ArmRotationSpeed * (vectorAction[1] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Arm Vertical - " + vectorAction[1] + " | " + rotation.z);
        }

        Debug.Log(vectorAction[2]);

        // shoot
        if(vectorAction[2] != 0)
        {
            shoot.Fire();
            Debug.Log("Shoot - " + vectorAction[2]);
        }

        transform.Rotate(rotation);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var keyboard = Keyboard.current;

        Debug.Log("Heuristic");
        var actions = actionsOut.DiscreteActions;
        
        actions[0] = 0;
        if(keyboard.xKey.isPressed)
        {
            Debug.Log("Input - Arm Turn Left");
            actions[0] = 1;     // left turn
        }
        else if (keyboard.cKey.isPressed)
        {
            Debug.Log("Input - Arm Turn Right");
            actions[0] = 2;     // turn right
        }

        
        actions[1] = 0;
        if (keyboard.upArrowKey.isPressed)
        {
            Debug.Log("Input - Arm Turn Left");
            actions[1] = 1;     // left up
        }
        else if (keyboard.downArrowKey.isPressed)
        {
            Debug.Log("Input - Arm Turn Right");
            actions[1] = 2;     // turn down
        }

        actions[2] = 0;
        if (keyboard.spaceKey.isPressed)
        {
            Debug.Log("Input - Shoot");
            actions[2] = 1;     // shoot
        }
    }
}
