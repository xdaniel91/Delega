using Delega.Api.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Delega.Api.Utils;

/// <summary>
/// Refatoração pendente.
/// </summary>
public class ConstMessages : IConstMessages
{
    private readonly IDistributedCache DistributedCache;

    public ConstMessages(IDistributedCache distributedCache)
    {
        DistributedCache = distributedCache;
    }
    /// <summary>
    /// será refatorado futuramente.
    /// </summary>
    /// <param name="language"></param>
    public void SetMessages(string language = "pt-br")
    {
        var key = "errorMessages";
        var languageLower = language.ToLower();

        Dictionary<string, string> messages;

        switch (languageLower)
        {
            case "en-us":
                messages = ErrorMessages.MessagesEnglish();
                break;

            case "es-es":
                messages = ErrorMessages.MessagesSpanish();
                break;

            default:
                messages = ErrorMessages.MessagesPortugues();
                break;
        }


        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(80))
        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

        var serializedMessagesJson = JsonConvert.SerializeObject(messages);
        var serializedMessagesBytes = Encoding.UTF8.GetBytes(serializedMessagesJson);
        DistributedCache.Set(key, serializedMessagesBytes, options);

    }

    /// <summary>
    /// será refatorado futaramente.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetMessages()
    {
        var key = "errorMessages";
        var redisResultBytes = DistributedCache.Get(key);

        if (redisResultBytes is null)
        {
            SetMessages();
        }

        var redisResultJson = Encoding.UTF8.GetString(redisResultBytes);
        var redisResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(redisResultJson);
        return redisResult;
    }
}

public static class ErrorMessagesSysid
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


}

public static class ErrorMessages
{
    public static string GetMessageByLanguageSysid(string sysid, string language = "pt-br")
    {
        switch (language)
        {
            case "en-us":
                return MessagesEnglish()[sysid];
            case "es-es":
                return MessagesSpanish()[sysid];
            default:
                return MessagesPortugues()[sysid];
        }
    }

    public static Dictionary<string, string> MessagesPortugues()
    {
        return new Dictionary<string, string>
            {
                { ErrorMessagesSysid.FirstNameNotEmptySysid, "Nome não pode ser vazio."},
                { ErrorMessagesSysid.FirstNameNotNullSysid, "Nome não pode ser nulo." },
                { ErrorMessagesSysid.FirstNameMinimiumLengthSysid, "Nome deve ter pelo menos 3 letras." },
                { ErrorMessagesSysid.LastNameNotEmptySysid, "Sobrenome não pode ser vazio." },
                { ErrorMessagesSysid.LastNameNotNullSysid, "Sobrenome não pode ser nulo." },
                { ErrorMessagesSysid.LastNameMinimiumLengthSysid, "Sobrenome deve ter pelo menos 3 letras." },
                { ErrorMessagesSysid.CpfNotEmptySysid, "Cpf não pode ser vazio." },

                { ErrorMessagesSysid.AccusedNotNull, "Acusado não pode ser nulo." },
                { ErrorMessagesSysid.AuthorNotNull, "Autor não pode ser nulo." },
                { ErrorMessagesSysid.LawyerNotNull, "Advogado não pode ser nulo." },
                { ErrorMessagesSysid.RequestedValueNotEmpty, "Valor requerido não poder ser vazio." },
                { ErrorMessagesSysid.RequestedValueNotNull, "Valor requerido não pode ser nulo." },
                { ErrorMessagesSysid.RequestedValueGreatherThan, "Valor requerido deve ser maior que 0." },
                { ErrorMessagesSysid.ValueNotEmpty, "Valor não pode ser vazio." },
                { ErrorMessagesSysid.ValueNotNull, "Valor não pode ser nulo." },
                { ErrorMessagesSysid.ValueGreatherThan, "Valor deve ser maior que 0." },
                { ErrorMessagesSysid.ReasonNotNull, "Motivo não pode ser nulo." },
                { ErrorMessagesSysid.ReasonNotEmpty, "Motivo não pode ser vazio." },
                { ErrorMessagesSysid.ReasonMinimiumLenght, "Motivo deve ter pelo menos 10 caracteres." },
                { ErrorMessagesSysid.VerdictMinimiumLenght, "Sentença deve ter pelo menos 10 caracteres." },
                { ErrorMessagesSysid.StatusInvalid, "Status inválido." },
                { ErrorMessagesSysid.DateHourCreatedInvalid, "Data hora de criação inválida." },
                { ErrorMessagesSysid.DateHourInProgressInvalid, "Data hora de inprogress inválida." },
                { ErrorMessagesSysid.DateHourFinishedInvalid, "Data hora de finalização inválida." },
            };
    }

