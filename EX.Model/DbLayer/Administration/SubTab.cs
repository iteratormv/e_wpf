namespace EX.Model.DbLayer
{
    public class SubTab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public int TabId { get; set; }
        public Tab Tab { get; set; }
    }
}