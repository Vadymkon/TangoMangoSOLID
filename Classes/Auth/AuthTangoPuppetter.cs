using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangoMangoSOLID.Interfaces;

namespace TangoMangoSOLID.Classes.Auth
{
    public class AuthTangoPuppetter : AuthTango
    {
        String login, password;
        PagePuppetter page;

        public AuthTangoPuppetter(String login, String password, PagePuppetter page)
        {
            this.login = login;
            this.password = password;
            this.page = page;
        }

        async public void AuthOnTango()
        {
            var gButton = await page.page.QuerySelectorAsync("button.UNPbK.wgwPg.oZjiQ.FSc9M.FTeqR");
            if (gButton != null)
            {
                await gButton.ClickAsync(); 
                Console.WriteLine("GAuth Clicked.");
            }
        }
    }
}