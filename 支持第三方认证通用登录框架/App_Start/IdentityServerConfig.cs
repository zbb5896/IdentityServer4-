using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 支持第三方认证通用登录框架.App_Start
{
    public class IdentityServerConfig
    {

        /// <summary>
        /// 允许访问哪些Api（就像我允许我家里的哪些东西可以让顾客访问使用，如桌子，椅子等等）   CreateDate：2019-12-26 14:08:29；Author：Ling_bug
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("api1", "Lingbug Api1"),
                new ApiResource("api2", "Lingbug Api2")
            };
        }


        /// <summary>
        /// 允许哪些客户端访问（就像我要求顾客必须具备哪些条件才可以拿到进入我家的钥匙）   CreateDate：2019-12-26 14:09:51；Author：Ling_bug
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    //对应请求参数的client_id（假设身高）
                    ClientId = "Client",
                    //对应请求参数的grant_type（GrantTypes.ClientCredentials是client_credentials）（假设体重）
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //对应请求参数的client_secret（假设口令）
                    ClientSecrets =
                    {
                        new Secret(Encrypt("secret")),
                    },
                    //对应请求参数的Scope，这里写的类似于一个id，对应的是上面的ApiResource的key
                    /*
                     * （这里假设为是VIP卡片，一个资源一张卡片，每个卡片对应的我上面的资源的key，
                     * 所以，
                     * 1.哪怕你有卡片，但是我这个资源不开放或者没有对应的资源，你也是访问不到的，即401
                     * 2.哪怕我有资源，你前面的身高体重都符合，口令也正确，但是你没有VIP卡片，你也是无法访问的）
                     */
                    AllowedScopes = {"api1", "api2"}
                }
            };
        }


        /// <summary>
        /// 加密   CreateDate：2019-12-26 11:19:04；Author：Ling_bug
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        private static string Encrypt(string valueString)
        {
            return string.IsNullOrWhiteSpace(valueString) ? null : valueString.Sha256();
        }

    }
}
