# Drag and Drop Mission System Setup Guide

## Overview
This system allows players to drag recruited adventurers and drop them into mission slots (1-3 slots per mission). The system includes:

- **Adventurer.cs**: Handles adventurer behavior and drag/drop mechanics (UI version - uses TextMeshProUGUI)
- **AdventurerWorldSpace.cs**: Alternative version for 3D world space UI (uses TextMeshPro)
- **Mission.cs**: Manages mission slots and adventurer assignments  
- **MissionUI.cs**: Provides UI for mission information and controls (uses TextMeshProUGUI)
- **MissionSlot.cs**: (Optional) Individual slot management with visual feedback
- **DragDropManager.cs**: (Optional) Centralized drag/drop management

## Quick Setup

### 1. Adventurer Setup

**For UI Canvas (Recommended):**
- Use `Adventurer.cs` script 
- Set up TextMeshProUGUI components for name, level, and experience display
- Ensure the adventurer GameObject is a child of a Canvas

**For 3D World Space:**
- Use `AdventurerWorldSpace.cs` script instead
- Set up TextMeshPro (3D) components for name, level, and experience display
- Position text elements in 3D space around the adventurer

**Common Setup:**
- Add `Adventurer.cs` (or `AdventurerWorldSpace.cs`) script to adventurer GameObjects
- Ensure adventurer has a `Collider` component for mouse detection
- Create a child GameObject named "HireButton" with a BoxCollider

### 2. Mission Setup
- Add `Mission.cs` script to mission GameObjects
- Configure mission properties in the inspector:
  - `missionName`: Display name
  - `adventurersAllowed`: Number of slots (1-3)
  - `missionLevel`, `missionDuration`, `rewardAmount`, `successRate`: Mission parameters
- Create empty GameObjects as children for slot positions
- Assign these slot position transforms to the `slotPositions` array in the inspector

### 3. Mission UI Setup (Optional)
- Add `MissionUI.cs` script to UI GameObjects
- Connect UI elements:
  - `missionNameText`: Displays mission name
  - `missionInfoText`: Shows mission details
  - `assignedAdventurersText`: Shows current assignments
  - `startMissionButton`: Button to start mission
  - `completeMissionButton`: Button to complete mission

## How It Works

### Adventurer Recruitment
1. Click the "HireButton" on an adventurer to recruit them
2. Once recruited, the hire button disappears
3. Recruited adventurers can be dragged to missions

### Drag and Drop Process
1. Click and hold a recruited adventurer to start dragging
2. Drag the adventurer near a mission (within 5 units)
3. Release to drop the adventurer into an available mission slot
4. If the drop fails, the adventurer returns to their original position

### Mission Management
- Missions track assigned adventurers and available slots
- Missions can only be started when at least one adventurer is assigned
- Once started, adventurers cannot be reassigned until mission completion
- Completing a mission returns all adventurers to idle status

## Customization

### Adjusting Drop Distance
In `Adventurer.cs`, modify the distance check:
```csharp
if (distance < 5.0f) // Change this value to adjust drop sensitivity
```

### Adding Visual Feedback
Use the `MissionSlot.cs` script for individual slot visual feedback:
- Assign highlight materials for hover effects
- Assign occupied materials to show filled slots
- Set up trigger colliders for better drop detection

### Mission Parameters
Customize mission behavior by modifying:
- `adventurersAllowed`: Maximum number of adventurers per mission
- `missionDuration`: How long missions take to complete
- `successRate`: Probability of mission success
- `rewardAmount`: Rewards given upon completion

## Debugging
- Check Unity Console for drag/drop status messages
- Ensure adventurers have proper colliders for mouse detection
- Verify mission slot positions are properly assigned
- Confirm UI elements are properly connected in the inspector

## Future Enhancements
- Add visual slot highlighting during drag operations
- Implement mission difficulty and adventurer level matching
- Add drag preview or ghost images
- Create save/load functionality for mission states
- Add sound effects for drag/drop actions
