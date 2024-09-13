using Newsletter.Entities;

namespace Newsletter.Data {
    public class Response<T> {
        public T? Result { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsSuccess => this.Errors.Count == 0;
        public void AddError(string message) {
            this.Errors.Add(message);
        }
    }
}
