namespace Newsletter.Data.SearchRequests {
    public class SearchRequestBase {
        public string? SearchTerm { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 50;
        public string? OrderBy { get; set; }
        public bool Descending { get; set; } = false;
    }
}
