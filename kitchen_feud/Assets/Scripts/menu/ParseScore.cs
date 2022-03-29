
public class ParseScore
{
    private static int score1 = 0;
    private static int score2 = 0;

    public void UpdateScores(int newScore1, int newScore2)
    {
        score1 = newScore1;
        score2 = newScore2;
    }

    public void AddScore1(int newScore1)
    {
        if ((score1 + newScore1) >= 0)
            score1 += newScore1;
    }

    public void AddScore2(int newScore2)
    {
        if ((score2 + newScore2) >= 0)
            score2 += newScore2;
    }

    public int GetScore1()
    {
        return score1;
    }

    public int GetScore2()
    {
        return score2;
    }
}
