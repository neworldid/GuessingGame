## Guessing Game Project
The goal of the game is to guess the number chosen by the program. You have 8 attempts. After each attempt, the program will provide you with small hints. For more details about the rules and functionality, you can explore the game itself.

1. To download the game, run the following command in your terminal at the desired location on your PC:
**git clone https://github.com/neworldid/GuessingGame.git**

2. Next, locate and open in any text editor the .env file in the  /guessing-game-ui/  directory and enter your provided Google Client ID after:
VITE_REACT_APP_GOOGLE_CLIENT_ID=  

3. Then, navigate to the main application directory (GuessingGame) and run in terminal the following command:
**docker-compose up -d**

4. Afterward, you'll need to apply database migrations: In the same directory, execute the following command:
**dotnet ef database update -p GuessingGameApiApplication/GuessingGame.Infrastructure -s GuessingGameApiApplication/GuessingGame.API**
(Note: Make sure database tools are installed.)

Now you can enjoy the game and challenge yourself! If you encounter any technical issues, feel free to contact me.
