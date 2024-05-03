using System.Collections.Generic;


namespace ComputerClub.Model
{
    public static class Logger
    {
        private static List<string> _history = new List<string>();

        public static void Add(string message) 
        {
            _history.Add(message);
        }

        public static void Clear()
        {
            _history.Clear();
        }
    }
}
