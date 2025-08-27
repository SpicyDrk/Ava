using UnityEngine;

public class DragDropManager : MonoBehaviour
{
    public static DragDropManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static bool TryDropAdventurerIntoSlot(Adventurer adventurer, Vector3 dropPosition)
    {
        // Find all mission objects in the scene
        Mission[] allMissions = FindObjectsByType<Mission>(FindObjectsSortMode.None);
        
        foreach (var mission in allMissions)
        {
            if (mission != null && mission.HasAvailableSlots())
            {
                // Check if the drop position is near this mission
                float distance = Vector3.Distance(dropPosition, mission.transform.position);
                if (distance < 5.0f) // Adjust this distance as needed
                {
                    bool success = mission.TryAssignAdventurer(adventurer);
                    if (success)
                    {
                        Debug.Log($"Successfully assigned {adventurer.name} to {mission.missionName}");
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    public static void RemoveAdventurerFromMission(Adventurer adventurer)
    {
        Mission[] allMissions = FindObjectsByType<Mission>(FindObjectsSortMode.None);
        
        foreach (var mission in allMissions)
        {
            if (mission != null && mission.RemoveAdventurer(adventurer))
            {
                Debug.Log($"Removed {adventurer.name} from {mission.missionName}");
                break;
            }
        }
    }
}
