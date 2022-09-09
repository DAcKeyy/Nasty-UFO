using System;
using Actors.Base;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[SelectionBase] [ExecuteInEditMode]
	[RequireComponent(typeof(Rigidbody))]
	public class ModularBuilding : MonoBehaviour
	{
		public BuildingData BuildingData => _buildingData;
		
		[SerializeField] private BuildingData _buildingData;

		private Vector3 _roofCenterPosition;

		public void Init(BuildingData data)
		{
			_buildingData = data;
		}	

		public void AssembleBuilding(ushort requiredFloors)
		{
			if(_buildingData._groundFloorElement == null) throw new Exception("Нету GroundFloorElement в префабе");
			if(_buildingData._middleFloorElement == null) throw new Exception("Нету MiddleFloorElement в префабе");
			if(_buildingData._roofFloorElement == null) throw new Exception("Нету RoofFloorElement в префабе");
			if (requiredFloors <= 0) throw new Exception("Строить дом без этажей не прикольно");
			
			BuildingFloor buildingFloorComponent = null;
			Vector3 buildPosition = this.transform.position;
			Quaternion buildRotation = this.transform.rotation;
			Vector3 newModulePosition = buildPosition;
			Bounds bounds = new Bounds();
			
			//TODO Как это разбить на потоки?
			for (ushort floorIterator = 1; floorIterator <= requiredFloors; floorIterator++)
			{
				switch (floorIterator) {
					case 1:
						buildingFloorComponent = Instantiate(_buildingData._groundFloorElement, 
							buildPosition, buildRotation, this.transform);
						bounds.center += new Vector3(_buildingData._groundFloorElement.RenderBounds.center.x, 0,
							_buildingData._groundFloorElement.RenderBounds.center.z);
						break;
					
					case var i when (floorIterator < requiredFloors):
						newModulePosition.y = bounds.size.y;
						buildingFloorComponent = Instantiate(_buildingData._middleFloorElement, 
							newModulePosition, buildRotation, this.transform);
						bounds.center += new Vector3(_buildingData._middleFloorElement.RenderBounds.center.x, 0,
							_buildingData._middleFloorElement.RenderBounds.center.z);
						break;
					
					case var i when (floorIterator == requiredFloors):
						newModulePosition.y = bounds.size.y;
						buildingFloorComponent = Instantiate(_buildingData._roofFloorElement, 
							newModulePosition, buildRotation, this.transform);
						bounds.center += new Vector3(_buildingData._roofFloorElement.RenderBounds.center.x, 0,
							_buildingData._roofFloorElement.RenderBounds.center.z);
					
						_roofCenterPosition = new Vector3(
							buildPosition.x,
							buildPosition.y + buildingFloorComponent.RenderBounds.size.y * 2, //* 2 чтобы точка надвисала над крышей
							buildPosition.z);
						break;
				}

				//buildingFloorComponent.SetParent(this);
				bounds.center += new Vector3(0, buildingFloorComponent.RenderBounds.size.y, 0);
				bounds.Encapsulate(buildingFloorComponent.RenderBounds);

#if UNITY_EDITOR	
				DestroyImmediate(buildingFloorComponent);
				if(Application.isPlaying)
#endif
					Destroy(buildingFloorComponent);

			}
		}
		
		public Vector3 GetRoofPosition()
		{
			return _roofCenterPosition;
		}	

		public Vector3 GetWorldGroundedConnectionPoint(Side side)
		{
			Vector3 worldGroundedColliderPosition = transform.TransformPoint(
				_buildingData.GetSumOfBounds().center.x,
				0, 
				_buildingData.GetSumOfBounds().center.z);

			return side switch
			{
				Side.Front => worldGroundedColliderPosition + (transform.rotation * Vector3.forward) * _buildingData.GetSumOfBounds().size.z / 2,
				Side.Right => worldGroundedColliderPosition + (transform.rotation * Vector3.right) * _buildingData.GetSumOfBounds().size.x / 2,
				Side.Back => worldGroundedColliderPosition + (transform.rotation * -Vector3.forward) * _buildingData.GetSumOfBounds().size.z / 2,
				Side.Left => worldGroundedColliderPosition + (transform.rotation * -Vector3.right) * _buildingData.GetSumOfBounds().size.x / 2,
				_ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
			};
		}

		public void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.TryGetComponent<ICrushable>(out var obstacle))
			{
				obstacle.Crush(col);
			}
		}

		/*
		private void OnDrawGizmos()
		{
			//рисует конект поинты
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(GetWorldGroundedConnectionPoint(Side.Front), 0.1f);
			Gizmos.DrawSphere(GetWorldGroundedConnectionPoint(Side.Back), 0.1f);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(GetWorldGroundedConnectionPoint(Side.Left), 0.1f);
			Gizmos.DrawSphere(GetWorldGroundedConnectionPoint(Side.Right), 0.1f);
			//
		}	
		*/
		public enum Side
		{
			Front,
			Right,
			Back,
			Left
		}
	}
}
