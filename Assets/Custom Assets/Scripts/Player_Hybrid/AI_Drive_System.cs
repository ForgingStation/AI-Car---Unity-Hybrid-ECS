﻿using Unity.Entities;
using Unity.Mathematics;

public class AI_Drive_System : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref AI_Drive_Component adc) =>
        {
            adc.currentSpeed = math.sqrt(math.pow(adc.currentVelocity.x, 2) +
                                        math.pow(adc.currentVelocity.y, 2) +
                                        math.pow(adc.currentVelocity.z, 2));
            adc.speedParameter = adc.maxMotorTorque * adc.maxAcceleration * (1 - (adc.currentSpeed / adc.maxSpeed));
            adc.breakParameter = 0;
            adc.inverseTransformPoint = math.transform(math.inverse(adc.rbLocalToWorld), adc.agentPosition);
            adc.rb_agent_distance = math.sqrt(math.pow(adc.inverseTransformPoint.x, 2) +
                                    math.pow(adc.inverseTransformPoint.y, 2) +
                                    math.pow(adc.inverseTransformPoint.z, 2));
            adc.steerParameter = math.normalize(adc.inverseTransformPoint).x * adc.maxSteerAngle;
        }).ScheduleParallel();
    }
}
