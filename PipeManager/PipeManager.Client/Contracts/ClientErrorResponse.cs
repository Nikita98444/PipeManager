using System.Text.Json.Serialization;

namespace PipeManager.Client.Contracts
{
    public class ClientErrorResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } // Должно быть false

        [JsonPropertyName("responseType")]
        public string? ResponseType { get; set; } // Должно быть "Error"

        [JsonPropertyName("message")]
        public string? Message { get; set; } // Сообщение об ошибке

        [JsonPropertyName("data")]
        public Dictionary<string, object>? Data { get; set; } // Доп. данные
    }
}
