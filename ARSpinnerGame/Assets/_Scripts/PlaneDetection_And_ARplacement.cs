using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlaneDetection_And_ARPlacement : MonoBehaviour
{
    //Access the plane manager script
    ARPlaneManager planeManager;
    //Access the placement manager
    ARPlacementManager placementManager;
    //Place button game object
    public GameObject placeButton;
    //Adjust button
    public GameObject adjustButton;
    //Search for a game 
    public GameObject checkforGameButton;
    //Inform the player about the current state of the game
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
        //Activate place buttons
        placeButton.SetActive(true);
       
        //deactivate adjust and check for game buttons
        adjustButton.SetActive(false);
        checkforGameButton.SetActive(false);
        
        //update the information panel
        informationPanel.text = "Move phone to detect planes and place the Arena!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableDetection()
    {
        //disable plane manager
        planeManager.enabled = false;
        //disable placement manager
        placementManager.enabled = false;
        //Activation method param is set to false
        activation(false);
 
        // Place button not shown
        placeButton.SetActive(false);
        // Adjust button shown
        adjustButton.SetActive(true);
        // Search game button shown
        checkforGameButton.SetActive(true);
         //update the information panel
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
        //Activate place button
        placeButton.SetActive(true);
         //deactivate adjust button
        adjustButton.SetActive(false);
          //deactivate check for game button
        checkforGameButton.SetActive(false);
         //update the information panel
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
