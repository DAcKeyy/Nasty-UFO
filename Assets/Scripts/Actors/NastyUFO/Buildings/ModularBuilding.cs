using System;
using UnityEngine;

namespace Scenes.Actors.NastyUFO.Buildings
{
	public class ModularBuilding : MonoBehaviour
	{
		[SerializeField] private BuildingFloor _groundFloorElement;
		[SerializeField] private BuildingFloor _middleFloorElement;
		[SerializeField] private BuildingFloor _roofFloorElement;
		[SerializeField] private Vector3 _roofCenterPosition;
		
		public void AssembleBuilding(ushort requiredFloors)
		{
			Debug.Log(transform.rotation.eulerAngles);
			
			if (requiredFloors == 0) throw new Exception("Сторить дом без этажей не прикольно");
			
			var newBuildingColliderSize = Vector3.zero;
			var newBuildingColliderCenter = Vector3.zero;
			Bounds moduleRenderBounds;
			BuildingFloor buildingFloorComponent = null;
			
			//TODO Как это разбить на потоки?
			
			for (ushort floorIterator = 1; floorIterator <= requiredFloors; floorIterator++)
			{
				if(floorIterator == 1) {
					MakeFirstFloor();
				}
				else if (floorIterator < requiredFloors) {
					MakeMiddleFloor();
				}
				else if(floorIterator == requiredFloors) {
					MakeRoof();
				}

				if (requiredFloors == 1) { //Если только один этаж нужен то пусть будет крыша
					MakeRoof();
				}
				
				Debug.Log(buildingFloorComponent.transform.rotation.eulerAngles);
				
				ClearBuildModule();
			}

			SetNewBulidingColliderValues(newBuildingColliderSize, newBuildingColliderCenter);
			
			#region Logic
				void MakeFirstFloor()
	            {
            		buildingFloorComponent = Instantiate(_groundFloorElement, this.transform.position, transform.rotation, transform);
                    moduleRenderBounds = buildingFloorComponent.GetComponent<Renderer>().bounds;

                    var localBoundsCenter = moduleRenderBounds.center - buildingFloorComponent.transform.position;
                    
            		newBuildingColliderSize = moduleRenderBounds.size;
            		newBuildingColliderCenter = localBoundsCenter;
	            }
				
	            void MakeMiddleFloor()
	            {
            		buildingFloorComponent = Instantiate(_middleFloorElement, this.transform.position, transform.rotation, transform);
            		moduleRenderBounds = buildingFloorComponent.GetComponent<Renderer>().bounds;
                    var boundsLocalCenter = moduleRenderBounds.center - buildingFloorComponent.transform.position;

            		buildingFloorComponent.transform.position = new Vector3(
            			transform.position.x,
            			newBuildingColliderSize.y + transform.position.y,
            			transform.position.z);

            		newBuildingColliderSize += new Vector3(0, moduleRenderBounds.size.y, 0);
            		newBuildingColliderCenter += new Vector3(0, boundsLocalCenter.y, 0);
	            }
	            
	            void MakeRoof()
	            {
            		buildingFloorComponent = Instantiate(_roofFloorElement, this.transform.position, transform.rotation, transform);
            		moduleRenderBounds = buildingFloorComponent.GetComponent<Renderer>().bounds;
                    var boundsLocalCenter = moduleRenderBounds.center - buildingFloorComponent.transform.position;
                    
            		buildingFloorComponent.transform.position = new Vector3(
            			transform.position.x, 
            			newBuildingColliderSize.y  + transform.position.y,
            			transform.position.z);
                    
                    newBuildingColliderSize += new Vector3(0, moduleRenderBounds.size.y, 0);
            		newBuildingColliderCenter += new Vector3(0, boundsLocalCenter.y, 0);
                    
                    _roofCenterPosition = new Vector3(
	                    transform.position.x,
	                    transform.position.y + newBuildingColliderSize.y + moduleRenderBounds.size.y * 2,
	                    transform.position.z);
	            }

	            void SetNewBulidingColliderValues(Vector3 newBuildingColliderSize, Vector3 newBuildingColliderCenter)
				{
					if (gameObject.TryGetComponent<BoxCollider>(out var colliderComp) == false)
						colliderComp = gameObject.AddComponent<BoxCollider>();
					
					colliderComp.center = newBuildingColliderCenter;
					colliderComp.size = newBuildingColliderSize;
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
			if (_roofCenterPosition == null) 
				throw new ArgumentNullException($"Дом непроиницализирован");
			
			return _roofCenterPosition;
		}
		
		private void OnCollisionEnter(Collision col)
		{
			//TODO Подумать как это скейлить проеврки на коллизии
			if (col.gameObject.GetComponent<UFO>() != null)
			{
				//TODO Magic number
				//_signalBus.TryFire(new BuildingTouchedSignal(col));
			}
		}
	}
}
