
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float MaxSpeed;


    [SerializeField]
    float rotationSpeed;




    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        MoveForward();
        Rotate();
    }

    private void MoveForward()
    {
        var forwardInput = Input.GetAxis("Vertical");
        if (forwardInput != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        transform.Translate(Vector3.forward * forwardInput * MaxSpeed);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

    void Rotate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");

        Debug.Log(mouseX);

        transform.Rotate(new Vector3(0, mouseX * rotationSpeed * Time.deltaTime,0));
    }
}
