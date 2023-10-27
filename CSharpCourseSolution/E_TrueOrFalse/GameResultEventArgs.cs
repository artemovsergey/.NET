using System;

namespace G_TrueOrFalse
{
    public class GameResultEventArgs : EventArgs
    {
        public GameResultEventArgs(int questionsPassed, int mistakesMade, bool isWin)
        {
            QuestionsPassed = questionsPassed;
            MistakesMade = mistakesMade;
            IsWin = isWin;
        }

        public int QuestionsPassed { get; }
        public int MistakesMade { get; }
        public bool IsWin { get; }
    }
}
