using UnityEngine;
using UnityEngine.UI;

public class CCameraMovement : MonoBehaviour
{
    public Text text; // FOR DEBUGGING REMOVE FOR RELEASE
    #region Public Variables
    [Tooltip("Higher value = smoother zoom.")]
    [Range(0.1f, 1.0f)]
    public float zoomSmooth;
    public float minZoom;
    public float maxZoom;
    [Tooltip("Camera movement speed.")]
    [Range(1f,10f)]
    public float moveSpeed;
    #endregion

    #region Private Variables
    // for zooming
    private Vector3 finger0;
    private Vector3 finger1;
    private float currDist;
    private float prevDist;
    private float fov;
    private float v = 0.0f;
    // for moving
    private Touch t;
    private Vector3 lerp;
    private Vector3 diff;
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Move();
        }
        else if (Input.touchCount == 2)
        {
            Zoom();
        }
        else // there are no fingers or more than 2
        {
            currDist = 0f; // zero the distances between fingers
        }
    }
    #endregion

    #region Private Methods
    private void Zoom()
    {
        finger0 = Input.GetTouch(0).position; // save finger 0 position
        finger1 = Input.GetTouch(1).position; // save finger 1 position
        currDist = Vector3.Distance(finger1, finger0); // calculate distance between fingers
        fov = Camera.main.fieldOfView; // save the current fov
        if (currDist > prevDist) // if current distance is greater than the previous frame
        {
            fov = Mathf.SmoothDamp(fov, fov - currDist * Time.deltaTime, ref v, zoomSmooth);
        }
        else if (prevDist > currDist) // if current distance is less than the previous frame
        {
            fov = Mathf.SmoothDamp(fov, fov + currDist * Time.deltaTime, ref v, zoomSmooth);
        }
        fov = Mathf.Clamp(fov, minZoom, maxZoom); // make sure fov stays within our limits
        Camera.main.fieldOfView = fov; // apply the fov change
        prevDist = currDist; // save the current distance to use next frame
        text.text = string.Concat(currDist, "-", fov); // DEBUGGING
    }

    private void Move()
    {
        t = Input.GetTouch(0);
        switch (t.phase)
        {
            case TouchPhase.Moved:
                diff = transform.position - ((Vector3)t.deltaPosition * moveSpeed);
                lerp = Vector3.Lerp(transform.position, diff, Time.deltaTime);
                transform.position = new Vector3(lerp.x, lerp.y, -10f);
                break;
            default:
                break;
        }
    }
    #endregion
}
