using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : Movement
{
    [SerializeField]
    protected float maxAcceletation;

    [SerializeReference, SubclassPicker]
    IControler controler;

    public override float MaxThrust => maxAcceletation;

    override protected void Move(Vector2 targetDirection)
    {
        thrust = Vector2.ClampMagnitude(targetDirection * maxAcceletation, maxAcceletation);

        AddThrust(thrust);
    }
    public override void AddThrust(Vector2 thrust)
    {
        rigidbody.AddForce(thrust * rigidbody.mass);
        rigidbody.AddForce(-rigidbody.velocity * maxAcceletation / maxSpeed);

        //Debug.Log("v: " + rigidbody.velocity.magnitude.ToString("F2") + "    a: " + (thrust.x * rigidbody.mass).ToString("F2") + "    real a: " + acceleration.ToString("F2"));
    }
    override protected void MoveTo(Vector2 targetPosition)
    {
        Vector2 errorVector = targetPosition - rigidbody.position;
        thrust = controler.Update(Time.fixedDeltaTime, errorVector, rigidbody, maxAcceletation, maxSpeed);
        thrust = Vector2.ClampMagnitude(thrust, maxAcceletation);

        AddThrust(thrust);
    }

}
