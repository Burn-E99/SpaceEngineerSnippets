namespace Sandbox.ModAPI.Ingame
{
	public struct MyShipMass
	{
		/// <summary>
		/// Gets the base mass of the ship.
		/// </summary>
		public readonly float BaseMass;

		/// <summary>
		/// Gets the total mass of the ship, including cargo.
		/// </summary>
		public readonly float TotalMass;

		/// <summary>
		/// Gets the physical mass of the ship, which accounts for inventory multiplier.
		/// </summary>
		public readonly float PhysicalMass;

		public MyShipMass(float mass, float totalMass, float physicalMass)
		{
			this.BaseMass = mass;
			this.TotalMass = totalMass;
			this.PhysicalMass = physicalMass;
		}
	}
}
