using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using System;
using System.Threading.Tasks;
using Command = OpenMod.Core.Commands.Command;

namespace SimpleDuty.Commands
{
    [Command("duty")]
    [CommandAlias("d")]
    [CommandDescription("Enable or disable bluehammer")]
    public class DutyCommand : Command
    {
        public DutyCommand(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task OnExecuteAsync()
        {
            var uPlayer = Context.Actor as UnturnedUser;

            if (uPlayer == null) return;

            await UniTask.SwitchToMainThread();

            if (SteamAdminlist.checkAdmin(uPlayer.SteamId))
            {
                SteamAdminlist.unadmin(uPlayer.SteamId);
                await uPlayer.PrintMessageAsync("You have been unadmined");
            }
            else
            {
                SteamAdminlist.admin(uPlayer.SteamId, uPlayer.SteamId);
                await uPlayer.PrintMessageAsync("You have been admined");
            }

            await UniTask.SwitchToThreadPool();
        }
    }
}
