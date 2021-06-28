using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovementYt : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private float MoveSpeed = 5;
 
    private void FixedUpdate()
    {
        AllChecks();
    }
    public void AllChecks()
    {
        MoveFormula();       
    }  
    public void MoveFormula()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        Rb.MovePosition(transform.position + (transform.forward * z * MoveSpeed * Time.deltaTime) + (transform.right * x * MoveSpeed * Time.deltaTime));
    }
    
}



  
