New Changes by MacharaStormwing:
--------------------------------

Update 0.2.6 (Oktober 8 2023):
- updated dependency to BepInEx 5.4.22.0
- updated to support Valheim 0.217.24 (no actual code change, just updated the BepInEx dependency)

Update 0.2.5 (August 26 2023):
- updated to support Valheim 0.217.14 "Hildir's Request"
- added optional improved logging for Taming skill (set "Enable Detailed Taming Logging = true" in MoreSkills.TamingConfig.cfg if you need it)
- refactoring of taming skill to track down issues (in progress, taming is still not properly working)
- added missing YggdrasilWood and Softtissue to config WoodCuttingApplyForItems configurable in MoreSkills.OverhaulsConfig.cfg (so skill is applied to them)
- Included fitness skill of pipakin found at https://github.com/pipakin/PipakinsMods since it was not updated (but I adapted and updated it)
  you can disable it by setting FitnessSkillEnabled=false in com.pipakin.FitnessSkillMod.cfg
- updated default values to something I find more reasonable and/or balanced

Update 0.2.4 (June 22 2023):
- small fix always logging a warning concerning IgnoreDropItemNames even when it should not be

Update 0.2.3 (June 22 2023):
- Added config fields to "MoreSkills.OverhaulsConfig.cfg:
  * IgnoreDropItemNames: List of known item drops (through WoodCutting, Pickaxes or other Misc means) that should not get a skill applied. This also avoids the warning of unknown drops (unknown drops do not get the skill applied ither).
  * DontDropItemNames: List of known item drops (through WoodCutting, Pickaxes or other Misc means) that should not be dropped at all.

Update 0.2.2 (June 20 2023):
- Changed vesioning from 4 to 3 digits to conform with Thunderstore
- implemented the use of configurable fields WoodCuttingApplyForItems, PickaxeApplyForItems and HuntingApplyForItems in Moreskills.OverhaulsConfig.cfg
- added advanced logging for handling of drops which are configurable through config "Enable Detailed Overhauls Logging". Should remain false unless needed.

Update 0.2.1 (June 16 2023):
- update to support Valheim 0.216.9
- various cleanups (logging more unified, refactoring)

Update 0.2.0:
- Updated for new Valheim version 0.215.2
- added handling of Woodcutting drop types: "BirchCone", "OakSeeds" and "Acorn"
- various build system updates
- made list of drop types configurable per skill so it's easier to update and more configurable
  (see WoodCuttingApplyForItems, PickaxeApplyForItems and HuntingApplyForItems in Moreskills.OverhaulsConfig.cfg)
  Those are not yet usable though (future update will use them)

Old changes by guiriguy:
------------------------

Update 0.1.10 NEW Fishing Skill:
- Added New Fishing Skill.
- Added Fish Variety, the bigger the stronger and the bigger the drops. (Optional) (Drops Configurable with Multiplier)
- Added Fish BaseHookChance to be bounded to Fishing Skill. (Optional)
- Added Fish Stamina Drain Decrease bounded to Fishing SKill. (Optional) 
- Added Fishing Stamina Drain Decrease bounded to Fishing Skill, at level 50 this number will be 0, higher than that upto 100 you gain Stamina as "rest". (Optional)
- Fixed Some Typos in Configs Decriptions and missing strings.
- Added again the Trophies and Blob config and check up to Hunting Skill. (Optional) 
- Added a TamerID ZDO to taming creatures. So it will be bounded to the closest one when it eats for the first time.
- Added a UnPatch and Patch to each skill individually.
- Added a Fishing AutoHook Chance, up to 50% at level 100 of Fishing Skill. (Optional) (Configurable)
- Added Custom Names to Tames. To Change the Name you Need to be Crouching. (Optional)
- Added See Stats from Tames. (BETA and Useless for now)

Hotfix 0.1.9.8:
- Added double check to damage that it doesn't come from a UserID, fire, drowning, fall damage and so on.

Hotfix 0.1.9.7:
- Fixed when ever you had RollOnFall deactivated it would affect all damages recieved or given.

