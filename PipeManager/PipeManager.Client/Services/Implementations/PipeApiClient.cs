using System.Net.Http.Json;
using PipeManager.Client.Services.Abstractions;
using PipeManager.Client.Contracts.Models;
using PipeManager.Client.Contracts;
using System.Text.Json;

namespace PipeManager.Client.Services.Implementations
{
    public class PipeApiClient : IPipeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _json = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public PipeApiClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<ApiResult<List<PipeItemDto>>> GetPipesAsync()
        {
            const string requestUri = "Pipes/GetPipes";

            using var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var success = await response.Content.ReadFromJsonAsync<PipeItemsResponse>(_json);
                return ApiResult<List<PipeItemDto>>.Ok(success?.Items ?? new List<PipeItemDto>());
            }

            ClientErrorResponse error;

            // 1. Проверяем, есть ли JSON в ответе
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content
                                     .ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else // 2. Любой другой Content-Type - читаем как строку
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }

            return ApiResult<List<PipeItemDto>>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<bool>> DeletePipeAsync(int pipeId)
        {
            using var response = await _httpClient.DeleteAsync($"Pipes/DeletePipe?pipeId={pipeId}");

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Ok(true);

            ClientErrorResponse error;

            // 1.  Проверяем, есть ли JSON в ответе
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content
                                      .ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else // 2.  Любой другой Content-Type — читаем как строку
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }

            return ApiResult<bool>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<List<SteelGradeItemDto>>> GetSteelGradesAsync()
        {
            const string requestUri = "Pipes/GetSteelGrades";

            using var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var success = await response.Content.ReadFromJsonAsync<SteelGradeItemResponse>(_json);
                return ApiResult<List<SteelGradeItemDto>>.Ok(success?.Items ?? new List<SteelGradeItemDto>());
            }

            ClientErrorResponse error;

            // 1. Проверяем, есть ли JSON в ответе
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content
                                     .ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else // 2. Любой другой Content-Type - читаем как строку
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }

            return ApiResult<List<SteelGradeItemDto>>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<List<PackageItemDto>>> GetPackagesAsync()
        {
            const string requestUri = "Pipes/GetPackages";

            using var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var success = await response.Content.ReadFromJsonAsync<PackagesItemResponse>(_json);
                return ApiResult<List<PackageItemDto>>.Ok(success?.Items ?? new List<PackageItemDto>());
            }

            ClientErrorResponse error;

            // 1. Проверяем, есть ли JSON в ответе
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content
                                     .ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else // 2. Любой другой Content-Type - читаем как строку
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }

            return ApiResult<List<PackageItemDto>>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<bool>> DeletePackageAsync(int packageId)
        {
            using var response = await _httpClient.DeleteAsync($"Pipes/DeletePackage?packageId={packageId}");

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Ok(true);

            ClientErrorResponse error;

            // 1.  Проверяем, есть ли JSON в ответе
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content
                                      .ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else // 2.  Любой другой Content-Type — читаем как строку
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }

            return ApiResult<bool>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<bool>> AddPipeAsync(AddPipeItemDto pipeToAdd)
        {
            const string requestUri = "Pipes/AddPipe";

            using var response = await _httpClient.PostAsJsonAsync(requestUri, pipeToAdd, _json);

            if (response.IsSuccessStatusCode)
            {
                return ApiResult<bool>.Ok(true);
            }

            ClientErrorResponse error;
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content.ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }
            return ApiResult<bool>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<bool>> AddPackageAsync(AddPackageItemDto packageToAdd)
        {
            const string requestUri = "Pipes/AddPackage";

            using var response = await _httpClient.PostAsJsonAsync(requestUri, packageToAdd, _json);

            if (response.IsSuccessStatusCode)
            {
                return ApiResult<bool>.Ok(true);
            }

            ClientErrorResponse error;
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content.ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }
            return ApiResult<bool>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<bool>> UpdatePipeAsync(PipeItemDto pipeToUpdate)
        {
            const string requestUri = "Pipes/UpdatePipe";

            using var response = await _httpClient.PostAsJsonAsync(requestUri, pipeToUpdate, _json);

            if (response.IsSuccessStatusCode)
            {
                return ApiResult<bool>.Ok(true);
            }

            ClientErrorResponse error;
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content.ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }
            return ApiResult<bool>.Fail(error, response.StatusCode);
        }

        public async Task<ApiResult<bool>> UpdatePackageAsync(PackageItemDto packageToUpdate)
        {
            const string requestUri = "Pipes/UpdatePackage";

            using var response = await _httpClient.PostAsJsonAsync(requestUri, packageToUpdate, _json);

            if (response.IsSuccessStatusCode)
            {
                return ApiResult<bool>.Ok(true);
            }

            ClientErrorResponse error;
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                error = await response.Content.ReadFromJsonAsync<ClientErrorResponse>(_json)
                        ?? new() { Success = false };
            }
            else
            {
                var text = await response.Content.ReadAsStringAsync();
                error = new ClientErrorResponse
                {
                    Success = false,
                    ResponseType = "Error",
                    Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {text}"
                };
            }
            return ApiResult<bool>.Fail(error, response.StatusCode);
        }
    }
}
