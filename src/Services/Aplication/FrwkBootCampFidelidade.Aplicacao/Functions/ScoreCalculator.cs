namespace FrwkBootCampFidelidade.Aplicacao.Functions
{
    public static class ScoreCalculator
    {
        public static float CalculateScoreByValue(float value)
        {
            return value * 100;
        }

        public static float CalculateValueByScore(float score)
        {
            return score / 100;
        }
    }
}
