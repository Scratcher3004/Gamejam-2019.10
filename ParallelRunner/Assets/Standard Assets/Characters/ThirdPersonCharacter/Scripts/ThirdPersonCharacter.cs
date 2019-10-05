using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;


		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
			m_Animator.applyRootMotion = false;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;
		}


		public void Move(Vector3 move, bool roll, bool jump)
		{
			UpdateAnimator();
			
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			move.Normalize();
			var move1 = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			//move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move1.x, move1.z);
			m_ForwardAmount = move.magnitude;

			m_Rigidbody.velocity = new Vector3(move.x * this.m_MoveSpeedMultiplier, m_Rigidbody.velocity.y,
				move.z * this.m_MoveSpeedMultiplier);
			
			ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			HandleGroundedMovement(roll, jump);
			if (!m_IsGrounded)
			{
				HandleAirborneMovement();
			}
			
			Debug.Log(m_Animator.applyRootMotion);

			ScaleCapsuleForCrouching(roll);
		}


		void ScaleCapsuleForCrouching(bool roll)
		{
			roll = roll || m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Roll";
			if (m_IsGrounded && roll)
			{
				if (m_Crouching) return;
				m_Capsule.height = m_Capsule.height / 2f;
				m_Capsule.center = m_Capsule.center / 2f;
				m_Crouching = true;
			}
			else
			{
				if (m_Crouching)
				{
					m_Capsule.height = m_CapsuleHeight * 2;
					m_Capsule.center = m_CapsuleCenter * 2;
					m_Crouching = false;
				}
			}
		}


		void UpdateAnimator()
		{
			// update the animator parameters
			m_Animator.SetBool("Running",m_ForwardAmount > 0.2f);
		}


		void HandleAirborneMovement()
		{
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			m_Rigidbody.AddForce(extraGravityForce);

			m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
		}


		void HandleGroundedMovement(bool roll, bool jump)
		{
			var currentClipName = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
			
			// check whether conditions are right to allow a jump:
			if (jump)
			{
				if (!roll && currentClipName != "JumpStanding" && currentClipName != "JumpRunning" && currentClipName != "Roll")
				{
					// jump!
					//m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
					m_IsGrounded = false;
					m_Animator.applyRootMotion = true;
					m_Animator.SetTrigger("Jump");
					m_GroundCheckDistance = 0.1f;
				}
			}

			if (currentClipName == "JumpStanding" || currentClipName == "JumpRunning" || jump)
			{}
			else
				m_Animator.applyRootMotion = false;
			
			if (roll && currentClipName != "JumpStanding" && currentClipName != "JumpRunning" && currentClipName != "Roll")
			{
				Debug.Log("Roll! 1");
				m_Animator.SetTrigger("Roll");
			}
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (m_IsGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
		}
	}
}
