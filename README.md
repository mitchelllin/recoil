# recoil
Working Recoil System for 3D Unity FPS

Weapon Recoil is implemented quite differently depending on the FPS game in question:
How recoil may differ depending on the game:
- How fast recoil ramps up 
- How fast recoil settles
- If recoil settles automatically/based on what variables
- Where the recoil settles back to 

The recoil in this Unity project has been implemented in the following way:
- Recoil velocity is immediately set to a positive number upon gunshot
- Recoil acceleration is immediately set to negative so that the recoil ramps up at a decreasing velocity
- The amount of muzzle climb is aggregated/saved.
- When the fire button is let go of:
  - If the player had moved their mouse downwards, the aggregate recoil will be set to zero. there will be no recoil settling
  - If the player has not moved their mouse downwards, the recoil will slowly settle to the original point + any mouse movement
- Weapon Recoil works in a similar way to Apex Legends
