# Frogger-Game

#Project title

This project is just a reskin of Frogger-Game 1981. 

# Project Flow 

* Game Start with a countdown from 5. 
* Once the game starts the player is able to move the character in all four directions and the obstacles from both the game areas start to spawn within a given time interval and     move across the screen.
* Using the Arrow key player is able to control the character.
* Users need to reach the home without getting killed.
* The player will have 3 life each time the player dies lives get reduced by 1
* If lives get to 0 the game over. The player will again start the game from scratch.
* If a player reaches all the 5 homes then the player wins the game.


# Flow Diagram
![](image_2021-04-14_132216.png)

# Project Structure

  1) GameManager Class :- This Class is used to control the over all state of the game.
      # CheckGameOver()  This Function is used to check whether the game is over or not and reduce lives
      # SpawnHome()      This function used spawn the home if player reached using the argument
      # CheckWin()       This function is used to check the win condition
      4) CountDown()      This function is used for Time CountDown
      5) GameStart()      This Function used to Start the game by countdown
      6) RestartGame()    Use to restart the Game
      
  2) Player Control:-     The purpos of this PlayerControl Class is to control the player movement,animation and the collision detection between different obstacle.

