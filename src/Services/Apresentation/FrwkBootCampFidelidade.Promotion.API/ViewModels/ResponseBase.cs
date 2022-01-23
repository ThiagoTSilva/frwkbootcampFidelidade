namespace FrwkBootCampFidelidade.Promotion.API.ViewModels
{
    public class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public dynamic Object { get; set; }
    }
}
