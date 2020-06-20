
using UnityEngine;

[RequireComponent(typeof(AbilityManager))]
public class InputManager : MonoBehaviour
{
    [SerializeField]
    float MaxSpeed;
    [SerializeField]
    float rotationSpeed=10;



    Animator animator;
    Rigidbody rigidbody;



    [SerializeField]
    float consecutiveInputTime = 0;
    float consecutiveInputTimer;
    bool pressedBackTwice;

    private void Awake()
    { 
      
    }


    void Start()
    {
        consecutiveInputTimer = 0;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        
    }

    void Update()
    {
       
        
      

        pressedBackTwice = IsDashBackTriggered();
        consecutiveInputTimer -= Time.deltaTime;

        if (isOnGround()) {
            if (Input.GetButtonDown("DashLeft"))
            {
                GetComponent<AbilityManager>().DashLeft();
            }
            if (Input.GetButtonDown("DashRight"))
            {
                GetComponent<AbilityManager>().DashRight();
            }
            if (pressedBackTwice)
            {
                GetComponent<AbilityManager>().DashBack();
                pressedBackTwice = false;
            }
            MoveForward();
            
        }

        Attack();
        Rotate();
        if (Input.GetButtonDown("DashBehindTarget"))
        {
            GetComponent<AbilityManager>().DashBehind();
        }
    }

    void MoveForward()
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
    void Attack()
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
    bool isOnGround()
    {
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, Vector3.down);

        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, 10f))
        {
            return true;
        }
        return false;

    }
}
