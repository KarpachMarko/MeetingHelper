using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Base.Extensions;
using WebApp.DTO.Identity;

namespace WebApp.TelegramAuthentication;

public static class TelegramAuthUtils
{

    public static TelegramData? ParseData(string initData, string hash)
    {
        if (!VerifyTelegramData(initData, hash))
        {
            return null;
        }

        var dataDict = new Dictionary<string, string>();
        foreach (var pair in initData.Split("\n"))
        {
            var key = pair.Split("=")[0];
            var value = pair.Split("=")[1];

            if (value.StartsWith("{"))
            {
                var json = JsonNode.Parse(value);
                var dictionary = json.Deserialize<Dictionary<string, object>>();
                if (dictionary == null) continue;

                var dictModified = dictionary.ToDictionary(valuePair => valuePair.Key,
                    valuePair => valuePair.Value.ToString() ?? "");
                dataDict = dataDict.Merge(dictModified, values => values.Last());
            }
            else
            {
                dataDict.Add(key, value);
            }
        }

        return new TelegramData
        {
            TelegramId = dataDict["id"],
            FirstName = dataDict["first_name"],
            LastName = dataDict["last_name"],
            UserName = dataDict["username"]
        };
    }

    public static bool VerifyTelegramData(string initData, string hash)
    {
        var botToken = Environment.GetEnvironmentVariable("mh_bot_token") ?? "";
        var hmacWebData = new HMACSHA256(Encoding.UTF8.GetBytes("WebAppData"));
        var secretKey = hmacWebData.ComputeHash(Encoding.UTF8.GetBytes(botToken));

        var hmac = new HMACSHA256(secretKey);
        var calculatedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(initData));
        var calculatedHashString = Convert.ToHexString(calculatedHash);

        if (!calculatedHashString.ToLower().Equals(hash.ToLower()))
        {
            return false;
        }
        
        var authDate = ParseAuthDate(initData);

        // return DateTime.UtcNow < authDate.AddMinutes(15); // TODO: Deal with old authDate
        return true;
    }
    
    private static DateTime ParseAuthDate(string initData)
    {
        var dateTime = DateTime.UnixEpoch;;
        
        foreach (var pair in initData.Split("\n"))
        {
            var key = pair.Split("=")[0];
            var value = pair.Split("=")[1];

            if (!key.ToLower().Equals("auth_date")) continue;
            
            dateTime = dateTime.AddSeconds( int.Parse(value) ).ToLocalTime();
            break;
        }

        return dateTime;
    }
}