namespace Potter.Domain.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string HogwartsHouse { get; set; }
        public string InterpretedBy { get; set; }
        public string Birthdate { get; set; }
        public string Image { get; set; }
    }
}