Update 0.1.9.6:
- Fix the Crash when you die having a few skills disabled.
- Added MaxFallAltitude, higher than this will multiply the amout of meters exceeded as damage.
- Changed the FallDamageDecrese to start based on the MaxRollAltitude. Up to 30m.
- Now the MaxRollAltitude is attached to Jump Skill Level
- Added a Configurable MaxFallAltitude Altitude in (Meters).
- Added a Configurable MaxFallAltitudeIncreaseDamageMultiplier per Meter Exceeded, By Default 2. It Need to be higher than 0.
- Added a Configurable Min and Max Roll Altitude if you are between these 2 values you will roll and not recieve Damage at all.
- Changed the Visual Text when about to tame a Creature to Needed Levels, Novice, Adept and Master Tamer. With a timer of the more or less time it will take to tame at that moment.
- Added a Trials of Odin Levels Compatibility so the drops with Hunting Skill will stick to the Stars Shown by ToO. This need to be Configured at HuntingConfig, with the same values from ToO.
- Another tiny fixed and numbers at Hunting Config as you are a Master Tamer with Everything by the HoverText.
- Some more things I can't remember at the moment.

Hotfix 0.1.9.5:
- Fixed Game Crash when Taming all Levels under Master.
- Fixed Null Log report when not close enought to the tame.
- Added Message on the top left corner for: When you are not the tamer. When you get far away from the tame, and when you get close again.	

Hotfix 0.1.9.4: 
- Fixed jump counting infinity. Sorry for that one...
- Fixed where again I'm speacial leaving unintended debugs on

Hotfix 0.1.9.3: 
- Fix of spam error in log at taming any animal in Unlock or lower levels.
- Fix where you would roll no matter the distance.
- Added 0 fall damage if you roll. Will later be revamped into a more inmersive progression. Trying to make a stable launch of 0.1.9
- Double Check the config as there was a typo in the boar's level

Hotfix 0.1.9.2: Fixed Missing Stone and fixed the drops when either of the factors wheren't activated.


MoreSkills 0.1.9 NEW!

________________________________________

New Jump Overhaul:

1- Now you can jump higher if you press Shift (Optional and custom if wanted).

a. Can change the base jump force, and the max when level 100 is reached.
b. Can change the Custom Keybind to activate the higher jump or deactivate this key.
c. Can disable Higher Jump if you don't want it.
d. Have a Decreased fall damage, with custom divider, reached at level 100.
e. Added a roll if you fall from heights from 4 to 8 meters(?). In the future this roll will have decreased fall damage and stamina with Vitality and Strength levels respectly.


2- Revamped Crafting, Hunting and Taming.

a. Now Simple Recycling is attached to your Crafting level, so the higher the cost the higher the recycling, but if you made something that cost 100 wood and then when you are level 100 it will read the cost then and give you back the selected amount (default 50%).
b. Now it will get and save all new creatures and items. In case of hunting it will get what it drops and do the math.
c. Now the drops take in count the creature's level and multiply it.
d. Now Taming will add al custom Creatures, and if not in the list it will add Default Taming Levels, Unlock: 30, Tamer: 60, Master: 90.


3- Fixed Bugs

a. Fixed bug in WoodCutting where FineWood would not drop.
b. Fixed bug in Taming that when looking to a boar it would send a log error.
c. Fixed the Wheel Scroll Killer Config.
d. Other minor fixes. 


________________________________________________________

