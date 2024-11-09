# Weaver - Red Bricks Game Jam
Highlights of the project:
- A robust Dialogue System that allows branching and player options
- A turn based battle system that takes in account character stats such as speed defense and speed

## Dialogue System
- **DialogueData**: A custom serializable class that encompasses CharacterEmotions, their name, options to be given to the player after the end of this particular dialogue and an event that can be called after end end of this dialogue

  ![image](https://github.com/user-attachments/assets/1269171a-57bf-4600-9775-68d89ee2243e)

- **Option Data**: Custom serializable class that has dialogueLine and the next Dialogue index. this dialogue index can be used to crate a branching narrative

  ![image](https://github.com/user-attachments/assets/bcbe36c9-c4ce-4286-81f6-c50e1a64a64f)

- **Conversation Trigger**: this denotes an entire sequence of dialogue to be shown on one conversation. When player walks in the trigger range of a npc or an object they get shown a prompt when triggers a conversation

  ![image](https://github.com/user-attachments/assets/bb54d4f5-f342-48f3-830a-4b2573c791ed)

- **Dialogue Manager**: The main controller for all dialogue. Only one in the scene. Acts as the communicator between conversationtriggers and the conversation ui.

- **DialogueUI**: To be called from the dialogue manager. Shows the dialogue on the screen

  ![image](https://github.com/user-attachments/assets/61e373aa-7d18-49d8-a471-838c29f6f747)
  ![image](https://github.com/user-attachments/assets/0d6ee80f-3064-415d-93d4-4a535c1dbf27)


## Turn Based System
- **CharacterStats**: A serializable class that contains base stats of each battle chracter. This is a present on player and all battlable NPCs

  ![image](https://github.com/user-attachments/assets/ab3eaf1f-3235-48fd-96a7-e1231dd1f216)

- **Moves Scriptable Object**: A move is defined by an Scriptable Object. Implementation is done for 2 types of moves: Damaging Moves and Stats Reducing Moves. We have one MoveSO script. DamageMove and StatusMove are derivatives of MoveSO

  ![image](https://github.com/user-attachments/assets/ef7e0a6e-a5e9-4afe-9137-1d0e93ac6669)
  ![image](https://github.com/user-attachments/assets/c52d8dea-7748-44f4-8341-9413a35b9806)

- **Turn Battle Manager**: This objects controls the entire flow of battle including calculating the turn of a particular sequence based on the combatants speed. It calles the BattleUI to show the battle on screen

- **Battle UI**: Controls the BattleUI GameObject in Canvas

  ![Screenshot 2024-11-09 225346](https://github.com/user-attachments/assets/06dabcc7-961c-474c-a7b8-a3ff4be3ad4f)
  ![Screenshot 2024-11-09 225354](https://github.com/user-attachments/assets/d34598ff-8b96-42b0-b826-8fb8cb122425)
  ![Screenshot 2024-11-09 225403](https://github.com/user-attachments/assets/25d50c61-98ff-470a-aa1d-bfd7620e2995)


