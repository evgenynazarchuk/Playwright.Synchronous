using Microsoft.Playwright;

namespace Playwright.Synchronous;

public static class PlaywrightSynchronous
{
    public static IPlaywright Create()
    {
        return Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();
    }
}
