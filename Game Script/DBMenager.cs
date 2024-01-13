using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBMenager 
{
    public static string username;
    public static int score;

    public static bool Login { get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }
}
