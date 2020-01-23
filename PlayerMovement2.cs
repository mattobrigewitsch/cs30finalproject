using System.Collections;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{

	public enum DIRECTION { UP, DOWN, LEFT, RIGHT }
	private bool canMove = true, moving = false;
	private int speed = 3, buttonCooldown = 0;
	private DIRECTION dir = DIRECTION.DOWN;
	public Sprite northSprite;
	public Sprite midnorthSprite;
	public Sprite eastSprite;
	public Sprite mideastSprite;
	public Sprite southSprite;
	public Sprite midsouthSprite;
	public Sprite westSprite;
	public Sprite midwestSprite;
	private Vector3 pos;
	public GameObject ObjCheckRight;
	public bool NoObjInWayR = true;
	public bool NoObjInWayL = true;
	public bool NoObjInWayN = true;
	public bool NoObjInWayS = true;
	public bool isAllowedToMove = true;
	private ObjRightCheck rc;

	// Use this for initialization
	void Start()
	{
		isAllowedToMove = true;
		NoObjInWayR = true;
		NoObjInWayL = true;
		NoObjInWayN = true;
		NoObjInWayS = true;
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "ObjCheckRight")
		{
			NoObjInWayR = false;
			Debug.Log("Collision Right");
		}
		if (col.gameObject.tag == "ObjCheckLeft")
		{
			NoObjInWayL = false;
			Debug.Log("Collision Left");
		}
		if (col.gameObject.tag == "ObjCheckNorth")
		{
			NoObjInWayN = false;
			Debug.Log("Collision North");
		}
		if (col.gameObject.tag == "ObjCheckSouth")
		{
			NoObjInWayS = false;
			Debug.Log("Collision South");
		}
	}

	// Update is called once per frame
	void Update()
	{

		if (canMove)
		{
			pos = transform.position;
			move();
		}

		if (moving)
		{
			if (transform.position == pos)
			{
				moving = false;
				canMove = true;

				move();
			}
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
		}


	}
	private void move()
	{
		if (buttonCooldown <= 0)
		{
			if (Input.GetKey(KeyCode.W) && NoObjInWayN && isAllowedToMove)
			{
				NoObjInWayR = true;
				NoObjInWayL = true;
				NoObjInWayN = true;
				NoObjInWayS = true;

				if (dir != DIRECTION.UP)
				{
					dir = DIRECTION.UP;
				}
				else
				{
					canMove = false;
					moving = true;
					pos += Vector3.up;
					gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
				}
			}
			else if (Input.GetKey(KeyCode.S) && NoObjInWayS && isAllowedToMove)
			{
				NoObjInWayR = true;
				NoObjInWayL = true;
				NoObjInWayN = true;
				NoObjInWayS = true;

				if (dir != DIRECTION.DOWN)
				{
					dir = DIRECTION.DOWN;
				}
				else
				{
					canMove = false;
					moving = true;
					pos += Vector3.down;
					gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
				}
			}
			else if (Input.GetKey(KeyCode.A) && NoObjInWayL && isAllowedToMove)
			{
				NoObjInWayR = true;
				NoObjInWayL = true;
				NoObjInWayN = true;
				NoObjInWayS = true;

				if (dir != DIRECTION.LEFT)
				{
					dir = DIRECTION.LEFT;
				}
				else
				{
					canMove = false;
					moving = true;
					pos += Vector3.left;
					gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
				}
			}
			else if (Input.GetKey(KeyCode.D) && NoObjInWayR && isAllowedToMove)
			{
				NoObjInWayR = true;
				NoObjInWayL = true;
				NoObjInWayN = true;
				NoObjInWayS = true;

				if (dir != DIRECTION.RIGHT)
				{
					dir = DIRECTION.RIGHT;
				}
				else
				{
					canMove = false;
					moving = true;
					pos += Vector3.right;
					gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
				}
			}
			else if (Input.GetKey(KeyCode.D) && NoObjInWayR && isAllowedToMove)
			{
				NoObjInWayR = true;
				NoObjInWayL = true;
				NoObjInWayN = true;
				NoObjInWayS = true;

				if (dir != DIRECTION.RIGHT)
				{
					dir = DIRECTION.RIGHT;
				}
				else
				{
					canMove = false;
					moving = true;
					pos += Vector3.right;
					gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
				}
			}
		}

	}
}





