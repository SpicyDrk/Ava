using UnityEngine;

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Create adventurersAllowed sprites below for adventurer slots
        for (int i = 0; i < adventurersAllowed; i++)
        {
            // Create and position each adventurer slot sprite
            GameObject slot = new GameObject($"AdventurerSlot_{i}");
            slot.transform.SetParent(transform);
            slot.transform.localPosition = new Vector3(i * 2.0f, 0, 0); // Position them in a row
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



    void StartMission()
    {
        if (missionStarted) return;
        missionStarted = true;
        // Logic to start the mission
        Debug.Log($"Mission {missionName} started!");
    }

    void CompleteMission()
    {
        missionCompleted = true;
        // Logic to complete the mission
        Debug.Log($"Mission {missionName} completed!");
    }
}
