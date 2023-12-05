# ARcadeGuardians
This project takes place in a class I attend at my HCI Master, Mixed Reality &amp; Tangible Interactions. The aim is to create a Tower Defense game where map &amp; ennemy would be virtual, and tower &amp; spells would be represented by tangible objects with marker's attached to it.

## Marker List
For this project to work, we had to design a bunch of different markers by ourselves, here they are :
- Level 1 -> 8cm (longer than wider), using green tones and full lines
- Archer Tower -> 6cm (squared), using blue tones and squares
- Bomb Tower -> 6cm (squared), using red tones and crossed circles
- Knight Tower -> 6cm (squared), using grey tones and triangles
- Range Upgrade -> 5cm (longer than wider), using pink tones and hexagones
- Damage Upgrade -> 5cm (longer than wider), using purple tones and freeforms

## Markers creation
In order to properly create the markers, we went on this website made by vuforia, and followed the instructions.
https://developer.vuforia.com/library/objects/best-practices-designing-and-developing-image-based-targets

## Interesting Features
Beside basic Vuforia markers detection, and the simple action of displaying a virtual object that interacts with other virtual objects based on one tangible, here are some features we found cool to add :
- Upgrades Combination -> User can clip multipls single upgrades together in order to create a new marker that represents an improved upgrade
- Spells that could be thrown on the battlefield -> Dedicated markers that triggers a spell when touching the board's surface.