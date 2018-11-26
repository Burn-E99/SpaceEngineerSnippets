using System;
using VRage.Game;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public struct MyDetectedEntityInfo
	{
		/// <summary>
		/// The entity's EntityId
		/// </summary>
		public readonly long EntityId;

		/// <summary>
		/// The entity's display name if it is friendly, or a generic descriptor if it is not
		/// </summary>
		public readonly string Name;

		/// <summary>
		/// Enum describing the type of entity
		/// </summary>
		public readonly MyDetectedEntityType Type;

		/// <summary>
		/// Position where the raycast hit the entity. (can be null if the sensor didn't use a raycast)
		/// </summary>
		public readonly Vector3D? HitPosition;

		/// <summary>
		/// The entity's absolute orientation at the time it was detected
		/// </summary>
		public readonly MatrixD Orientation;

		/// <summary>
		/// The entity's absolute velocity at the time it was detected
		/// </summary>
		public readonly Vector3 Velocity;

		/// <summary>
		/// Relationship between the entity and the owner of the sensor
		/// </summary>
		public readonly MyRelationsBetweenPlayerAndBlock Relationship;

		/// <summary>
		/// The entity's world-aligned bounding box
		/// </summary>
		public readonly BoundingBoxD BoundingBox;

		/// <summary>
		/// Time when the entity was detected. This field counts milliseconds, compensated for simspeed
		/// </summary>
		public readonly long TimeStamp;

		/// <summary>
		/// The entity's position (center of the Bounding Box)
		/// </summary>
		public Vector3D Position
		{
			get
			{
				return this.BoundingBox.Center;
			}
		}

		public MyDetectedEntityInfo(long entityId, string name, MyDetectedEntityType type, Vector3D? hitPosition, MatrixD orientation, Vector3 velocity, MyRelationsBetweenPlayerAndBlock relationship, BoundingBoxD boundingBox, long timeStamp)
		{
			if (timeStamp <= 0)
			{
				throw new ArgumentException("Invalid Timestamp", "timeStamp");
			}
			this.EntityId = entityId;
			this.Name = name;
			this.Type = type;
			this.HitPosition = hitPosition;
			this.Orientation = orientation;
			this.Velocity = velocity;
			this.Relationship = relationship;
			this.BoundingBox = boundingBox;
			this.TimeStamp = timeStamp;
		}

		/// <summary>
		/// Determines if this structure is empty; meaning it does not contain any meaningful data
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty()
		{
			return this.TimeStamp == 0;
		}
	}
}
