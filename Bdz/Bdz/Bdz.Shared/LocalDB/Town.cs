namespace Bdz.LocalDB
{
    using SQLite;

    [Table("Towns")]
   public class Town
    {
        public Town(string name)
        {
            this.Name = name;
        }

        public Town() { }

        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
