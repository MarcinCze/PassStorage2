namespace PassStorage2.Models
{
    public class PasswordTag
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Reference to Tag
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// Reference to Password
        /// </summary>
        public int PasswordId { get; set; }
    }
}
