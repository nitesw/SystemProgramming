using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_04_Semaphore_02
{
    [Serializable]
    public class PlayerStats
    {
        public string PlayerName { get; set; }
        public int StartBalance { get; set; }
        public int FinalBalance { get; set; }

        public PlayerStats(string playerName, int startBalance, int finalBalance)
        {
            PlayerName = playerName;
            StartBalance = startBalance;
            FinalBalance = finalBalance;
        }
    }

}
