namespace G_TrueOrFalse
{
    public class Question
    {
        public Question(string text, bool correctAnswer, string explanation)
        {
            Text = text;
            CorrectAnswer = correctAnswer;
            Explanation = explanation;
        }

        public string Text { get; }
        public bool CorrectAnswer { get; }
        public string Explanation { get; }
    }
}
