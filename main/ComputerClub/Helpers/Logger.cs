using ComputerClub.Services;
using Microsoft.VisualBasic;
using System.Collections.Generic;


namespace ComputerClub.Model
{
    public static class Logger
    {
        private static LinkedList<string> _history = new LinkedList<string>();

        public static void Add(string message) 
        {
            _history.AddLast($"[{DateAndTime.DateString} {DateAndTime.TimeString}]  {AuthService.GetInstance().CurrentUser.Fullname}: {message}");
        }

        public static void Clear()
        {
            _history.Clear();
        }
    }
}
