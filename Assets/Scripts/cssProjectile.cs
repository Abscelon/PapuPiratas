using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class cssProjectile : MonoBehaviour {

    public Rigidbody2D myRigidBody;
    public Vector3 myInitialPos;
    public bool inspected, clicked;
    public float shotStrength, myGravity;

	void Start ()
    {
        myInitialPos = this.transform.position;
	}

    private void OnMouseDrag()
    {
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePos.z = transform.position.z;
        clicked = true;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, currentMousePos, 0.3f);
    }

    private void OnMouseUp()
    {
        if (clicked)
        {
            myRigidBody.gravityScale = myGravity;
            myRigidBody.velocity = Vector3.Lerp(myRigidBody.velocity, (myInitialPos - transform.position)*shotStrength, 0.3f);
        }
    }

    private void OnMouseEnter()
    {
        inspected = true;
    }

    private void OnMouseExit()
    {
        inspected = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Scene myScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(myScene.name);
        }
    }
}
