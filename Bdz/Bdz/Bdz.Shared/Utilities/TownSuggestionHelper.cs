namespace Bdz.Utilities
{
    using Bdz.LocalDB;
    using System.Collections.Generic;

    public class TownSuggestionHelper
    {
        private LocalDBManager manager;
        public TownSuggestionHelper()
        {
            manager = LocalDBManager.Instance;
            this.SetTowns();
        }

        public List<Town> Towns { get;private set; }

        private async void SetTowns()
        {
            this.Towns = await manager.RetrieveTownTable();
        }
    }
}
