namespace PassStorage2.Models
{
    /// <summary>
    /// Counters container
    /// </summary>
    public class Counters
    {
        /// <summary>
        /// All passwords counter
        /// </summary>
        public int All { get; set; }
        /// <summary>
        /// Most used passwords counter
        /// </summary>
        public int MostUsed { get; set; }
        /// <summary>
        /// Expired passwords counter
        /// </summary>
        public int Expired { get; set; }
        /// <summary>
        /// Counters needs refresh ?
        /// </summary>
        public bool NeedRefresh => All == 0 && MostUsed == 0 && Expired == 0;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="all">All passwords counter</param>
        /// <param name="mostUsed">Most used counter</param>
        /// <param name="expired">Expired counter</param>
        public Counters (int all, int mostUsed, int expired)
        {
            All = all;
            MostUsed = mostUsed;
            Expired = expired;
        }
    }
}
