using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private Mission mission;
    [SerializeField] private TextMeshProUGUI missionNameText;
    [SerializeField] private TextMeshProUGUI missionInfoText;
    [SerializeField] private TextMeshProUGUI assignedAdventurersText;
    [SerializeField] private Button startMissionButton;
    [SerializeField] private Button completeMissionButton;

    void Start()
    {
        if (mission == null)
        {
            mission = GetComponent<Mission>();
        }

        if (startMissionButton != null)
        {
            startMissionButton.onClick.AddListener(StartMission);
        }

        if (completeMissionButton != null)
        {
            completeMissionButton.onClick.AddListener(CompleteMission);
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (mission == null) return;

        if (missionNameText != null)
        {
            missionNameText.text = mission.missionName;
        }

        if (missionInfoText != null)
        {
            string info = $"Level: {mission.missionLevel}\n";
            info += $"Duration: {mission.missionDuration:F1}s\n";
            info += $"Reward: {mission.rewardAmount}\n";
            info += $"Success Rate: {mission.successRate:F1}%\n";
            info += $"Slots: {mission.GetAssignedAdventurerCount()}/{mission.adventurersAllowed}";
            
            if (mission.missionStarted && !mission.missionCompleted)
            {
                info += "\n[MISSION IN PROGRESS]";
            }
            else if (mission.missionCompleted)
            {
                info += "\n[MISSION COMPLETED]";
            }
            
            missionInfoText.text = info;
        }

        if (assignedAdventurersText != null)
        {
            assignedAdventurersText.text = $"Assigned: {mission.GetAssignedAdventurerCount()}/{mission.adventurersAllowed}";
        }

        // Update button states
        if (startMissionButton != null)
        {
            startMissionButton.interactable = mission.CanStartMission();
        }

        if (completeMissionButton != null)
        {
            completeMissionButton.interactable = mission.missionStarted && !mission.missionCompleted;
        }
    }

    public void StartMission()
    {
        if (mission != null && mission.CanStartMission())
        {
            mission.StartMission();
        }
    }

    public void CompleteMission()
    {
        if (mission != null && mission.missionStarted && !mission.missionCompleted)
        {
            mission.CompleteMission();
        }
    }
}
