using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/*
    Place battle arena on flat surfaces
    Does this via raycasting against detected planes
    
*/
public class ARPlacementManager : MonoBehaviour
{

    ARRaycastManager rayCast;
    static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();
    //ar camera object
    public Camera aRCamera;
    //Battle arena gameb object
    public GameObject battleArena;

    
    private void Awake()
    {
    //Get the raycast manager
     rayCast = GetComponent<ARRaycastManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the centre point of the screen
        Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        // Ray cast from the centre of the screen
        Ray ray = aRCamera.ScreenPointToRay(centerScreen);
        //take rays coming from the centre of the screen
        //the ray cast hits
        //only take plans that are polygon
        if  (rayCast.Raycast(ray,raycast_Hits,TrackableType.PlaneWithinPolygon))
        {

            //if it gets inside this if statment a ray has been sent and has detected a valid plane

            //Get the pose of the hitget the first hit
            //Takes the first item because hits are sorted by distance
            Pose hitPose = raycast_Hits[0].pose;
            //position to place the arena
            Vector3 positionToBePlaced = hitPose.position;
            //place the battle arena 
            battleArena.transform.position = positionToBePlaced;
        }
    }
}
