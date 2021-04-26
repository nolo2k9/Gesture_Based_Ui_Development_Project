using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlaneDetection_And_ARplacement : MonoBehaviour
{
    //Access the plane manager script
    ARPlaneManager planeManager;
    //Access the placement manager
    ARPlacementManager placementManager;

    public GameObject placeButton;
    public GameObject adjustButton;
    //Search for a game 
    public GameObject checkforGameButton;
    public GameObject scaleSlider;

    public TextMeshProUGUI informationPanel;

    private void Awake()
    {
        //Get the components
        planeManager = GetComponent<ARPlaneManager>();
        placementManager = GetComponent<ARPlacementManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Activate objects
        placeButton.SetActive(true);
        scaleSlider.SetActive(true);
        //deactivate objects
        adjustButton.SetActive(false);
        checkforGameButton.SetActive(false);
        
        informationPanel.text = "Move phone to detect planes and place the Battle Arena!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableDetection()
    {
        planeManager.enabled = false;
        placementManager.enabled = false;
        activation(false);

        scaleSlider.SetActive(false);
        placeButton.SetActive(false);
        adjustButton.SetActive(true);
        checkforGameButton.SetActive(true);

        informationPanel.text = "You have been placed. Search for a battle";
    }

    public void enableDetection()
    {
        //Enable plane manager to look for planes
        planeManager.enabled = true;
        //Enable placement manager to make placements
        placementManager.enabled = true;
        //Set action to true
        activation(true);
        //Activate slider
        scaleSlider.SetActive(true);
        //Activate place button
        placeButton.SetActive(true);
        //deactivate adjust button
        adjustButton.SetActive(false);
        //deactivate check for game button
        checkforGameButton.SetActive(false);

        informationPanel.text = "move your phone to find a suitable surface";  
    }

    private void activation(bool isActive)
    {
        //Get all the planes
        foreach (var plane in planeManager.trackables)
        {
            //Set them according to a isActive
            plane.gameObject.SetActive(isActive);
        }
    }
}
