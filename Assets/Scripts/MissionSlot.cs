using UnityEngine;

public class MissionSlot : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;
    [SerializeField] private Adventurer assignedAdventurer = null;
    [SerializeField] private BoxCollider slotCollider;
    
    private Material originalMaterial;
    private Renderer slotRenderer;
    
    // Visual feedback materials (assign these in the inspector)
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material occupiedMaterial;

    void Start()
    {
        slotCollider = GetComponent<BoxCollider>();
        slotRenderer = GetComponent<Renderer>();
        
        if (slotRenderer != null)
        {
            originalMaterial = slotRenderer.material;
        }
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public bool CanAcceptAdventurer()
    {
        return !isOccupied;
    }

    public bool AssignAdventurer(Adventurer adventurer)
    {
        if (!CanAcceptAdventurer())
        {
            return false;
        }

        assignedAdventurer = adventurer;
        isOccupied = true;
        
        // Position the adventurer in the slot
        adventurer.transform.position = transform.position;
        adventurer.SetOnMission(true);
        
        // Update visual feedback
        UpdateSlotVisual();
        
        Debug.Log($"Adventurer {adventurer.name} assigned to mission slot");
        return true;
    }

    public void RemoveAdventurer()
    {
        if (assignedAdventurer != null)
        {
            assignedAdventurer.SetOnMission(false);
            assignedAdventurer = null;
        }
        
        isOccupied = false;
        UpdateSlotVisual();
    }

    public Adventurer GetAssignedAdventurer()
    {
        return assignedAdventurer;
    }

    public void OnAdventurerEnter()
    {
        // Visual feedback when adventurer is dragged over
        if (CanAcceptAdventurer() && slotRenderer != null && highlightMaterial != null)
        {
            slotRenderer.material = highlightMaterial;
        }
    }

    public void OnAdventurerExit()
    {
        // Remove visual feedback
        UpdateSlotVisual();
    }

    private void UpdateSlotVisual()
    {
        if (slotRenderer == null) return;

        if (isOccupied && occupiedMaterial != null)
        {
            slotRenderer.material = occupiedMaterial;
        }
        else if (originalMaterial != null)
        {
            slotRenderer.material = originalMaterial;
        }
    }

    // Called when something enters the slot's trigger zone
    void OnTriggerEnter(Collider other)
    {
        Adventurer adventurer = other.GetComponent<Adventurer>();
        if (adventurer != null && adventurer.IsBeingDragged())
        {
            OnAdventurerEnter();
        }
    }

    // Called when something exits the slot's trigger zone  
    void OnTriggerExit(Collider other)
    {
        Adventurer adventurer = other.GetComponent<Adventurer>();
        if (adventurer != null && adventurer.IsBeingDragged())
        {
            OnAdventurerExit();
        }
    }
}
