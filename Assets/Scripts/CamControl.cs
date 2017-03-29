// List of search terms – replace <> with angle [] brackets
// search <**todo> for code that still needs optimization/changing
// search <**critical> for code that needs fixing ASAP
// search <**bugsplat> for code that is buggy/broken/not working
// search <**debug> for blocks of code/functions that were commented out
// code used from: https://github.com/hsinwang5/Unity-Projects


using UnityEngine;
using System.Collections;

/* Camera script works by first rotating while on the center 
 * of the player, then using inverse transform.forward to move backwards in a straight line */

public class CamControl : MonoBehaviour
{

    public enum CAMERASTATE { DISABLED = 1, DEFAULT = 2, DIALOG = 3, STOPROTATION = 4 };

    public float rotateSpeed = 100f;
    public float zoomout = 10f;
    public float zoomin = 1f;
    public float zoomspeed = .2f;
    public float camheight = 1.5f;

    public GameObject Target;

    float horizontal;
    float vertical;
    float distance = 5f;
    float cameraZoomDistance;
    float savedDistance;                                                                                                //Saves previous distance before camera clip
    bool isClip = false;
    bool isScrollEnabled = true;
    bool notStoredYet = true;
    Vector3 last;
    Vector3 desired;
    LayerMask cameraClipMask;
    RaycastHit rayHit;

    void Start()
    {
        transform.position = Target.transform.position;
        cameraClipMask = LayerMask.GetMask("CameraClip");
    }

    //[**todo] implement camera damping and use generic target
    void LateUpdate()
    {


        if (Input.GetMouseButton(1))
        {
            horizontal = Input.GetAxis("Mouse X") * (rotateSpeed / 10);
            vertical = Input.GetAxis("Mouse Y") * (rotateSpeed / 10);
            transform.Rotate(vertical * -1, horizontal, 0);
            Cursor.lockState = CursorLockMode.Locked;
        }

        ZReset();

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        /*the following blocks of code allows the camera to move forwards and backwards
        * from the player, controlling zoom. 
        * [**todo] Use raycasting for a better, more accurate camera clip correction*/

        //constants for camera travel [**todo] Couldn't I improve this with Vector3.forward?
        float x = transform.forward.x * -1;
        float y = transform.forward.y * -1;
        float z = transform.forward.z * -1;

        Vector3 rayVector = new Vector3(x, y, z);

        //Input for scrolling with mousewheel
        if (isScrollEnabled)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance += scroll * -1 * zoomspeed;
        }

        cameraZoomDistance = Vector3.Distance(Target.transform.position, transform.position);

        //Sends out an invisible raycast line that searches for potential areas that could clip the camera,
        //and adjusts the zoom to compensate. 
        if (Physics.Raycast(new Vector3(Target.transform.position.x, Target.transform.position.y + .5f, Target.transform.position.z), rayVector, out rayHit, cameraZoomDistance + .2f, cameraClipMask))
        {
            if (notStoredYet)
            {
                savedDistance = distance;
                notStoredYet = false;
            }
            distance = rayHit.distance;
            isClip = true;
        }
        else
        {
            if (isClip)
            {
                distance = savedDistance;
                isClip = false;
                notStoredYet = true;
            }
        }

        //Clamp zoom in and zoom out distance
        distance = Mathf.Clamp(distance, zoomin, zoomout);

        //distance = Mathf.Lerp(lastDistance, distance, .9f);

        //Set Camera distance and height 
        print(distance);

        Vector3 vector = new Vector3(x * distance, y * distance, z * distance);
        Vector3 height = new Vector3(0f, camheight, 0f);

        transform.position = Target.transform.position + vector + height;
    }

    //This method resets the z-rotation of the camera to neutral if it gets tilted too much
    void ZReset()
    {
        if (transform.eulerAngles.z > .2 || transform.eulerAngles.z < -.2)
        {
            float x1 = transform.eulerAngles.x;
            float y1 = transform.eulerAngles.y;
            Quaternion reset = Quaternion.Euler(x1, y1, 0f);
            transform.rotation = reset;
        }
    }

}