    public static Dictionary<string, string> MessagesEnglish()
    {
        return new Dictionary<string, string>
            {
                { ErrorMessagesSysid.FirstNameNotEmptySysid, "Name cannot be empty."},
                { ErrorMessagesSysid.FirstNameNotNullSysid, "Name cannot be null." },
                { ErrorMessagesSysid.FirstNameMinimiumLengthSysid, "Name must be at least 3 letters long." },
                { ErrorMessagesSysid.LastNameNotEmptySysid, "Last name cannot be empty." },
                { ErrorMessagesSysid.LastNameNotNullSysid, "Last name cannot be null." },
                { ErrorMessagesSysid.LastNameMinimiumLengthSysid, "Last name must be at least 3 letters long." },
                { ErrorMessagesSysid.CpfNotEmptySysid, "Cpf cannot be empty." },

                {ErrorMessagesSysid.AccusedNotNull, "Accused cannot be void." },
                {ErrorMessagesSysid.AuthorNotNull, "Author cannot be null." },
                {ErrorMessagesSysid.LawyerNotNull, "Lawyer cannot be null." },
                {ErrorMessagesSysid.RequestedValueNotEmpty, "Required value cannot be empty." },
                {ErrorMessagesSysid.RequestedValueNotNull, "Required value cannot be null." },
                {ErrorMessagesSysid.RequestedValueGreatherThan, "Required value must be greater than 0." },
                {ErrorMessagesSysid.ValueNotEmpty, "Value cannot be empty." },
                {ErrorMessagesSysid.ValueNotNull, "Value cannot be null." },
                {ErrorMessagesSysid.ValueGreatherThan, "Value must be greater than 0." },
                {ErrorMessagesSysid.ReasonNotNull, "Reason cannot be null." },
                {ErrorMessagesSysid.ReasonNotEmpty, "Reason cannot be empty." },
                {ErrorMessagesSysid.ReasonMinimiumLenght, "Reason must be at least 10 characters long." },
                {ErrorMessagesSysid.VerdictMinimiumLenght, "Sentence must be at least 10 characters long." },
                {ErrorMessagesSysid.StatusInvalid, "Invalid status." },
                {ErrorMessagesSysid.DateHourCreatedInvalid, "Invalid creation datetime." },
                {ErrorMessagesSysid.DateHourInProgressInvalid, "Inprogress datetime is invalid." },
                {ErrorMessagesSysid.DateHourFinishedInvalid, "Invalid end date time." },
            };
    }

    public static Dictionary<string, string> MessagesSpanish()
    {
        return new Dictionary<string, string>
            {
                { ErrorMessagesSysid.FirstNameNotEmptySysid, "El nombre no puede estar vacío."},
                { ErrorMessagesSysid.FirstNameNotNullSysid, "El nombre no puede ser nulo." },
                { ErrorMessagesSysid.FirstNameMinimiumLengthSysid, "El nombre debe tener al menos 3 letras." },
                { ErrorMessagesSysid.LastNameNotEmptySysid, "El apellido no puede estar vacío." },
                { ErrorMessagesSysid.LastNameNotNullSysid, "El apellido no puede ser nulo." },
                { ErrorMessagesSysid.LastNameMinimiumLengthSysid, "El apellido debe tener al menos 3 letras." },
                { ErrorMessagesSysid.CpfNotEmptySysid, "Cpf no puede estar vacío." },

                { ErrorMessagesSysid.AccusedNotNull, "Acusado no puede ser nulo." },
                { ErrorMessagesSysid.AuthorNotNull, "El autor no puede ser nulo." },
                { ErrorMessagesSysid.LawyerNotNull, "Advogado não pode ser nulo." },
                { ErrorMessagesSysid.RequestedValueNotEmpty, "El valor requerido no puede estar vacío." },
                { ErrorMessagesSysid.RequestedValueNotNull, "El valor requerido no puede ser nulo." },
                { ErrorMessagesSysid.RequestedValueGreatherThan, "El valor requerido debe ser mayor que 0." },
                { ErrorMessagesSysid.ValueNotEmpty, "El valor no puede estar vacío." },
                { ErrorMessagesSysid.ValueNotNull, "El valor no puede ser nulo." },
                { ErrorMessagesSysid.ValueGreatherThan, "El valor debe ser mayor que 0." },
                { ErrorMessagesSysid.ReasonNotNull, "La razón no puede ser nula." },
                { ErrorMessagesSysid.ReasonNotEmpty, "La razón no puede estar vacía." },
                { ErrorMessagesSysid.ReasonMinimiumLenght, "El motivo debe tener al menos 10 caracteres." },
                { ErrorMessagesSysid.VerdictMinimiumLenght, "La oración debe tener al menos 10 caracteres." },
                { ErrorMessagesSysid.StatusInvalid, "Estado inválido." },
                { ErrorMessagesSysid.DateHourCreatedInvalid, "Fecha y hora de creación no válida." },
                { ErrorMessagesSysid.DateHourInProgressInvalid, "La fecha y hora en curso no es válida." },
                { ErrorMessagesSysid.DateHourFinishedInvalid, "Hora de fecha de finalización no válida." },
            };
    }
}
    


