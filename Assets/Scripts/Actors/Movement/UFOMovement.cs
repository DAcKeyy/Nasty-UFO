using System;
using UnityEngine;
using UnityEngine.Events;

namespace Actors.Movement
{
	[Serializable]
	[RequireComponent(typeof(Rigidbody))]
	public class UFOMovement : MonoBehaviour
	{
		public float SpeedFixedUpdate => _speedFixedUpdate;
		public float SpeedMultiplier { get; set; } = 1;
		
		[SerializeField] private MovementState _currentState;
		[SerializeField] private float _flyForce;
		[SerializeField] private float _speedFixedUpdate;
		[SerializeField] private UnityEvent _thrustersStart;
		[SerializeField] private UnityEvent _thrustersStop;
		
		private Rigidbody _rigidbody;
		private bool _isAccelerate;
		

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			ChangeState(_currentState);
		}
		
        private void FixedUpdate()
        {
            MoveToward(new Vector2(_speedFixedUpdate * SpeedMultiplier, _rigidbody.velocity.y));
            
            if(_isAccelerate) _rigidbody.AddForce(_flyForce * Vector3.up, ForceMode.Acceleration);
        }

        public void StartAcceleration()
        {
	        _thrustersStart.Invoke();
	        _isAccelerate = true;
        }

        public void StopAcceleration()
        {
	        _thrustersStop.Invoke();
	        _isAccelerate = false;
        }
        
        
        public void ChangeState(MovementState state)
        {
	        if (_currentState == state) return;
	        
	        switch (state)
	        {
		        case MovementState.Fly:
			        _currentState = MovementState.Fly;
			        _rigidbody.useGravity = false;
			        break;
		        case MovementState.Falling:
			        _currentState = MovementState.Falling;
			        _rigidbody.useGravity = true;
			        break;
		        default:
			        throw new ArgumentOutOfRangeException(nameof(state), state, null);
	        }
        }
        
        private void MoveToward(Vector2 direction)
        {
	        _rigidbody.velocity = direction;
        }

        [Serializable]
		public enum MovementState
		{
			Fly = 1,
			Falling = 2
		}
	}
}
