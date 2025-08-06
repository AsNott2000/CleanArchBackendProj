using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Application.Attributes;

public class FeatureManagerAttribute(string featureKey) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 1. Servis provider’dan IFeatureManager’ı çekiyoruz
        var featureManager = (IFeatureManager)context.HttpContext.RequestServices.GetRequiredService(typeof(IFeatureManager));
        // 2. İlgili feature (anahtar) aktif mi diye bakıyoruz
        if (!await featureManager.IsEnabledAsync(featureKey))
        {
            // 3. Kapalıysa özel exception fırlatılıyor
            throw new Exceptions.NotImplementedException(Resources.Messages.NotImplemented);
        }
        // 4. Açık ise işleme devam
        await next();
    }
}

/*
    [FeatureManager("YeniLoginEkrani")]
    public async Task<IActionResult> Login()
    {
        // Eğer "YeniLoginEkrani" özelliği aktifse burası çalışır
    }
*/