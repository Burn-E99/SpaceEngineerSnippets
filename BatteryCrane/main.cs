/* This program assumes the crane is set up as such:
 * -----
 * |A|B|
 * -----
 * |P|S|
 * -----
 * Where A and B are battery bays, P is the safe idle parking location for the crane, and S is the location where ships can pick batteries up.
 */

// Piston group for moving forward/back (movements: P<->A, S<->B)
string _lowerPistonGroup = "BC Lower Pistons";
// Piston group for moving left/right (movements: P<->S, A<->B)
string _middlePistonGroup = "BC Middle Pistons";
// Piston group for moving up/down
string _upperPistonGroup = "BC Upper Pistons";
// Magnetic Plate group used for holding battery to crane
string _magPlateGroup = "BC Mag Plates";
// Merge Block group for Battery Bay A
string _bayAMergeBlockGroup = "BC Bay A Merge Blocks";
// Merge Block group for Battery Bay B
string _bayBMergeBlockGroup = "BC Bay B Merge Blocks";
// LCD Panel (designed for a 1x1 panel) for showing current status
string _statusPanelName = "BC Status LCD";
// Light Block group for status lights
string _statusLightGroup = "BC Status Lights";
// Hinge for floor door
string _doorHingeName = "BC Door Hinge";

// Max velocities for each piston group, must be positive values
float _maxLowerPistonVelocity = 0.5f;
float _maxMiddlePistonVelocity = 0.5f;
float _maxUpperPistonVelocity = 0.5f;

public Program() {
  // Runtime.UpdateFrequency = UpdateFrequency.Update100;
}

// Utility functions for piston groups
List<IMyPistonBase> getPistonsFromGroup(IMyBlockGroup group) {
  List<IMyPistonBase> pistons = new List<IMyPistonBase>();
  group.GetBlocksOfType<IMyPistonBase>(pistons);
  return pistons;
}
void applyPistonDistanceLimit(IMyBlockGroup group, float minDist, float maxDist) {
  List<IMyPistonBase> pistons = getPistonsFromGroup(group);
  foreach(IMyPistonBase piston in pistons) {
    piston.MinLimit = minDist;
    piston.MaxLimit = maxDist;
  }
}
void applyPistonVelocity(IMyBlockGroup group, float velocity) {
  List<IMyPistonBase> pistons = getPistonsFromGroup(group);
  foreach(IMyPistonBase piston in pistons) {
    piston.Velocity = velocity;
  }
}
void applyPistonEnabled(IMyBlockGroup group, bool enabled) {
  List<IMyPistonBase> pistons = getPistonsFromGroup(group);
  foreach(IMyPistonBase piston in pistons) {
    piston.Enabled = enabled;
  }
}
bool getPistonsStopped(IMyBlockGroup group) {
  List<IMyPistonBase> pistons = getPistonsFromGroup(group);
  bool allStopped = true;
  foreach(IMyPistonBase piston in pistons) {
    if(piston.Status == PistonStatus.Stopped) {
      allStopped = false;
    }
  }
  return allStopped;
}

// Utility functions for reading block states
bool getMagPlatesLocked(IMyBlockGroup group) {
  List<IMyLandingGear> plates = new List<IMyLandingGear>();
  group.GetBlocksOfType<IMyLandingGear>(plates);
  bool locked = false;
  foreach(IMyLandingGear plate in plates) {
    if(plate.IsLocked) {
      locked = true;
    }
  }
  return locked;
}
bool getMergeBlocksLocked(IMyBlockGroup group) {
  List<IMyShipMergeBlock> mergeBlocks = new List<IMyShipMergeBlock>();
  group.GetBlocksOfType<IMyShipMergeBlock>(mergeBlocks);
  bool locked = false;
  foreach(IMyShipMergeBlock mergeBlock in mergeBlocks) {
    if(mergeBlock.IsConnected) {
      locked = true;
    }
  }
  return locked;
}

public void Main() {
  IMyBlockGroup lowerPistons = GridTerminalSystem.GetBlockGroupWithName(_lowerPistonGroup);
  IMyBlockGroup middlePistons = GridTerminalSystem.GetBlockGroupWithName(_middlePistonGroup);
  IMyBlockGroup upperPistons = GridTerminalSystem.GetBlockGroupWithName(_upperPistonGroup);
  IMyBlockGroup magPlates = GridTerminalSystem.GetBlockGroupWithName(_magPlateGroup);
  IMyBlockGroup bayAMergeBlocks = GridTerminalSystem.GetBlockGroupWithName(_bayAMergeBlockGroup);
  IMyBlockGroup bayBMergeBlocks = GridTerminalSystem.GetBlockGroupWithName(_bayBMergeBlockGroup);
  IMyTextPanel statusPanel = GridTerminalSystem.GetBlockWithName(_statusPanelName) as IMyTextPanel;
  IMyBlockGroup statusLights = GridTerminalSystem.GetBlockGroupWithName(_statusLightGroup);
  IMyMotorAdvancedRotor doorHinge = GridTerminalSystem.GetBlockWithName(_doorHingeName) as IMyMotorAdvancedRotor;
}