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

    [SerializeField]
    private ScoreKeeper scoreKeeper;

    public LayerMask aidLayer;

    private float timer;
    private GameObject look; 

    private float RestartTimer;
    public TargetSpawner spawner;


    // Start is called before the first frame update
    void Start()
    {

        if (!scoreKeeper)
            scoreKeeper = gameObject.GetComponentInParent<ScoreKeeper>();

        shoot = gameObject.GetComponent<Shoot>();
    }

    internal void Miss()
    {
        AddReward(-2f); 
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - RestartTimer > 60)
        {
            EndEpisode();
        }

        if (ScoreBoard != null)
            ScoreBoard.text = GetCumulativeReward().ToString("f4");

        //helping with training throug raycast
        RaycastHit hit; 
        if(Physics.Raycast(transform.position, transform.right, out hit, 30, aidLayer))
        {
            AddReward(0.5f);
            Debug.DrawLine(transform.position, hit.point); 

            if(hit.transform.gameObject == look)
            {
                
                //reward for looking at target
                AddReward(2f); 

                if(Time.time - timer > 3.5f)
                {  
                    // AddReward(-0.5f); 
                }

            } else
            {
                look = hit.transform.gameObject;

                AddReward(0.01f); 

                timer = Time.time; 
            }

        }
        else
        {
            AddReward(-0.05f);
        }

        //negative reward if no targets hit
        if(scoreKeeper.getAiScore() == 0)
        {
            // AddReward(-0.0001f); 
        }

        //reward for moving toward target
        Vector3 targetDir = spawner.LastShot.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.right);

        //Debug.Log(angle); 


        AddReward(-Mathf.Clamp(angle - 5, 0, 360)  / 360f);




    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Restarting Eposh");
        if (scoreKeeper != null)
        {
            scoreKeeper.clearScores();
        }
        RestartTimer = Time.time;
        spawner.Restart();
    }

    internal void hit()
    {
        AddReward(4f); 
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log("OnActionReceived");

        var vectorAction = actions.DiscreteActions;

        Vector3 rotation = Vector3.zero;


        //TODO: UNCOMMENT ACTIONS


        // horizontal rotation arm - Y
        if (vectorAction[0] != 0)
        {
            rotation.y = ArmRotationSpeed * (vectorAction[0] * 2 - 3) * Time.deltaTime;
            // Debug.Log("Rotate Arm Horizontal - " + vectorAction[0] + " | " + rotation.y);
        }

        // vertical rotation arm    - X
        if(vectorAction[1] != 0)
        {
            if(vectorAction[1] == 1)
            {
                rotation.z = ArmRotationSpeed * Time.deltaTime;
            }
            else if (vectorAction[1] == 2){
                rotation.z = ArmRotationSpeed * -Time.deltaTime;

            }
            //Debug.Log("Rotate Arm Vertical - " + vectorAction[1] + " | " + rotation.z);
        }

        // shoot
        if(vectorAction[2] != 0)
        {
            shoot.Fire();
            // Debug.Log("Shoot - " + vectorAction[2]);

            //AddReward(0.00001f);

        }

        /*if(rotation != Vector3.zero)
        {
            AddReward(0.001f);
        }else
        {
            AddReward(-0.001f);
        }*/

        transform.parent.Rotate(0, rotation.y, 0);
        transform.Rotate(0, 0, rotation.z);
        if (transform.localRotation.eulerAngles.z < 100 && transform.localRotation.eulerAngles.z >= 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(transform.localRotation.eulerAngles.z, 1, 75));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 1);
        }

        if(transform.localRotation.eulerAngles.z > 70 || transform.localRotation.eulerAngles.z < 5) {
            // Give penelty for keep looking up
            AddReward(-0.001f);
        }
        // transform.Rotate(rotation);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var keyboard = Keyboard.current;

        // Debug.Log("Heuristic");
        var actions = actionsOut.DiscreteActions;
        
        actions[0] = 0;
        if(keyboard.xKey.isPressed)
        {
            // Debug.Log("Input - Arm Turn Left");
            actions[0] = 1;     // left turn
        }
        else if (keyboard.cKey.isPressed)
        {
            // Debug.Log("Input - Arm Turn Right");
            actions[0] = 2;     // turn right
        }

        
        actions[1] = 0;
        if (keyboard.upArrowKey.isPressed)
        {
            // Debug.Log("Input - Arm Turn Up");
            actions[1] = 1;     // left up
        }
        else if (keyboard.downArrowKey.isPressed)
        {
            // Debug.Log("Input - Arm Turn Down");
            actions[1] = 2;     // turn down
        }

        actions[2] = 0;
        if (keyboard.spaceKey.isPressed)
        {
            // Debug.Log("Input - Shoot");
            actions[2] = 1;     // shoot
        }
    }
}