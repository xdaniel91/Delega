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

        #region JudicialProcess

        public static string AccusedNotNull => "ecbf1d3f-10b4-451d-961f-4757c649b718";
        public static string AuthorNotNull => "9a67098a-31f7-4014-9470-c98c2c31e56d";
        public static string LawyerNotNull => "2464393d-204d-406d-a361-b4de4647fdfe";
        public static string RequestedValueNotEmpty => "6b568669-54e2-4205-b6ec-dc1aa7ed1465";
        public static string RequestedValueNotNull => "2f4fae65-7689-406c-b01f-ce5be4a46fbc";
        public static string RequestedValueGreatherThan => "a88fda51-7108-43bd-932f-28b353a5fdc8";
        public static string ValueNotEmpty => "dcd92579-5409-4d26-8336-d2ccc5fd362f";
        public static string ValueNotNull => "f6b88248-adfd-424f-9fc4-65e990087af7";
        public static string ValueGreatherThan => "d44bbc3c-2054-48b6-867f-4f7fe46a91f4";
        public static string ReasonNotNull => "3aa866b9-d09e-4215-bd25-2c6b87371b03";
        public static string ReasonNotEmpty => "b43eab4c-f983-486b-a31b-00883ffcf236";
        public static string ReasonMinimiumLenght => "873d28e8-fb6f-4908-a937-c36af6c2e55f";
        public static string VerdictMinimiumLenght => "53f5b661-51fb-485f-8e0d-5add6f883fae";
        public static string StatusInvalid => "d2aab2cb-e4ed-44ab-a282-9cfd8f7050e4";
        public static string DateHourCreatedInvalid => "1b8b9099-c2bf-4421-ae2c-98d92847f351";
        public static string DateHourInProgressInvalid => "35147546-bdcb-42fc-a191-7375d84c6fae";
        public static string DateHourFinishedInvalid => "f028a2d5-fbd5-4179-b99c-16a86495d32d";


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

                { AccusedNotNull, "Acusado não pode ser nulo." },
                { AuthorNotNull, "Autor não pode ser nulo." },
                { LawyerNotNull, "Advogado não pode ser nulo." },
                { RequestedValueNotEmpty, "Valor requerido não poder ser vazio." },
                { RequestedValueNotNull, "Valor requerido não pode ser nulo." },
                { RequestedValueGreatherThan, "Valor requerido deve ser maior que 0." },
                { ValueNotEmpty, "Valor não pode ser vazio." },
                { ValueNotNull, "Valor não pode ser nulo." },
                { ValueGreatherThan, "Valor deve ser maior que 0." },
                { ReasonNotNull, "Motivo não pode ser nulo." },
                { ReasonNotEmpty, "Motivo não pode ser vazio." },
                { ReasonMinimiumLenght, "Motivo deve ter pelo menos 10 caracteres." },
                { VerdictMinimiumLenght, "Sentença deve ter pelo menos 10 caracteres." },
                { StatusInvalid, "Status inválido." },
                { DateHourCreatedInvalid, "Data hora de criação inválida." },
                { DateHourInProgressInvalid, "Data hora de inprogress inválida." },
                { DateHourFinishedInvalid, "Data hora de finalização inválida." },
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

                { AccusedNotNull, "Accused cannot be void." },
                { AuthorNotNull, "Author cannot be null." },
                { LawyerNotNull, "Lawyer cannot be null." },
                { RequestedValueNotEmpty, "Required value cannot be empty." },
                { RequestedValueNotNull, "Required value cannot be null." },
                { RequestedValueGreatherThan, "Required value must be greater than 0." },
                { ValueNotEmpty, "Value cannot be empty." },
                { ValueNotNull, "Value cannot be null." },
                { ValueGreatherThan, "Value must be greater than 0." },
                { ReasonNotNull, "Reason cannot be null." },
                { ReasonNotEmpty, "Reason cannot be empty." },
                { ReasonMinimiumLenght, "Reason must be at least 10 characters long." },
                { VerdictMinimiumLenght, "Sentence must be at least 10 characters long." },
                { StatusInvalid, "Invalid status." },
                { DateHourCreatedInvalid, "Invalid creation datetime." },
                { DateHourInProgressInvalid, "Inprogress datetime is invalid." },
                { DateHourFinishedInvalid, "Invalid end date time." },
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
                
                { AccusedNotNull, "Acusado no puede ser nulo." },
                { AuthorNotNull, "El autor no puede ser nulo." },
                { LawyerNotNull, "Advogado não pode ser nulo." },
                { RequestedValueNotEmpty, "El valor requerido no puede estar vacío." },
                { RequestedValueNotNull, "El valor requerido no puede ser nulo." },
                { RequestedValueGreatherThan, "El valor requerido debe ser mayor que 0." },
                { ValueNotEmpty, "El valor no puede estar vacío." },
                { ValueNotNull, "El valor no puede ser nulo." },
                { ValueGreatherThan, "El valor debe ser mayor que 0." },
                { ReasonNotNull, "La razón no puede ser nula." },
                { ReasonNotEmpty, "La razón no puede estar vacía." },
                { ReasonMinimiumLenght, "El motivo debe tener al menos 10 caracteres." },
                { VerdictMinimiumLenght, "La oración debe tener al menos 10 caracteres." },
                { StatusInvalid, "Estado inválido." },
                { DateHourCreatedInvalid, "Fecha y hora de creación no válida." },
                { DateHourInProgressInvalid, "La fecha y hora en curso no es válida." },
                { DateHourFinishedInvalid, "Hora de fecha de finalización no válida." },
            };
        }

    }
}