(For any advice, tutorial, help, whatever I am more than glad to listen and chat with whomever just contact me at discord: Jordan Connor#6458)


The idea of this mod was made basically by making it more bareble with Higher Difficulty mods such as Trials of Odin﻿ or Unidarkshin's Overhaul﻿.
As I stated before please be patient. This is my ever attempt to ever make a mod at all and even with the 0 knowledge I had with the language of C#.
This mod is made posible by the Skill Injector by Pipakin.﻿
Some ideas that I have I quickly gave up on due to the existence of beautifully made mods such as:


Gathering Skill by Pipakin
﻿﻿﻿﻿Fitness Skill by Pipakin
﻿Cartography by Advize
Cooking Skill by thegreyham


Mod not Compatible with:


- Valheim Plus
(WAS being looked at with them)﻿

- Skillful﻿﻿
(Added the crouch speed as "fix")﻿


﻿What this mod includes and what is yet to come:


(All of them mostly configurable and can be individually enabled or disabled)

Includes:


New Skills:

____________________________________________________________________________________


Strength: 

Increases Max Carry Weight
﻿﻿
﻿﻿You can increase this skill by making some exercise.

﻿﻿
Spoiler:  Show

Vitality:

Increases your Health
﻿﻿
﻿﻿
You can increase this skill by being a true viking, endure the pain or never see Valhalla.

﻿﻿
Spoiler:  Show



Sailing:

Increase a few aspects in Sailing

﻿﻿
As the Captain of the boat you will need your fellow crewmates, or not. But everyone will learn with the speed of the wind! The more Crewmates the faster you row!


﻿﻿
Spoiler:  Show

(BETA) Crafting 0.2 NEW!
﻿Decrese or Increase the cost of crafting.


You can choose the middle point, if wanted:
Now you say the amount to be multiplied at the begging and the amount to be divided at the end with a configurable middle point.
You can also select this middle point to be either 0 or 100 if you dont want multiplier or divider.

Multiplier: Makes the objects cost more at the begging (Level 0), if the Middle Level is 0 then it jumps it.

Divider: Makes the objects cost less at the end (Level 100), if the Middle Level is 100 then it jumps it.

All these number are Configurable.


Many bugs will show up for sure, even incompatibilities. I have tried with Epic Loots﻿ and Simple Recycling﻿ and both work wonderful.
(Bronze is not affected as it reads it twice and becomes an infinite number, no idea how to fix it, tried many things these 3 days tho... haha)



Hunting Skill
Increase the amount of drops and the chance of them

This skill changes all the drops and chances of all mobs or/and bosses if activated.
You gain the Max max amount Drops cap set at level 50
You gain the Max min amount Drop cap set al level 100
You gain 100% Chance of Drop if activated


Spoiler:  Show

Taming Skill
Decrease the amount of needed time to tame a creature

1- If Activated get the ability to Unlock the Tame at certain level.

a. Can change eat mob needed level per each ability (Unlock, Tamer, Master)
b. Each ability gives you Reduced tame time, if activated.
c. If you are lower than Unlock's level you will take the default time to tame per missing level.
d. If you are Equal or Higher than Unlock's level you will get the default time to tame it and each level up will decrease it.
e. If you are Equal or Highet than Tamer's level you will get the half of the default time to tame and each level up will decrease it.
f. If you are Equal or Higher than Master's level... Well you are a Master, you will only need 2 min to tame the creature.


2- If Unlock Mod not activated.

a. You will take the default time to tame it and each level will decrease until reached Level 100 which it will be 2 minutes.


3- Compatible with AllTamable ﻿by buzz if Activated in config

a. You can do either to all Custom, vanilla, added creatures to be tamed.


Spoiler:  Show

OVERHAULS:

_________________________________________________________________________


Sneak:

Increase your Crouch Speed
﻿﻿
Uses Vanilla Sneak Skill to get higher Crouch Speed. (As a "fix" to incompability with Skillful Mod)


Swim:

Increase your Swim Speed
﻿﻿
Uses Vanilla Swim Skill to get higher Swim Speed.


Mining:

Increase the max and min drop

﻿Uses Vanilla Pickaxe Skill to increase the drops.


Stamina:

Gain stamina while encumbred and not moving, based on the amount of extra weight carried and strength skill
Gain stamina while swimming and not moving, based on the swim skill

﻿Uses vanilla swim skill and custom strength skill to calculate the amount of stamina to give to the player. The higher the skills the higher this number is, multipliers in the config.

Wood Cutting

Increased drops of all drops from trees.

Uses vanilla WoodCutting Skill to give increased drops. 

__________________________________________________________________________________________________________________________________________________
﻿
Yet to come (if posible):

Strength:

 Increase the max stacks
 Better Wagon movement

﻿﻿﻿(I yet have to find how the h*** get this to work, without loosing the stack or making it update such stacks acordingly your skill level. And to not interfere with your friends containers inventory, etc... If anyone got and idea for this and wants to share it contact me :D)

Vitality:

Add Base Resistance to all damage.

Crafting:

Speed of crafting
The base stats would be doubled as skill increases. 
If possible, make it work with Epic Loots so you have more chance of more buff or higher chance of better buffs (Separated from the original for people who want to add this or not)

Building:

Cost of building reduced with the skill.

Hunting/Fishing (If possible):

Tracking senses Perk