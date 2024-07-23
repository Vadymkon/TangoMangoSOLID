using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangoMangoSOLID.Interfaces;

namespace TangoMangoSOLID.Classes.Auth
{
    public class AuthGooglePuppetter : AuthGoogle
    {
        String login, password;
        PagePuppetter GAuthPage;

        public AuthGooglePuppetter(String login, String password, PagePuppetter page)
        {
            this.login = login;
            this.password = password;
            this.GAuthPage = page;
        }
        public void GAuth()
        {
            try
            {
                if (GAuthPage.URL.Contains("oauth2"))
                {
                    justPressContinue(); // just choose & click
                }
                else
                {
                    passLoginForm(); // login + password
                }
            }
            catch
            {
            };
        }
        public async void justPressContinue()                                
        {
            //login page
            await GAuthPage.page.WaitForSelectorAsync("div.VV3oRb.YZVTmd.SmR8");
            var submitElement = await GAuthPage.page.QuerySelectorAsync("div.VV3oRb.YZVTmd.SmR8");
            await submitElement.ClickAsync();

            //okay page
            while (GAuthPage.URL.Contains("oauthchooseaccount?"))
            {
                await Task.Delay(100); // Wait until browser is initialized
            }
            await GAuthPage.page.WaitForSelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");
            var submitElements = await GAuthPage.page.QuerySelectorAllAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");//
            submitElement = submitElements.Last();
            await submitElement.ClickAsync();
        }
        public async void passLoginForm()   
        {
            //login page
            await GAuthPage.page.WaitForSelectorAsync("input#identifierId.whsOnd.zHQkBf");
            var field = await GAuthPage.page.QuerySelectorAsync("input#identifierId.whsOnd.zHQkBf");
            await field.TypeAsync(login);
            var submitElement = await GAuthPage.page.QuerySelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.LQeN7.BqKGqe.Jskylb.TrZEUc.lw1w4b");
            await submitElement.ClickAsync();


            //password page
            while (!GAuthPage.URL.Contains("pwd?"))
            {
                await Task.Delay(100); // Wait until browser is initialized
            }
            field = await GAuthPage.page.QuerySelectorAsync("input.whsOnd.zHQkBf"); //
            await field.TypeAsync(password);
            submitElement = await GAuthPage.page.QuerySelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.LQeN7.BqKGqe.Jskylb.TrZEUc.lw1w4b");//
            await submitElement.ClickAsync();

            //okay page
            while (GAuthPage.URL.Contains("pwd?"))
            {
                await Task.Delay(100); // Wait until browser is initialized
            }
            await GAuthPage.page.WaitForSelectorAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");
            var submitElements = await GAuthPage.page.QuerySelectorAllAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-INsAgc.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.Rj2Mlf.OLiIxf.PDpWxe.P62QJc.LQeN7.BqKGqe.pIzcPc.TrZEUc.lw1w4b");//
            submitElement = submitElements.Last();
            await submitElement.ClickAsync();
        }
    }
}
