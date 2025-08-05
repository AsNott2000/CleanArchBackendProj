using System;
using Microsoft.Extensions.Caching.Memory;

namespace BuildingBlocks.Application.Methods;

public static partial class ValidationHelper
{
    public static bool IsValidEnum<TEnum>(int data) where TEnum : struct, Enum
    {
        //gelen enum değerinin tanımlı olup olmadığını kontrol eder
        return Enum.IsDefined(typeof(TEnum), data);
    }
    public static bool IsValidMobile(string? mobile)
    {
        //gelen mobil numaranın geçerli olup olmadığını kontrol eder
        //Türkiye için geçerli bir mobil numara 10 haneli ve başında 0 olmamalıdır
        if (string.IsNullOrEmpty(mobile))
            return false;

        return mobile.Length == 10 && mobile.StartsWith("09");
    }
    [System.Text.RegularExpressions.GeneratedRegex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])")]
    private static partial System.Text.RegularExpressions.Regex EmailRegex();
    public static bool IsValidEmail(string? email)
    {
        //gelen email adresinin geçerli olup olmadığını kontrol eder. Standartlara uygun mu?
        if (string.IsNullOrEmpty(email))
            return false;

        var regex = EmailRegex();
        return regex.IsMatch(email);
    }

    public static bool IsValidObject(object? data)
    {
        //gelen nesnenin null olup olmadığını kontrol eder
        return data is not null;
    }

    public static bool IsValidString(string? data)
    {
        //gelen string verinin null veya boş olup olmadığını kontrol eder
        return !string.IsNullOrEmpty(data);
    }

    public static bool IsValidNationalCode(string? nationalCode)
    {
        //gelen TC kimlik numarasının geçerli olup olmadığını kontrol eder
        //TC kimlik numarası 10 haneli olmalı ve tüm rakamlar aynı olmamalıdır
        //Örnek: 12345678900 geçerli, 11111111111 geçersiz
        if (string.IsNullOrEmpty(nationalCode))
            return false;

        if (nationalCode.Length != 11)
            return false;

        return nationalCode switch
        {
            "00000000000" or
            "11111111111" or
            "22222222222" or
            "33333333333" or
            "44444444444" or
            "55555555555" or
            "66666666666" or
            "77777777777" or
            "88888888888" or
            "99999999999" => false,
            _ => true,
        };
    }

    public static bool IsValidLengthString(string? data, int minLength = 0, int maxLength = int.MaxValue)
    {
        //gelen string verinin uzunluğunu kontrol eder
        //minLength ve maxLength parametreleri ile minimum ve maksimum uzunlukları belirleriz
        //Eğer data null veya boş ise false döner
        if (string.IsNullOrEmpty(data))
            return false;

        return data.Length >= minLength && data.Length <= maxLength;
    }
    
    public static bool IsValidCaptchaCode(IMemoryCache memoryCache, string? captchaCode, string? encryptedCaptchaCode)
    {
        //gelen captcha kodunun geçerli olup olmadığını kontrol eder
        //memoryCache'den captcha kodunu alır ve karşılaştırma yapar
        if (string.IsNullOrEmpty(captchaCode) || string.IsNullOrEmpty(encryptedCaptchaCode))
            return false;

        if (memoryCache.TryGetValue(encryptedCaptchaCode, out string? cachedCaptchaCode))
        {
            if (string.Equals(captchaCode, cachedCaptchaCode, StringComparison.OrdinalIgnoreCase))
            {
                memoryCache.Remove(encryptedCaptchaCode);
                return true;
            }
        }

        return false;
    }

}
