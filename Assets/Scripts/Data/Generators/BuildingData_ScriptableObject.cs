using Actors.NastyUFO.Buildings;
using UnityEngine;

namespace Data.Generators
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Building Data", order = 1)]
	public class BuildingData_ScriptableObject : ScriptableObject
	{
		public BuildingData _buildingData;
	}
}