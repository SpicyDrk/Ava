using UnityEngine;
using System.Collections.Generic;

public class Mission : MonoBehaviour
{
    public string missionName = "Mission Name";
    public int missionLevel = 1;
    public float missionDuration = 10.0f;
    public int rewardAmount = 10;
    public float successRate = 90.0f;

    public int adventurersAllowed = 1;
    public bool missionStarted = false;
    public bool missionCompleted = false;

    [SerializeField] private Transform[] slotPositions; // Assign slot positions in inspector
    private List<Adventurer> assignedAdventurers = new List<Adventurer>();
    private Dictionary<int, Adventurer> slotAssignments = new Dictionary<int, Adventurer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize slot assignments dictionary
        for (int i = 0; i < adventurersAllowed; i++)
        {
            slotAssignments[i] = null;
        }
        
        // Ensure we have slot positions defined
        if (slotPositions == null || slotPositions.Length == 0)
        {
            Debug.LogWarning($"Mission {missionName} has no slot positions defined!");
        }
        else if (slotPositions.Length > adventurersAllowed)
        {
            Debug.LogWarning($"Mission {missionName} has more slot positions ({slotPositions.Length}) than allowed adventurers ({adventurersAllowed})");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (missionStarted && !missionCompleted)
        {
            missionDuration -= Time.deltaTime;
            if (missionDuration <= 0)
            {
                CompleteMission();
            }
        }
    }



    public void StartMission()
    {
        if (missionStarted) return;
        if (assignedAdventurers.Count == 0)
        {
            Debug.LogWarning($"Cannot start mission {missionName} - no adventurers assigned!");
            return;
        }
        
        missionStarted = true;
        // Logic to start the mission
        Debug.Log($"Mission {missionName} started with {assignedAdventurers.Count} adventurers!");
    }

    public void CompleteMission()
    {
        missionCompleted = true;
        
        // Return adventurers from mission
        foreach (var adventurer in assignedAdventurers)
        {
            if (adventurer != null)
            {
                adventurer.SetOnMission(false);
            }
        }
        
        // Clear all slot assignments
        for (int i = 0; i < adventurersAllowed; i++)
        {
            slotAssignments[i] = null;
        }
        
        assignedAdventurers.Clear();
        
        // Logic to complete the mission
        Debug.Log($"Mission {missionName} completed!");
    }

    public bool TryAssignAdventurer(Adventurer adventurer)
    {
        if (missionStarted)
        {
            Debug.LogWarning("Cannot assign adventurer to mission that has already started!");
            return false;
        }

        if (assignedAdventurers.Count >= adventurersAllowed)
        {
            Debug.LogWarning($"Mission {missionName} already has maximum adventurers ({adventurersAllowed})!");
            return false;
        }

        // Find an empty slot
        for (int i = 0; i < adventurersAllowed; i++)
        {
            if (slotAssignments[i] == null)
            {
                // Assign to this slot
                slotAssignments[i] = adventurer;
                assignedAdventurers.Add(adventurer);
                adventurer.SetOnMission(true);
                adventurer.SetCurrentMission(this);
                
                // Position the adventurer at the slot position if available
                if (slotPositions != null && i < slotPositions.Length && slotPositions[i] != null)
                {
                    adventurer.transform.position = slotPositions[i].position;
                }
                
                Debug.Log($"Adventurer {adventurer.name} assigned to mission {missionName} in slot {i}");
                return true;
            }
        }

        Debug.LogWarning($"No available slots for adventurer in mission {missionName}");
        return false;
    }

    public bool RemoveAdventurer(Adventurer adventurer)
    {
        if (missionStarted)
        {
            Debug.LogWarning("Cannot remove adventurer from mission that has already started!");
            return false;
        }

        // Find the slot with this adventurer
        for (int i = 0; i < adventurersAllowed; i++)
        {
            if (slotAssignments[i] == adventurer)
            {
                slotAssignments[i] = null;
                assignedAdventurers.Remove(adventurer);
                adventurer.SetOnMission(false);
                adventurer.SetCurrentMission(null);
                Debug.Log($"Adventurer {adventurer.name} removed from mission {missionName}");
                return true;
            }
        }

        return false;
    }

    public bool CanStartMission()
    {
        return !missionStarted && assignedAdventurers.Count > 0;
    }

    public int GetAssignedAdventurerCount()
    {
        return assignedAdventurers.Count;
    }

    public bool HasAvailableSlots()
    {
        return assignedAdventurers.Count < adventurersAllowed;
    }
}
