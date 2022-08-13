using System.Text;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using static System.GC;

namespace Mango.Web.Services;

public class BaseService : IBaseService
{
    public ResponseDto responseModel { get; set; }
    private readonly IHttpClientFactory _httpClientFactory;

    protected BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        responseModel = new ResponseDto();
    }

    public async Task<T> SendAsync<T>(ApiRequest request)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("MangoAPI");
            var httpRequestMessage = new HttpRequestMessage(request.ApiType, request.Url);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Clear();
            if (request.Data != null)
            {
                httpRequestMessage.Content =
                    new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
            }

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<T>(apiResponse);
            return apiResponseDto;
        }
        catch (Exception e)
        {
            var responseDto = new ResponseDto
            {
                isSuccess = false,
                DisplayMessage = "Error",
                ErrorMessage = new List<string> {Convert.ToString(e.Message)}
            };
            var serializeObject = JsonConvert.SerializeObject(responseDto);
            var deserializeObject = JsonConvert.DeserializeObject<T>(serializeObject);
            return deserializeObject;
        }
    }

    public void Dispose()
    {
        SuppressFinalize(true);
    }
}