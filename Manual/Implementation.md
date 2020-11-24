# Painting Tanks - Implementation
## Mechanics
### Painting
#### Implementation
- Using Surface Shader as new ShaderGraph solution don't make it reasonable
- Using **Graphics.Blit()** to paint onto map
- Using **Texture2D.ReadPixels32()** to count pixels.
- Written custom simple library to count part of the mesh texture and speed up production 
#### Optimization
- Using more objects, but having textures being smaller
  - Suggested 512 x 512 max, as Graphics.Blit have issues with bigger textures
  - *Color32* instead of *Color* when ReadPixels32
- Texture checking 
  - partially and perform final check after match / game is ended
  - ulong instead of int as we don't need negative values.

#### Issues
- Paint applied on edge of the mesh cannot be applied into another one just with one **Graphics.Blit()** so I have to write custom solution which will create more issues. So as it's better suited to create multiple meshes and perform more **Graphics.Blit()** than one with big texture (higher 2048 x 2048px) - but still can be problematic task.

## Gameplay
### Vehicle Movement
#### Implementation
- Using Rigidbody for Tank
- Uses new Input system package
- Fixed Update for Movement Changes
- Controller Motives
  - Tank 
  - *to implement* Vehicle

### Gun System
- Using Rigidbody for Gun Turret and Turret
- // trajectory implementation

### Perk System
- 


