Guessing Game Project
This is the Guessing Game project. The goal of the game is to guess the number chosen by the program. You have 8 attempts. After each attempt, the program will provide you with small hints. For more details about the rules and functionality, you can explore the game itself.

To download the game, run the following command in your terminal at the desired location on your PC:
git clone https://github.com/neworldid/GuessingGame.git  

Next, locate the .env file in the  /guessing-game-ui/  directory and enter your provided Google Client ID after:
VITE_REACT_APP_GOOGLE_CLIENT_ID=  

Then, navigate to the main application directory (GuessingGameApp) and run in terminal the following command:
docker-compose up -d  

Afterward, you'll need to apply database migrations. In the same directory, execute the following command:
dotnet ef database update -p GuessingGameApiApplication/GuessingGame.DataAccess -s GuessingGameApiApplication/GuessingGame.Application  
(Note: Make sure database tools are installed.)

Now you can enjoy the game and challenge yourself! If you encounter any technical issues, feel free to contact the author.
