using System;
using UnityEngine;
using UnityEngine.Events;

namespace Actors.Movement
{
	[RequireComponent(typeof(Rigidbody))]
	public class UFOMovement : MonoBehaviour
	{
		public MovementState State => _state;
		
		[SerializeField] private MovementState _state = MovementState.Fly;
		[SerializeField] [Range(0, 20f)] private float _jumpForce;
		[SerializeField] [Range(-5, 5f)] private float _xPositionMovement;
		[SerializeField] [Range(0, 100f)] private float _rotationMultiplier = 20;
		[SerializeField] private UnityEvent _jumpEvent;
		private Rigidbody _rigidbody;
		
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			ChangeState((int)_state);
		}
		
        private void FixedUpdate()
        {
            MoveToward(new Vector2(_xPositionMovement, _rigidbody.velocity.y));
            RotateDown();
        }
        
        public void Jump()
        {
	        if(_state == MovementState.Fly) return;
	        if(this.enabled == false) return;//UnityEventы могут вызывать методы в выключеных компонентах kekw0_0
            
	        _jumpEvent.Invoke();
	        _rigidbody.velocity = Vector2.zero;
	        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode.Impulse);
        }

        public void ChangeState(int state)
        {
	        switch ((MovementState) state)
	        {
		        case MovementState.Fly:
			        _state = MovementState.Fly;
			        _rigidbody.useGravity = false;
			        break;
		        case MovementState.Falling:
			        _state = MovementState.Falling;
			        _rigidbody.useGravity = true;
			        Jump();
			        break;
	        }
        }
        
        private void MoveToward(Vector2 direction)
        {
	        _rigidbody.velocity = direction;
        }

        private void RotateDown()
        {
	        //TODO: Remove magic number
	        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(_rigidbody.velocity.y * (_rotationMultiplier / 2), -80, 50));
        }
		
		[Serializable]
		public enum MovementState
		{
			Fly = 1,
			Falling = 2
		}
	}
}
