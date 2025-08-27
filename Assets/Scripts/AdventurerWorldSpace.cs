using System.Diagnostics;
using TMPro;
using UnityEngine;

/// <summary>
/// Alternative Adventurer script that uses 3D world space TextMeshPro components
/// Use this version if your adventurer UI elements are in world space rather than UI Canvas
/// </summary>
public class AdventurerWorldSpace : MonoBehaviour
{
    enum AdventurerStatus { idle, onMission }

    [SerializeField] private string adventurerName;
    [SerializeField] private int adventurerLevel;
    [SerializeField] private bool isRecruited;
    [SerializeField] private int experience;
    [SerializeField] private AdventurerStatus status = AdventurerStatus.idle;

    // Use TextMeshPro for world space 3D text
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro levelText;
    [SerializeField] private TextMeshPro experienceBar;

    private GameObject _hireButton;
    private bool _beingDragged;
    private Vector3 _originalPosition;
    private Mission _currentMission;
    
    // Drag and drop related
    private Camera _mainCamera;
    private Vector3 _dragOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameText.text = adventurerName;
        levelText.text = adventurerLevel.ToString();
        //find child object with name "HireButton",
        _hireButton = transform.Find("HireButton").gameObject;
        
        // Store original position for returning after failed drops
        _originalPosition = transform.position;
        
        // Get reference to main camera
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            _mainCamera = FindFirstObjectByType<Camera>();
        }
        
        //experienceBar.fillAmount = experience / 100f; // Assuming 100 is the max experience
    }

    // Update is called once per frame
    void Update()
    {
        if (_beingDragged && _mainCamera != null)
        {
            // Handle dragging logic here
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Vector3.Distance(_mainCamera.transform.position, transform.position);
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x + _dragOffset.x, worldPosition.y + _dragOffset.y, transform.position.z);
        }
    }

    void OnMouseDown()
    {
        if (!isRecruited)
        {
            //log mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            UnityEngine.Debug.Log("Mouse position: " + worldPosition);

            //log position of hire button
            UnityEngine.Debug.Log("Hire button position: " + _hireButton.transform.position);

            // Check if the world position of the mouse click is within the hire button's collider bounds
            if (_hireButton.GetComponent<BoxCollider>().bounds.Contains(worldPosition))
            {
                isRecruited = true;
                _hireButton.SetActive(false);
                //log
                UnityEngine.Debug.Log("Adventurer hired");
                return; // Exit early to prevent dragging when hiring
            }
        }
        
        // Only start dragging if adventurer is recruited and not on mission
        if (isRecruited && status == AdventurerStatus.idle)
        {
            _beingDragged = true;
            _originalPosition = transform.position;
            
            // Calculate drag offset to prevent jumping
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Vector3.Distance(_mainCamera.transform.position, transform.position);
            Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            _dragOffset = transform.position - worldMousePosition;
            
            //log
            UnityEngine.Debug.Log("Dragging started");
        }
    }

    void OnMouseUp()
    {
        if (!isRecruited || !_beingDragged)
        {
            return;
        }
        
        _beingDragged = false;
        
        // Try to find a mission slot to drop into
        bool successfulDrop = TryDropIntoMissionSlot();
        
        if (!successfulDrop)
        {
            // Return to original position if drop failed
            transform.position = _originalPosition;
            UnityEngine.Debug.Log("Drop failed, returning to original position");
        }
        
        //log
        UnityEngine.Debug.Log("Dragging stopped");
    }

    private bool TryDropIntoMissionSlot()
    {
        // Find all missions in the scene
        Mission[] allMissions = FindObjectsByType<Mission>(FindObjectsSortMode.None);
        
        foreach (var mission in allMissions)
        {
            if (mission != null && mission.HasAvailableSlots())
            {
                // Check if the drop position is near this mission
                float distance = Vector3.Distance(transform.position, mission.transform.position);
                if (distance < 5.0f) // Adjust this distance as needed
                {
                    bool success = mission.TryAssignAdventurer(this);
                    if (success)
                    {
                        UnityEngine.Debug.Log($"Successfully assigned {name} to {mission.missionName}");
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    public bool IsBeingDragged()
    {
        return _beingDragged;
    }

    public void SetOnMission(bool onMission)
    {
        if (onMission)
        {
            status = AdventurerStatus.onMission;
        }
        else
        {
            status = AdventurerStatus.idle;
            _currentMission = null;
        }
    }

    public bool IsOnMission()
    {
        return status == AdventurerStatus.onMission;
    }

    public bool IsRecruited()
    {
        return isRecruited;
    }

    public int GetLevel()
    {
        return adventurerLevel;
    }

    public string GetAdventurerName()
    {
        return adventurerName;
    }

    public void SetCurrentMission(Mission mission)
    {
        _currentMission = mission;
    }

    public Mission GetCurrentMission()
    {
        return _currentMission;
    }
}
