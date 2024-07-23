using PuppeteerExtraSharp;
using PuppeteerExtraSharp.Plugins.AnonymizeUa;
using PuppeteerExtraSharp.Plugins.BlockResources;
using PuppeteerExtraSharp.Plugins.ExtraStealth;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangoMangoSOLID
{
    class Program
    {

        public String nameOfBrowser = "msedge.exe"; //change in 2 places
        public class Browser
        {
            public IBrowser browser;
            public List<IPage> pages = new List<IPage>();
            public bool isLogined = false;
            //just for remind aboit it:
            String pathToBrowser = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
            public String login = "";
            public String password = "";
            
            public Browser(bool headless = false)
            {
                addBrowser(headless);
            }
            public Browser(String login_name, String pass, bool headless = false)
            {
                login = login_name;
                password = pass;
                addBrowser(headless);
            }

            async void addBrowser(bool headless)
            {
                var blockPlugin = new BlockResourcesPlugin();
                var extra = new PuppeteerExtra().Use(new AnonymizeUaPlugin()).Use(new StealthPlugin());

                //block staff on page
                blockPlugin.AddRule(builder => builder.BlockedResources(
                    ResourceType.Image,
                    ResourceType.Img,
                    ResourceType.Media,
                    ResourceType.Font,
                    ResourceType.Manifest,
                    ResourceType.EventSource,
                    ResourceType.Ping,
                    ResourceType.TextTrack,
                    ResourceType.Unknown,
                    ResourceType.WebSocket,


                    /*
                     * BLOCKING SITE FULLY
                     * 
                     ResourceType.StyleSheet,
                     ResourceType.Document,
                     ResourceType.Script,
                     ResourceType.Other,
                     ResourceType.Fetch,
                     */
                    ResourceType.Xhr
                    ).ForUrl("tango.me"));

                extra.Use(blockPlugin); //blocker

                var log_name = login.Split('@')[0];


                LaunchOptions browserOptions;

                browserOptions = new LaunchOptions
                {
                    Headless = headless,
                    ExecutablePath = pathToBrowser,
                    Args = new String[40] { "--fast-start", "--disable-extensions", "--no-sandbox", "--disable-dev-shm-usage",

                    //HERE STARTS THIS LIST 

                    "--autoplay-policy=user-gesture-required",
                "--disable-background-networking",
                "--disable-background-timer-throttling",
                "--disable-backgrounding-occluded-windows",
                "--disable-breakpad",
                "--disable-client-side-phishing-detection",
                "--disable-component-update",
                "--disable-default-apps",
                "--disable-dev-shm-usage",
                "--disable-domain-reliability",
                "--disable-extensions",
                "--disable-features=AudioServiceOutOfProcess",
                "--disable-hang-monitor",
                "--disable-ipc-flooding-protection",
                "--disable-notifications",
                "--disable-offer-store-unmasked-wallet-cards",
                "--disable-popup-blocking",
                "--disable-print-preview",
                "--disable-prompt-on-repost",
                "--disable-renderer-backgrounding",
                "--disable-setuid-sandbox",
                "--disable-speech-api",
                "--disable-sync",
                "--hide-scrollbars",
                "--ignore-gpu-blacklist",
                "--metrics-recording-only",
                "--mute-audio",
                "--no-default-browser-check",
                "--no-first-run",
                "--no-pings",
                "--no-sandbox",
                "--no-zygote",
                "--password-store=basic",
                "--use-gl=swiftshader",
                "--use-mock-keychain"


                //HERE END OF THIS LIST


                    ,"--disable-plugins" }, //best (silent&show)
                                            // "--headless",
                    IgnoreHTTPSErrors = true,
                    UserDataDir = $"userData\\usersTango\\{log_name}"
                };


                //var browser
                browser = await extra.LaunchAsync(browserOptions);
            }
            public async Task updatePages()
            {
                var new_pages = await browser.PagesAsync();
                pages = new_pages.ToList();
            }
            public async void openAndTryLogin(string URL = "https://tango.me/")
            {
                openURL(URL);
                while (pages.Count == 0)
                {
                    await Task.Delay(100); // Wait until browser is initialized
                }
                await check(pages.Last());
            }
            public async void openURL(string URL = "https://tango.me/")
            {
                while (browser == null)
                {
                    await Task.Delay(100); // Wait until browser is initialized
                }
                var page = await browser.NewPageAsync(); // initialize

                //делает медленнее await page.SetRequestInterceptionAsync(true);
                //--making slower await page.GoToAsync(URL, WaitUntilNavigation.DOMContentLoaded ); // go to url

                //speed up (CPU++)
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 0,
                    Height = 0
                });

                //compress staff
                await page.SetExtraHttpHeadersAsync(new Dictionary<string, string> { { "Accept-Encoding", "gzip, deflate, br" } });

                await page.GoToAsync(URL); // go to url
                pages.Add(page); // save access to this page
            }
            public async void closeURL(String URL)
            {
                while (browser == null)
                {
                    await Task.Delay(100); // Wait until browser is initialized
                }
                pages.Where(x => x.Url.Contains(URL)).ToList().ForEach(x => x.CloseAsync()); //close them
            }
            async void GAuth(IPage GAuthPage)
            {
                if (!isLogined)
                {
                    try
                    {
                        if (GAuthPage.Url.Contains("oauth2"))
                        {
                            GAuthOpen(GAuthPage); // just choose & click
                        }
                        else
                        {
                            GAuthFull(GAuthPage); // login + password
                        }
                    }
                    catch
                    {
                        isLogined = true;
                    };
                }

            }
            async void GAuthOpen(IPage GAuthPage)
            {
                //login page
                await GAuthPage.WaitForSelectorAsync("div.VV3oRb.YZVTmd.SmR8");
                var submitElement = await GAuthPage.QuerySelectorAsync("div.VV3oRb.YZVTmd.SmR8");
                await submitElement.ClickAsync();

                //okay page
                while (GAuthPage.Url.Contains("oauthchooseaccount?"))
                {
                    await Task.Delay(100); // Wait until browser is initialized
                }
                await GAuthPage.WaitForSelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");
                var submitElements = await GAuthPage.QuerySelectorAllAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");//
                submitElement = submitElements.Last();
                await submitElement.ClickAsync();

                while (pages.IndexOf(GAuthPage) != -1)
                {
                    updatePages();
                    await Task.Delay(1000);
                }
                isLogined = true;
            }
            async void GAuthFull(IPage GAuthPage)
            {
                //String gAuthPageURL
                //var GAuthPage = await browser.NewPageAsync(); // initialize
                //await GAuthPage.SetBypassCSPAsync(true);
                //await GAuthPage.GoToAsync(gAuthPageURL); // go to url

                //login page
                await GAuthPage.WaitForSelectorAsync("input#identifierId.whsOnd.zHQkBf");
                var field = await GAuthPage.QuerySelectorAsync("input#identifierId.whsOnd.zHQkBf");
                await field.TypeAsync(login);
                var submitElement = await GAuthPage.QuerySelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.LQeN7.BqKGqe.Jskylb.TrZEUc.lw1w4b");
                await submitElement.ClickAsync();


                //password page
                while (!GAuthPage.Url.Contains("pwd?"))
                {
                    await Task.Delay(100); // Wait until browser is initialized
                }
                field = await GAuthPage.QuerySelectorAsync("input.whsOnd.zHQkBf"); //
                await field.TypeAsync(password);
                submitElement = await GAuthPage.QuerySelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.LQeN7.BqKGqe.Jskylb.TrZEUc.lw1w4b");//
                await submitElement.ClickAsync();

                //okay page
                while (GAuthPage.Url.Contains("pwd?"))
                {
                    await Task.Delay(100); // Wait until browser is initialized
                }
                await GAuthPage.WaitForSelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");
                var submitElements = await GAuthPage.QuerySelectorAllAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");//
                submitElement = submitElements.Last();
                await submitElement.ClickAsync();

                while (pages.IndexOf(GAuthPage) != -1)
                {
                    updatePages();
                    await Task.Delay(1000);
                }
                isLogined = true;
            }
            public async void AuthOnTango(IPage page)
            {
                // await page.WaitForSelectorAsync("button.UNPbK.wgwPg.ml1sV.FSc9M.X4J4Q", new WaitForSelectorOptions { Timeout = 20000 }); //wait for button

                //await page.WaitForSelectorAsync("button.UNPbK.wgwPg.oZjiQ.FSc9M.FTeqR"); //wait for button

                isLogined = false;
                var gButton = await page.QuerySelectorAsync("button.UNPbK.wgwPg.oZjiQ.FSc9M.FTeqR");
                if (gButton != null)
                {
                    await gButton.ClickAsync( //open GAuth menu
                        ); //new ClickOptions() { Button = MouseButton.Right }
                    Console.WriteLine("GAuth Clicked.");
                    updatePages();
                    while (pages.Count < 2) //waiting for GAuth window
                    {
                        await Task.Delay(100); // Wait until browser is initialized
                    }
                    IPage GAuthPage = pages.First(x => x.Url.Contains("accounts.google.com"));
                    String gAuthURL = GAuthPage.Url;
                    //await GAuthPage.CloseAsync();
                    await Task.Run(() => { GAuth(GAuthPage); }); //AUTH ON GOOGLE
                }
            }
            public async void refreshAndCheck(IPage page)
            {
                isLogined = false;
                await page.ReloadAsync(30000); //reload
                                               //--making slower await page.ReloadAsync(30000, new WaitUntilNavigation[1] { WaitUntilNavigation.DOMContentLoaded }); //reload

                await check(page);

            }
            public async Task check(IPage page)
            {

                await Task.Run(async () =>
                {
                    try
                    {
                        //is there button for logining
                        await page.WaitForSelectorAsync("button.UNPbK.wgwPg.oZjiQ.FSc9M.FTeqR", new WaitForSelectorOptions { Timeout = 15000 }); //wait for button
                                                                                                                                                 //then login
                        AuthOnTango(page);
                    }
                    catch
                    {
                        Console.WriteLine("No GButton on Tango Page");
                        //or it already loginned
                        isLogined = true;
                    };
                });
            }
            //Browser ends here
        }


        static void Main()
        {

        }

        //global vars
        List<Browser> browsers = new List<Browser>();


    }


}
