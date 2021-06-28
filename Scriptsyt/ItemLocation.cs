using UnityEngine;

public class ItemLocation : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Vector3 equippedPosition;
    [SerializeField] private Vector3 equippedRotation;
   
    public float XPosition, YPosition, ZPosition;
    public float XRotation, YRotation, ZRotation;

    private void Start()
    {
        equippedPosition = new Vector3(XPosition, YPosition, ZPosition);
        equippedRotation = new Vector3(XRotation, YRotation, ZRotation);        
    }

}

