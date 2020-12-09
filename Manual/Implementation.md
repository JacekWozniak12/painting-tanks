# Painting Tanks - Implementation
## Mechanics
### Painting
Describes process of changing color of mesh. It's important to have mesh that doesn't have UV with parts of mesh overlapping on one position, as well as set mesh to Read/Write operations. 
#### Implementation
- I will be using **TEXCOORD2** and **UV2** and:
  - Base texture is set in **UV0**.
  - Lightmap texture should be set in **UV1**.
- Using Surface Shader as new ShaderGraph solution don't make it reasonable.
- I had written custom shader Paint_BlendShader_Surface in SurfaceShader manner - so I have light / shadow system implementation provided by Unity.
- Using **Graphics.Blit()** to paint into *Render Texture*, which is taken by shader and displayed.
- Using **Texture2D.ReadPixels()** to count pixels.
- Written custom simple library to count part of the mesh texture and speed up production.
#### Optimization
- Using more objects, but having textures being smaller:
  - Suggested 512 x 512 max, as Graphics.Blit have issues with bigger textures.
  - *Color32* instead of *Color* when **Texture2D.ReadPixels()**.
- Texture checking: 
  - Check partially and perform final check after match / game is ended.
  - *ulong* instead of int as we don't need negative values...

#### Issues
- Paint applied on edge of the mesh cannot be applied into another one just with one **Graphics.Blit()** so I have to write custom solution which will create more issues. So as it's better suited to create multiple meshes and perform more **Graphics.Blit()** than one with big texture (higher 2048 x 2048px) - but still can be problematic task.

### Vehicle Movement
#### Implementation
- Using Rigidbody for Tank
- Uses new Input system package
- **FixedUpdate()** for Movement Changes
- Movement Agents defined for Body, Turret and Gun
- Vehicle Movement Styles (wip)
  - Tank - is moved by its base controls and its turret is moved by pointer (mouse)
  - Assault Gun - is moved by its base controls and rotated by pointer. Turret rotation is limited
  - Artillery - is moved and rotated by its controls, not pointer. It is base moslty for artillery vehicles / functions

##### Optimization
- **FixedUpdate()** instead of **Update()**;

### Weapon System
#### Implementation
- Using *Rigidbody* for Gun Turret and Turret.
- Using *Camera* with mouse pointer and *Physics.Raycast* with some vector functions to implement minimal / maximal range system that player can target to.
- Using multiple vehicle movement styles to create different experiences for different type of weapons. Example: Hotchkiss H39 'ground stuka' tank which turret rotation does not influence rocket barrage of it as launcher is not pinned to its turret.



