using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangoMangoSOLID.Interfaces;
using TangoMangoSOLID.Classes.Auth;
using PuppeteerSharp;

namespace TangoMangoSOLID.Classes
{
    public class PagePuppetter : Interfaces.Page
    {
        public PuppeteerSharp.IPage page;
        String login, password;

        public string URL { get => page.Url; }

        
        public PagePuppetter(PuppeteerSharp.IPage page, String login, String password)
        {
            this.page = page;
            this.login = login;
            this.password = password;
        }

        async public void Refresh()
        {
            await page.ReloadAsync(30000); //reload
            
            // --making slower await
            //page.ReloadAsync(30000, new WaitUntilNavigation[1] { WaitUntilNavigation.DOMContentLoaded }); //reload
        }

        async public Task<bool> Check()
        {
            var button = await this.page.WaitForSelectorAsync("button.UNPbK.wgwPg.oZjiQ.FSc9M.FTeqR", new WaitForSelectorOptions { Timeout = 15000 }); //wait for button
            return button != null;
        }
        async public void Close()
        {
            await this.page.CloseAsync();
        }
        public void Auth()
        {
            if (Check().GetAwaiter().GetResult())
            {
                new AuthTangoPuppetter(login, password, this).AuthOnTango();
            }
            new AuthGooglePuppetter(login, password, this).GAuth();
        }
    }
}
