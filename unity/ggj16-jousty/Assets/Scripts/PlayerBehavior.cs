using UnityEngine;

using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public float m_Speed = 60f;
	public float m_TurnSpeed = 320f;
	public float m_DefaultSpeed = 60f;
	public float m_DefaultTurnSpeed = 320f;
	public float m_DashSpeed = 100f;
	public float m_DashTurnSpeed = 160f;
	public GameObject bodyObj;
	private string m_VertAxisName;
	private string m_HorizAxisName;
	private Rigidbody m_Rigidbody;
	public int m_PlayerNumber;
	private Animator m_Animator;
	private Transform m_Transform;
	public int m_Health = 03;
	public AudioSource m_GruntingAudio;
	public AudioClip m_LungingClip;
	public AudioClip m_AltLungingClip;
	public AudioSource m_PlayerAudio;
	public AudioClip m_DeathClip;
	private bool m_AmDead;
	private float m_SpeedTimer;
	private float m_DashTimer;
	private float m_FireTimer;
	private bool m_FireSpearActivated;
	private Color m_DefaultColor;
	Vector3 movement;
	Vector3 turn;

	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Animator = GetComponent<Animator> ();
		m_Transform = GetComponent<Transform> ();
		m_AmDead = false;
		m_SpeedTimer = 0;
		m_DashTimer = 0;
		m_FireTimer = 0;
		m_FireSpearActivated = false;
		m_DefaultColor = Color.white;
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
	void Start ()
	{
		bodyObj = transform.GetChild (0).gameObject;
		m_VertAxisName = "Vertical" + m_PlayerNumber;
		m_HorizAxisName = "Horizontal" + m_PlayerNumber;
	}

	public void ChangeColor(Color c)
	{
		m_Transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color = c;
	}

	public void SpeedBoost()
	{
		m_DefaultColor = m_Transform.GetChild (0).gameObject.GetComponent<Renderer> ().material.color;
		ChangeColor (Color.yellow);
		m_SpeedTimer = 2;
	}
	public void Dash()
	{
		m_DashTimer = 0.5f;
	}
	public void FireSpear()
	{
		m_FireTimer = 4f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_SpeedTimer > 0) {
			m_Speed = 120f;
			m_TurnSpeed = 400f;
			m_SpeedTimer -= Time.deltaTime;
			if (m_SpeedTimer <= 0) {
				ChangeColor (m_DefaultColor);
				m_Speed = 60f;
				m_TurnSpeed = 320f;
			}
		}
		/* if (m_DashTimer > 0) {
			m_Speed = m_DashSpeed;
			m_TurnSpeed = m_DashTurnSpeed;
			m_DashTimer -= Time.deltaTime;
			if (m_DashTimer <= 0) {
				m_Speed = m_DefaultSpeed;
				m_TurnSpeed = m_DefaultTurnSpeed;
			}
			
		
		
		}*/
		if (m_FireTimer > 0) {
			if (!m_FireSpearActivated) {
				m_FireSpearActivated = true;
				m_Transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.SetActive (true);
			}
			m_FireTimer -= Time.deltaTime;
		} 
		if(m_FireTimer <= 0){
			if (m_FireSpearActivated) {
				m_FireSpearActivated = false;
				m_Transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.SetActive (false);
			}
		}

		float isPoke = Input.GetAxis("RightTrigger" + m_PlayerNumber);
		float isDash = Input.GetAxis("LeftTrigger" + m_PlayerNumber);
		if (isPoke > 0.5f && !m_AmDead) {
			if (Random.value < 0.5)
			{
				m_GruntingAudio.clip = m_LungingClip;
			} else {
				m_GruntingAudio.clip = m_AltLungingClip;
			}
			m_GruntingAudio.Play();
			m_Animator.SetTrigger("Attack");
		}
		if (isDash > 0.5f && !m_AmDead) {
			Debug.Log ("666");
			//m_Animator.SetTrigger("Dash");
			Dash();
		}
		/* PAUSE FUNCTIONALITY WILL BE IN FIRST DLC BUNDLE
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

		if (!m_AmDead) {
			Move (h, v);
		}

		float rh = Input.GetAxis("RotateX" + m_PlayerNumber);
		float rv = Input.GetAxis("RotateY" + m_PlayerNumber);
		if (((Mathf.Abs (rh) > 0.1f) || Mathf.Abs (rv) > 0.1f) && !m_AmDead) {
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
		m_AmDead = true;
		m_PlayerAudio.clip = m_DeathClip;
		m_PlayerAudio.Play();
		m_Transform.GetChild(0).gameObject.SetActive (false);
		m_Transform.GetChild (3).gameObject.SetActive (true);
		//gameObject.SetActive(false);
		Game.manager.GetComponent<GameManager>().CheckPlayersAlive();
	}
}
