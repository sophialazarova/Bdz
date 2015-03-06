namespace Bdz.LocalDB
{
    using SQLite;

    [Table("Towns")]
   public class Town
    {

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
