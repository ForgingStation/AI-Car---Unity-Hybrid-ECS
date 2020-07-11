using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.AI;

public class AI_Drive_Entity : MonoBehaviour
{
    public float maxSpeed;
    public float maxAcceleration;
    public float maxMotorTorque;
    public float maxBreakTorque;
    public float maxSteerAngle;
    public GameObject destination;
    public NavMeshAgent agent;
    public float max_rb_agent_distance;

    private float currentSpeed;
    private EntityManager entitymamager;
    private Entity entity;
    private Rigidbody rb;
    private Drive_Bridge db;
    private float speedParameter;
    private float steerParameter;
    private float breakParameter;


    // Start is called before the first frame update
    void Start()
    {
        db = GetComponent<Drive_Bridge>();
        rb = GetComponentInParent<Rigidbody>();

        entitymamager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype ea = entitymamager.CreateArchetype(
                             typeof(AI_Drive_Component)
                             );
        entity = entitymamager.CreateEntity(ea);
        entitymamager.AddComponentData(entity, new AI_Drive_Component
        {
            maxAcceleration = maxAcceleration,
            maxSpeed = maxSpeed,
            maxMotorTorque = maxMotorTorque,
            maxBreakTorque = maxBreakTorque,
            maxSteerAngle = maxSteerAngle
        });
        agent.SetDestination(destination.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        AI_Drive_Component adc = entitymamager.GetComponentData<AI_Drive_Component>(entity);
        adc.rbLocalToWorld = rb.transform.localToWorldMatrix;
        adc.agentPosition = agent.transform.position;
        adc.currentVelocity = rb.velocity;
        entitymamager.SetComponentData(entity, adc);
        currentSpeed = adc.currentSpeed;
        speedParameter = adc.speedParameter;
        steerParameter = adc.steerParameter;
        breakParameter = adc.breakParameter;
        SetDriveParameters();
        ControlAgent(adc.rb_agent_distance);
    }

    private void ControlAgent(float dist)
    {
        if(dist > max_rb_agent_distance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    private void SetDriveParameters()
    {
        if (db != null)
        {
            db.steerParameter = steerParameter;
            db.breakParameter = breakParameter;
            if (currentSpeed < maxSpeed)
            {
                db.speedParameter = speedParameter;
            }
            else
            {
                db.speedParameter = 0;
            }
        }
    }
}
