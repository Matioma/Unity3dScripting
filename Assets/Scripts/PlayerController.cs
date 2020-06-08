
using UnityEngine;

[RequireComponent(typeof(AbilityManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float MaxSpeed;


    [SerializeField]
    float rotationSpeed=10;


    public Vector3 MoveVector { get; set; }






    Animator animator;
    Rigidbody rigidbody;



    [SerializeField]
    float consecutiveInputTime = 0;
    float consecutiveInputTimer;

    bool pressedBackTwice;

    void Start()
    {
        consecutiveInputTimer = 0;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DashLeft")) {
            GetComponent<AbilityManager>().DashLeft();
        }
        if (Input.GetButtonDown("DashRight"))
        {
            GetComponent<AbilityManager>().DashRight();
        }
        if (pressedBackTwice) {
            GetComponent<AbilityManager>().DashBack();
            pressedBackTwice = false;
        }


        pressedBackTwice = IsDashBackTriggered();
        consecutiveInputTimer -= Time.deltaTime;

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

        rigidbody.velocity =new Vector3(0,rigidbody.velocity.y,0)+ transform.forward * forwardInput * MaxSpeed;
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
        transform.Rotate(new Vector3(0, horizontalInput * rotationSpeed * Time.deltaTime,0));
    }

    bool isOnGround() {
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, Vector3.down);

        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, 10f))
        {
            return true;
        }
        return false;
    
    }


    bool IsDashBackTriggered() {
        if (Input.GetButtonDown("S"))
        {
            if (consecutiveInputTimer < 0)
            {
                consecutiveInputTimer = consecutiveInputTime;
            }
            else {
                consecutiveInputTimer = -1;
                return true;
            }
        }
        return false;
    }


}
