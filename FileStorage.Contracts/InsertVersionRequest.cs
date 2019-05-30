
namespace Elinkx.FileStorage.Contracts
{
    public class InsertVersionRequest
    {
        public int FileId { get; set; }
        public bool Signed { get; set; }
        public string UserCode { get; set; }
        public byte[] Content { get; set; }
       // public bool UpdateLastVersion { get; set; }

    }
}
