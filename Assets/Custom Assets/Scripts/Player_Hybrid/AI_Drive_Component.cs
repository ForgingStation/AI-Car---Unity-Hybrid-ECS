using Unity.Entities;
using Unity.Mathematics;

public struct AI_Drive_Component : IComponentData
{
    public float maxMotorTorque;
    public float maxBreakTorque;
    public float maxSteerAngle;
    public float maxSpeed;
    public float maxAcceleration;

    public float3 currentVelocity;
    public float currentSpeed;

    public float3 agentPosition;
    public float3 inverseTransformPoint;
    public float4x4 rbLocalToWorld;
    public float rb_agent_distance;

    public float speedParameter;
    public float breakParameter;
    public float steerParameter;
}
