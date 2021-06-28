using UnityEngine;

public class RayCastHitItem : MonoBehaviour
{
    [SerializeField] private const float MaxDistance = 5f;

    public InSlotController inSlotController;

    private BoxCollider RayCastingBoxColliderTrigger = null;
    private BoxCollider RayCastingBoxColliderMesh = null;
    private Rigidbody RayCastingRb = null;

    [SerializeField] private BoxCollider EquippedBoxColliderTrigger = null;
    [SerializeField] private BoxCollider EquippedBoxColliderMesh = null;
    [SerializeField] private Rigidbody EquippedRb = null;

    [SerializeField] private Transform WeaponSlot, items;
    [SerializeField] private ItemLocation itemLocation;

    [SerializeField] private bool RayHit = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RayCasting();
            Debug.Log("Pressed E");
            WeaponSwitch();
        }
    }
    public void RayCasting()
    {
        Transform cam = Camera.main.gameObject.transform;
        var ray = new Ray(cam.position, cam.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                RayHit = true;
                Debug.Log("RayHitItem");

                if (hit.collider.GetComponentInChildren<ItemLocation>() != null)
                {
                    itemLocation = hit.collider.gameObject.GetComponent<ItemLocation>();
                }
                if (hit.collider.GetComponentInChildren<Rigidbody>() != null)
                {
                    RayCastingRb = hit.collider.gameObject.GetComponent<Rigidbody>();
                }
                if (hit.collider.GetComponentInChildren<BoxCollider>() != null)
                {
                    RayCastingBoxColliderTrigger = hit.collider.gameObject.GetComponent<BoxCollider>();
                }
                if (hit.collider.GetComponentInChildren<BoxCollider>() != null)
                {
                    RayCastingBoxColliderMesh = hit.collider.gameObject.GetComponent<BoxCollider>();
                }
            }
            else
            {
                RayCastingRb = null;
                RayCastingBoxColliderMesh = null;
                RayCastingBoxColliderTrigger = null;
                
                RayHit = false;
                Debug.Log("Ray Not Hitting Item");
            }
        }
    }
    public void WeaponSwitch()
    {
        if (inSlotController.weaponSlotFull == true)
        {
            inSlotController.weaponSlotFull = false;
            
            Drop();
        }
        else if (inSlotController.weaponSlotFull == false && RayHit == true)
        {
            Debug.Log("PickupRunning");
            inSlotController.weaponSlotFull = true;
            Pickup();
           
        }

    }

    public void SetNewParentToHands()
    {
        Debug.Log("Parenting Item To WeaponSlot");

        RayCastingRb.transform.SetParent(WeaponSlot);
        RayCastingRb.transform.localPosition = new Vector3(itemLocation.XPosition, itemLocation.YPosition, itemLocation.ZPosition);
        RayCastingRb.transform.localEulerAngles = new Vector3(itemLocation.XRotation, itemLocation.YRotation, itemLocation.ZRotation);
        

        Debug.Log("ConstraintsPickup");
        RayCastingRb.constraints = RigidbodyConstraints.FreezeAll;
        RayCastingRb.isKinematic = true;
        RayCastingBoxColliderMesh.enabled = false;
        RayCastingBoxColliderTrigger.enabled = false;

        RayHit = false;
    }

    public void Pickup()
    {       
        Debug.Log(" Pickup Item");
        EquippedRb = RayCastingRb;
        EquippedBoxColliderMesh = RayCastingBoxColliderMesh;
        EquippedBoxColliderTrigger = RayCastingBoxColliderTrigger;

       
        SetNewParentToHands();
    }
    public void Drop()
    {
        inSlotController.weaponSlotFull = false;

        EquippedRb.transform.SetParent(items);
        EquippedRb.AddForce(new Vector3(2.5f, 2.5f, 2.5f));
        EquippedRb.constraints = RigidbodyConstraints.None;
        EquippedRb.isKinematic = false;

        EquippedBoxColliderMesh.enabled = true;
        EquippedBoxColliderTrigger.enabled = true;

        EquippedBoxColliderMesh = null;
        EquippedBoxColliderTrigger = null;
        EquippedRb = null;
       
        Debug.Log("DroppingItem");        
    }
}







