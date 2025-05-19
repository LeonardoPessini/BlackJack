using BlackJack.Console;
using BlackJack.Domain;

var game = new BlackJackGame();

bool jogarNovamente;

do
{
    try
    {
        game.StartGame();

        jogarNovamente = Janela.DesejaJogarNovamente();
    }catch(ExitException)
    {
        jogarNovamente = false;
    }
    
}while(jogarNovamente);