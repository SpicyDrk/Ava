using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMe : MonoBehaviour
{
    public int clickCount = 0;

    [SerializeField] TextMeshProUGUI clickCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickCountText.text = "Click Count: " + clickCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        clickCount++;
        clickCountText.text = "Click Count: " + clickCount;
    }
}
