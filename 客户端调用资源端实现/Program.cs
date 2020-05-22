using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace 客户端调用资源端实现
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TestIdentity4Async();
            }
            catch (Exception e)
            {
                Console.WriteLine("异常：" + e.Message);
            }
            Console.ReadKey();
        }

        private static async void TestIdentity4Async()
        {
            //获取到获取token的url
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:3065");
            if (disco.IsError)
            {
                Console.WriteLine("获取到获取token的url失败：" + disco.Error);
                return;
            }
            Console.WriteLine("获取token的url为：" + disco.TokenEndpoint);
            Console.WriteLine();

            //获取token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,//就是我们postman请求token的地址
                ClientId = "Client",//客户端
                ClientSecret = "secret",//秘钥
                Scope = "api1"//请求的api
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine("获取token失败：" + tokenResponse.Error);
                return;
            }

            //模拟客户端调用需要身份认证的api
            var apiClient = new HttpClient();
            //赋值token（携带token访问）
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            //发起请求
            var response = await apiClient.GetAsync("http://localhost:10469/Home/Get");
            if (response.IsSuccessStatusCode)
            {
                //请求成功
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("请求成功，返回结果是：" + content);
            }
            else
            {
                //请求失败
                Console.WriteLine($"请求失败，状态码为：{(int)response.StatusCode}，描述：{response.StatusCode.ToString()}");
            }
        }
    }
}
