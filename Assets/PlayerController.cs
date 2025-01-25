using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeReference, SubclassPicker]
    Movement movement;

    Vector2 inputVector;
    // Start is called before the first frame update
    void Start()
    {
        movement.Start(GetComponent<Rigidbody2D>());
        movement.SwitchToDirectMovement(true);
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        movement.SetMovementVector(inputVector);
        
    }

    private void FixedUpdate()
    {
        movement.Update();
    }
}
