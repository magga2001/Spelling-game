public class PlayerAnswerData
{
    private SpellingGames spellingGames;
    private string correctAnswer;
    private string playerAnswer;

    public PlayerAnswerData(){}

    public PlayerAnswerData(SpellingGames spellingGames,string correctAnswer, string playerAnswer)
    {
        this.spellingGames = spellingGames;
        this.correctAnswer = correctAnswer;
        this.playerAnswer = playerAnswer;
    }

    public SpellingGames SpellingGames => spellingGames;
    public string CorrectAnswer => correctAnswer;
    public string PlayerAnswer => playerAnswer;
}
