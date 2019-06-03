
namespace Elinkx.FileStorage.Contracts
{
    public class InsertVersionRequest
    {
        public int FileId { get; set; }
        public bool Signed { get; set; }
        public string UserCode { get; set; }
        public byte[] Content { get; set; }
        public bool UpdateLastVersion { get; set; } = false;
       // reseni: implementovat novou metodu v DL a nasledne se stejnym requestem rozhodnout v SL co bude. vracet stejny Result

    }
}
