using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DrogenTimer
{
    static class Program
    {
        static shadowAPI2.Chat chat;
        static System.Timers.Timer refreshTimer;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

            shadowAPI2.API.Init();
            chat = shadowAPI2.Chat.GetInstance();
            chat.OnChatMessage += chat_OnChatMessage;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void chat_OnChatMessage(DateTime time, string message)
        {
            if (message.Contains("Du hast 2500 Samen eingepflanzt. In 15 Minuten kannst du sie ernten."))
            {
                chat.AddMessage("{00FF00}DrogenTimer gestartet. Du wirst in 15 Minuten benachrichtigt!");

                refreshTimer = new System.Timers.Timer();
                refreshTimer.Elapsed += new ElapsedEventHandler(Timer);
                refreshTimer.Interval = 900000;
                refreshTimer.Enabled = true;
            }
        }

        private static void Timer(object sender, ElapsedEventArgs e)
        {
            chat.AddMessage("{00FF00}Deine Drogen, die du gepflanzt hast, sind nun fertig!");
            refreshTimer.Enabled = false;
        }

    }
}
