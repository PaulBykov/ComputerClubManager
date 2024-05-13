using System;
using ComputerClub.Services;
using System.Collections.Generic;


namespace ComputerClub.Model
{
    public static class Logger
    {
        private static readonly LinkedList<string> _history = new ();

        public static LinkedList<string> Messages => _history;

        public static void Add(string message) 
        {
            _history.AddLast($"[{DateTime.Now}]  {AuthService.GetInstance().CurrentUser.Fullname}: {message}");
        }

        public static void Clear()
        {
            _history.Clear();
        }
    }
}
