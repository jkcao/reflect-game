# Reflect Game 

Camera movement using `Arrows Right/Left`.

Camera Zooimg: (a) Zoom out: `Up Arrow` (b) Zoom In: `Down Arrow`.

## Key changes from level-design branch

1. Camera Panning Fix - after showing a level, when you press `wasd` keys, camera snaps back to player.
2. Add reference to "final_position" and "initial_position" for cameras.
3. Slightly move the 'Endpt' platform.

### Level Movement

Set Camera's final position to an object (currently a platform) where camera will move towards, then pressing the `wasd` (movement) keys snaps camera to player.

