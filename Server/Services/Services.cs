using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Server.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public GenericService(HttpClient httpClient, string apiUrl)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
        }

        public async Task Create(T entity)
        {
            await _httpClient.PostAsJsonAsync(_apiUrl, entity);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
        }

        public async Task<List<T>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(_apiUrl);
        }

        public async Task<T> GetById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{_apiUrl}/{id}");
        }

        public async Task Update(T entity)
        {
            await _httpClient.PutAsJsonAsync(_apiUrl, entity);
        }
    }
}
