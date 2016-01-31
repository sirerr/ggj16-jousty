using UnityEngine;

using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public float m_Speed = 20f;
	public float m_TurnSpeed = 6f;
	public GameObject bodyObj;
	private string m_VertAxisName;
	private string m_HorizAxisName;
	private Rigidbody m_Rigidbody;
	public int m_PlayerNumber;
	private Animator m_Animator;
	private Transform m_Transform;
	public int m_Health = 3;

	Vector3 movement;
	Vector3 turn;

	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Animator = GetComponent<Animator> ();
		m_Transform = GetComponent<Transform> ();
	}

	private void OnEnable ()
	{
		// When the tank is turned on, make sure it's not kinematic.
		m_Rigidbody.isKinematic = false;
	}


	private void OnDisable ()
	{
		// When the tank is turned off, set it to kinematic so it stops moving.
		m_Rigidbody.isKinematic = true;
	}

	// Use this for initialization
	void Start () {
		bodyObj = transform.GetChild (0).gameObject;
		m_VertAxisName = "Vertical" + m_PlayerNumber;
		m_HorizAxisName = "Horizontal" + m_PlayerNumber;
	}
	
	// Update is called once per frame
	void Update () {


		float isPoke = Input.GetAxis("RightTrigger" + m_PlayerNumber);
		if (isPoke > 0.5f) {
			m_Animator.SetTrigger ("Attack");
		}
		/*
		bool startPressed = Input.GetButtonDown("Pause" + m_PlayerNumber);

		if (startPressed)
		{
			Debug.Log("Player that paused: " + m_PlayerNumber);
			Game.manager.TogglePause(m_PlayerNumber);
			startPressed = false;
		
		}
*/
	}

	private void Move (float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * m_Speed * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}

	private void FixedUpdate ()
	{
		// Adjust the rigidbody's position and orientation in FixedUpdate.
		float h = Input.GetAxis(m_HorizAxisName);
		float v = Input.GetAxis(m_VertAxisName);

		Move (h, v);

		float rh = Input.GetAxis("RotateX" + m_PlayerNumber);
		float rv = Input.GetAxis("RotateY" + m_PlayerNumber);
		if ((Mathf.Abs (rh) > 0.1f) || Mathf.Abs (rv) > 0.1f) {
			Turn (rh, rv);
		}
	}

	private void Turn (float rh, float rv)
	{
		turn.Set (-rh, 0f, rv);
		m_Transform.rotation = Quaternion.RotateTowards (m_Transform.rotation, Quaternion.LookRotation(turn), m_TurnSpeed * Time.deltaTime);
		
	}

	void OnTriggerEnter(Collider other)
	{

	}
	
	public void Damage (int damage)
	{
		m_Health -= damage;
		if (m_Health <= 0)
		{
			Die();
		}
	}
	
	public bool IsAlive()
	{
		return m_Health > 0;
	}
	
	public void Die ()
	{
		gameObject.SetActive(false);
		Game.manager.GetComponent<GameManager>().CheckPlayersAlive();
	}
}
