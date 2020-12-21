using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Models;
using ABCSchool.Uwp.Interfaces;
using Newtonsoft.Json;

namespace ABCSchool.Uwp.Services
{
    public class StudentService : IStudentService<Student>
    {
        private const string ServiceUri = @"https://localhost:44318/api/Student";

        public async Task<List<Student>> GetAllAsync(string accessToken = null, bool forceRefresh = false)
        {
            List<Student> result = default;

            try
            {
                using (var handler = new HttpClientHandler { AllowAutoRedirect = false })
                using (HttpClient client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(ServiceUri);
                    var json = await response.Content.ReadAsStringAsync();
                    result = await Task.Run(() => JsonConvert.DeserializeObject<List<Student>>(json));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }

        public async Task<Student> GetByIdAsync(int id, string accessToken = null, bool forceRefresh = false)
        {
            Student result = default;

            try
            {
                using (var handler = new HttpClientHandler { AllowAutoRedirect = false })
                using (HttpClient client = new HttpClient(handler))
                {
                    var json = await client.GetStringAsync($@"{ServiceUri}/{id}");
                    result = await Task.Run(() => JsonConvert.DeserializeObject<Student>(json));


                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public async Task<bool> PostAsync(Student item)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (item == null)
                    {
                        return false;
                    }

                    var serializedItem = JsonConvert.SerializeObject(item);
                    var buffer = Encoding.UTF8.GetBytes(serializedItem);
                    var byteContent = new ByteArrayContent(buffer);

                    var response = await client.PostAsync(ServiceUri, byteContent);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Student> PostAsJsonAsync(Student item)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (item == null)
                    {
                        return null;
                    }

                    var serializedItem = JsonConvert.SerializeObject(item);

                    var response = await client.PostAsync(ServiceUri,
                        new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();

                        Student student = JsonConvert.DeserializeObject<Student>(result);
                        return student;
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode);
                        return null;
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> PutAsync(Student item)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (item == null)
                    {
                        return false;
                    }

                    var serializedItem = JsonConvert.SerializeObject(item);
                    var buffer = Encoding.UTF8.GetBytes(serializedItem);
                    var byteContent = new ByteArrayContent(buffer);

                    var response = await client.PutAsync(ServiceUri, byteContent);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> PutAsJsonAsync(Student item)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (item == null)
                    {
                        return false;
                    }

                    var serializedItem = JsonConvert.SerializeObject(item);

                    var response = await client.PutAsync($@"{ServiceUri}/{item.Id}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int studentModelId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($@"{ServiceUri}/{studentModelId}");

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Add this to all public methods
        public void AddAuthorizationHeader(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Authorization = null;
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

