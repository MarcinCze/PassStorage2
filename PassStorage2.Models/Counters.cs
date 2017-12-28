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
        /// <param name="a">All passwords counter</param>
        /// <param name="m">Most used counter</param>
        /// <param name="e">Expired counter</param>
        public Counters (int a, int m, int e)
        {
            All = a;
            MostUsed = m;
            Expired = e;
        }
    }
}
