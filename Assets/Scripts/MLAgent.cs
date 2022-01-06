using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using UnityEngine.InputSystem;

public class MLAgent : Agent
{
    [SerializeField]
    private Shoot shoot;

    public TextMeshPro ScoreBoard;

    public float RotationSpeed;
    public float ArmRotationSpeed;

    private ScoreKeeper scoreKeeper;

    public LayerMask aidLayer; 

    // Start is called before the first frame update
    void Start()
    {
        if (!scoreKeeper)
            scoreKeeper = gameObject.GetComponentInParent<ScoreKeeper>();

        shoot = gameObject.GetComponent<Shoot>();
    }

    internal void Miss()
    {
        AddReward(-1f); 
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreBoard != null)
            ScoreBoard.text = GetCumulativeReward().ToString("f4");

        //helping with training throug raycast
        RaycastHit hit; 

        if(Physics.Raycast(transform.position, transform.right, out hit, 30, aidLayer))
        {
            AddReward(0.5f);
            Debug.Log("Raycast aider did hit"); 
            Debug.DrawLine(transform.position, hit.point); 
        }


    }

    public override void OnEpisodeBegin()
    {
        if(scoreKeeper != null)
        {
            scoreKeeper.clearScores();
        }    

    }

    internal void hit()
    {
        AddReward(3f); 

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("OnActionReceived");

        var vectorAction = actions.DiscreteActions;

        Vector3 rotation = Vector3.zero;

        // horizontal rotation arm - Y
        if (vectorAction[0] != 0)
        {
            rotation.y = ArmRotationSpeed * (vectorAction[0] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Arm Horizontal - " + vectorAction[0] + " | " + rotation.y);

            AddReward(0.001f);

        }

        // vertical rotation arm    - X
        if(vectorAction[1] != 0)
        {
            rotation.z = ArmRotationSpeed * (vectorAction[1] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Arm Vertical - " + vectorAction[1] + " | " + rotation.z);


            AddReward(0.001f);

        }

        // shoot
        if(vectorAction[2] != 0)
        {
            shoot.Fire();
            Debug.Log("Shoot - " + vectorAction[2]);
      
        }

        transform.parent.Rotate(0, rotation.y, 0);
        transform.Rotate(0, 0, rotation.z);
        // transform.Rotate(rotation);
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
            Debug.Log("Input - Arm Turn Up");
            actions[1] = 1;     // left up
        }
        else if (keyboard.downArrowKey.isPressed)
        {
            Debug.Log("Input - Arm Turn Down");
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