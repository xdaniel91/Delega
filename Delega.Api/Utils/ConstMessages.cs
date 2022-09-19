using Delega.Api.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Delega.Api.Utils
{
    public class ConstMessages : IConsMessages
    {
        private readonly IDistributedCache DistributedCache;

        public ConstMessages(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }

        public void SetMessages(string language)
        {
            var key = "errorMessages";
            var languageLower = language.ToLower();

            Dictionary<string, string> messages;

            switch (languageLower)
            {
                case "pt-br":
                    messages = ConsMessagesSysId.MessagesPortugues();
                    break;

                case "en-us":
                    messages = ConsMessagesSysId.MessagesEnglish();
                    break;

                case "es-es":
                    messages = ConsMessagesSysId.MessagesSpanish();
                    break;

                default:
                    throw new Exception("Language not supported.");

            }


            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(80))
            .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            var serializedMessagesJson = JsonConvert.SerializeObject(messages);
            var serializedMessagesBytes = Encoding.UTF8.GetBytes(serializedMessagesJson);
            DistributedCache.Set(key, serializedMessagesBytes, options);

        }

        public Dictionary<string, string> GetMessages()
        {
            var key = "errorMessages";
            var redisResultBytes = DistributedCache.Get(key);
            var redisResultJson = Encoding.UTF8.GetString(redisResultBytes);
            var redisResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(redisResultJson);

            return redisResult;
        }
    }

    public static class ConsMessagesSysId
    {
        #region Person

        public static string FirstNameNotEmptySysid => "e66be75c-6b81-49a5-b682-4cacd6df7e0d";
        public static string FirstNameNotNullSysid => "94b9c11f-c099-466e-bace-28d65b0d776c";
        public static string FirstNameMinimiumLengthSysid => "36801d96-bed8-424a-96ba-5af4e2df74e5";

        public static string LastNameMinimiumLengthSysid => "ea3cff5a-8f0d-4e7a-b203-e3b7c14c7912";
        public static string LastNameNotNullSysid => "ea4324597-437d-49ad-8a86-026874be3e3c";
        public static string LastNameNotEmptySysid => "98ce118a-3032-4c53-9ce3-390cd4bbe47c";

        public static string CpfNotEmptySysid => "6d20d280-ab4e-47b9-9455-711dfd555e0c";
        #endregion

        public static Dictionary<string, string> MessagesPortugues()
        {
            return new Dictionary<string, string>
            {
                { FirstNameNotEmptySysid, "Nome não pode ser vazio."},
                { FirstNameNotNullSysid, "Nome não pode ser nulo." },
                { FirstNameMinimiumLengthSysid, "Nome deve ter pelo menos 3 letras." },
                { LastNameNotEmptySysid, "Sobrenome não pode ser vazio." },
                { LastNameNotNullSysid, "Sobrenome não pode ser nulo." },
                { LastNameMinimiumLengthSysid, "Sobrenome deve ter pelo menos 3 letras." },
                { CpfNotEmptySysid, "Cpf não pode ser vazio." },
            };
        }

        public static Dictionary<string, string> MessagesEnglish()
        {
            return new Dictionary<string, string>
            {
                { FirstNameNotEmptySysid, "Name cannot be empty."},
                { FirstNameNotNullSysid, "Name cannot be null." },
                { FirstNameMinimiumLengthSysid, "Name must be at least 3 letters long." },
                { LastNameNotEmptySysid, "Last name cannot be empty." },
                { LastNameNotNullSysid, "Last name cannot be null." },
                { LastNameMinimiumLengthSysid, "Last name must be at least 3 letters long." },
                { CpfNotEmptySysid, "Cpf cannot be empty." },
            };
        }

        public static Dictionary<string, string> MessagesSpanish()
        {
            return new Dictionary<string, string>
            {
                { FirstNameNotEmptySysid, "El nombre no puede estar vacío."},
                { FirstNameNotNullSysid, "El nombre no puede ser nulo." },
                { FirstNameMinimiumLengthSysid, "El nombre debe tener al menos 3 letras." },
                { LastNameNotEmptySysid, "El apellido no puede estar vacío." },
                { LastNameNotNullSysid, "El apellido no puede ser nulo." },
                { LastNameMinimiumLengthSysid, "El apellido debe tener al menos 3 letras." },
                { CpfNotEmptySysid, "Cpf no puede estar vacío." },
            };
        }

    }
}
