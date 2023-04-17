using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public int layer = 8;
    public GameObject moveIcon;
    public float runSpeed = 10f;
    Animator animator;
    NavMeshAgent myNavMesh;


    public float cameraSmoothness = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        myNavMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        animationControl();
    }

    void animationControl() {
        if (myNavMesh.velocity.sqrMagnitude != 0f) {
            animator.SetBool("Running", true);
        }
        if (myNavMesh.velocity.sqrMagnitude == 0f)
        {
            animator.SetBool("Running", false);
        }

        if (Input.GetKey(KeyCode.Q)) {
            animator.SetBool("Slashattack", true);
        }
    }

    void movePlayer() {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.layer == layer)
                    {
                        myNavMesh.SetDestination(hit.point);
                        myNavMesh.speed = runSpeed;
                    }
                }
            }
        }
    }

    public void animationFinishedTrigger()
    {
        animator.SetBool("Slashattack", false);
    }
}
 