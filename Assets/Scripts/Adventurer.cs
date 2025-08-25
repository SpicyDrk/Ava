using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Adventurer : MonoBehaviour
{

    enum AdventurerStatus { idle, onMission }

    [SerializeField] private string adventurerName;

    [SerializeField] private int adventurerLevel;
    [SerializeField] private bool isRecruited;
    [SerializeField] private int experience;

    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro levelText;
    [SerializeField] private TextMeshPro experienceBar;


    private GameObject _hireButton;

    private bool _beingDragged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameText.text = adventurerName;
        levelText.text = adventurerLevel.ToString();
        //find child object with name "HireButton"
        _hireButton = transform.Find("HireButton").gameObject;

        
        //experienceBar.fillAmount = experience / 100f; // Assuming 100 is the max experience
    }

    // Update is called once per frame
    void Update()
    {
        if (_beingDragged)
        {
            // Handle dragging logic here
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }
    void OnMouseDown()
    {
        if (!isRecruited)
        {
            //log mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
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
        
        // Only start dragging if adventurer is recruited
        if (isRecruited)
        {
            _beingDragged = true;
            //log
            UnityEngine.Debug.Log("Dragging started");
        }
    }

    void OnMouseUp()
    {
        if (!isRecruited)
        {
            return;
        }
        _beingDragged = false;
        //log
        UnityEngine.Debug.Log("Dragging stopped");
    }
}
