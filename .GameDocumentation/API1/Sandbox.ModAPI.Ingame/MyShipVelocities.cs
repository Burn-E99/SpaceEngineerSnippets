using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public struct MyShipVelocities
	{
		/// <summary>
		/// Gets the ship's linear velocity (motion).
		/// </summary>
		public readonly Vector3D LinearVelocity;

		/// <summary>
		/// Gets the ship's angular velocity (rotation).
		/// </summary>
		public readonly Vector3D AngularVelocity;

		public MyShipVelocities(Vector3D linearVelocity, Vector3D angularVelocity)
		{
			this = default(MyShipVelocities);
			this.LinearVelocity = linearVelocity;
			this.AngularVelocity = angularVelocity;
		}
	}
}
