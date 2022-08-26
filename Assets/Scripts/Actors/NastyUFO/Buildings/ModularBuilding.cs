using System;
using Actors.Base;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[SelectionBase]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
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
			if(_buildingData.GroundFloorElement == null) throw new Exception("Объект не проинициализирован");
			//я не подразумиваю строить цокольные этажи, угомонись
			if (requiredFloors <= 0) throw new Exception("Сторить дом без этажей не прикольно");
			
			var newBuildingColliderSize = Vector3.zero;
			var newBuildingColliderCenter = Vector3.zero;
			Bounds moduleRenderBounds;
			BuildingFloor buildingFloorComponent = null;
			
			//TODO Как это разбить на потоки?
			
			for (ushort floorIterator = 1; floorIterator <= requiredFloors; floorIterator++)
			{
				if (floorIterator == 1) {
					MakeFirstFloor();
				}
				else if (floorIterator < requiredFloors) {
					MakeMiddleFloor();
				}
				if (floorIterator == requiredFloors) {
					MakeRoof();
				}

				ClearBuildModule();
			}

			var coll = GetComponent<BoxCollider>();
			coll.enabled = false;
			coll.size = newBuildingColliderSize;
			coll.center = newBuildingColliderCenter;
			
			#region Logic
				void MakeFirstFloor()
	            {
		            buildingFloorComponent = Instantiate(
			            _buildingData.GroundFloorElement, 
			            this.transform.position,
			            this.transform.rotation,
			            this.transform);

                    moduleRenderBounds = buildingFloorComponent.GetComponent<Renderer>().bounds;

                    var localBoundsCenter = moduleRenderBounds.center - buildingFloorComponent.transform.position;
                    
            		newBuildingColliderSize = moduleRenderBounds.size;
                       newBuildingColliderCenter = localBoundsCenter;
	            }
				
	            void MakeMiddleFloor()
	            {
            		buildingFloorComponent = Instantiate(
	                    _buildingData.MiddleFloorElement, 
	                    this.transform.position, 
	                    this.transform.rotation, 
	                    this.transform);
                    
            		moduleRenderBounds = buildingFloorComponent.GetComponent<Renderer>().bounds;
                    
                    var boundsLocalCenter = moduleRenderBounds.center - buildingFloorComponent.transform.position;

            		buildingFloorComponent.transform.position = new Vector3(
	                    this.transform.position.x,
            			this.transform.position.y + newBuildingColliderSize.y,
	                    this.transform.position.z);

            		newBuildingColliderSize += new Vector3(0, moduleRenderBounds.size.y, 0);
            		newBuildingColliderCenter += new Vector3(0, boundsLocalCenter.y, 0);
	            }
	            
	            void MakeRoof()
	            {
            		buildingFloorComponent = Instantiate(
	                    _buildingData.RoofFloorElement, 
	                    this.transform.position, 
	                    this.transform.rotation, 
	                    this.transform);
                    
            		moduleRenderBounds = buildingFloorComponent.GetComponent<Renderer>().bounds;
                    var boundsLocalCenter = moduleRenderBounds.center - buildingFloorComponent.transform.position;
                    
            		buildingFloorComponent.transform.position = new Vector3(
	                    this.transform.position.x, 
            			this.transform.position.y + newBuildingColliderSize.y,
	                    this.transform.position.z);
                    
                    newBuildingColliderSize += new Vector3(0, moduleRenderBounds.size.y, 0);
            		newBuildingColliderCenter += new Vector3(0, boundsLocalCenter.y, 0);
                    
                    _roofCenterPosition = new Vector3(
	                    this.transform.position.x,
	                    this.transform.position.y + newBuildingColliderSize.y + moduleRenderBounds.size.y * 2,
	                    this.transform.position.z);
	            }

	            void ClearBuildModule() //убираем лишнее с модуля чтобы осталась только текстура
				{
#if UNITY_EDITOR	
					DestroyImmediate(buildingFloorComponent);
					if(Application.isPlaying)
#endif
						Destroy(buildingFloorComponent);
				}
			#endregion
		}
		
		public Vector3 GetRoofPosition()
		{
			return _roofCenterPosition;
		}

		public Bounds GetBuildingSize()
		{
			return GetComponent<Collider>().bounds;
		}
		
		private void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.TryGetComponent<ICrushable>(out var obstacle))
			{
				obstacle.Crush();
			}
		}
	}
}
