using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour {

    [SerializeField]
    private Slot[] slots;
    public Slot this[int index] { get { return slots[index]; } }
    private static Donut instance;
    public static Donut Instance { get { return instance; } }

    private bool inDoubleJump;
    private bool isGrounded;
                
        

    public int Score { get { return 0; } }

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float gravityScale;
    [SerializeField]
    private float jumpForce;

    new private Rigidbody2D rigidbody;



    void Awake()
    {
        instance = this;
        rigidbody = GetComponent<Rigidbody2D>();
    }


  
    void Start () {
	
	}
	
	
    void FixedUpdate()
    {
        ChekGround();
    }
  
	void Update () {

        Move();

        if (Input.GetKeyDown("space")) { Jump(); }

        rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, float.MinValue, jumpForce));

    }

    private void Move()
    {
        Gravity();
        rigidbody.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);

    }

    public void Jump()
    {
        if (!inDoubleJump)
        {
            float modifier = 1.0F;

            if (!isGrounded)
            {
                if (rigidbody.velocity.y <= -1.0F) modifier = 1.7F;
            }

            rigidbody.AddForce(Vector2.up * jumpForce * modifier, ForceMode2D.Impulse);
            inDoubleJump = true;
        }

    }

    private void ChekGround()
    {
        Vector2 positionDonut = transform.position;
        positionDonut.y -= 0.1F;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(positionDonut, 0.48F);
        if (colliders.Length > 1) { isGrounded = true; inDoubleJump = false; }
        else isGrounded = false;
    }
            
    private void Gravity()
    {
        rigidbody.AddForce(Vector2.down * gravityScale, ForceMode2D.Impulse);
    }

    public void PickUp(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == null)
            {
                slots[i].Item = item;
                item.transform.SetParent(slots[i].transform);
                item.transform.position = slots[i].transform.position;
                Destroy(item.GetComponent<Collider2D>());
                return;
            }; 
        }
    }

    public void DropItem(Obstacle obstacle)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Item != null)
            {
                Debug.Log("OK");
                slot.Item.transform.SetParent(obstacle.transform);
                slot.Item.transform.position = obstacle.transform.position;
                slot.Item = null;
                return;

            }
        }

    }



}
