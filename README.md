# ARcadeGuardians
This project takes place in a class I attend at my HCI Master, Mixed Reality &amp; Tangible Interactions. The aim is to create a Tower Defense game where map &amp; ennemy would be virtual, and tower &amp; spells would be represented by tangible objects with marker's attached to it.

## Markers
  As this project takes place in vuforia, we decided to use Image Targets, recognized by the program as markers, here are some precisions about these
  #List
    - One Level Marker, that represents the level the user is gonna play on
    - Two Tower Markers, that each allow the user to select the type of tower he wanna place on the map, plus he can use the marker position to modify its position.
      * Archer
      * Bomber 
    - Two Upgrade Markers, each allowing to upgrade some stats on related tower. These markers also serve the user when he wanna cast a spell on the map, by selecting the Spell option on the offered choice.
      * Fire Upgrade (for bomber)
      * Arrow Upgrade (for archer)
  #Design
    For the design, we've followed Vuforia design tips, and used a graphic tablet to draw the markers by ourselves, making them visually coherent with what they serve to.
  #Tangible
    In order to ease the manipulation, we've put the markers on solid basis, the level's one being higher than other for abritrary reasons

## Game Features
  This Project implements the following features
    - Tower placement 
    - Tower Upgrading
    - Wave Launching
    - Level Choice + positionning
    - UI selections
    - Spells Casting on the map
    - Recognition of closest item on which user can place the related item.
    
## UI
  The UI mainly serves as an information holder, but we've decided to include as many clarifications and helps inside of it, in order to make the user experience easier and as interactive as possible.
  
## Specificity
  Contrary to many AR games involving tangibles, we've decided to not use a tangible board, so the playable map is defined by the level marker, which makes the level design a bit easier, and the whole game more carryable.
