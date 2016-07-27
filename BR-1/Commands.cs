using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BR_1
{
    static class Commands
    {
        public static void CreateCommands()
        {
            var cService = Program._client.GetService<CommandService>();
            // Set IDs for all the faction roles
            ulong vanuID = 175844028997304320;
            ulong terranID = 175844070256541696;
            ulong conglomerateID = 175844232181841920;
            // Make an array for the long copypastas for the !dank command
            string[] copypastas =
            {
                "What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.",
                "Here's the thing. You said a \"jackdaw is a crow.\" Is it in the same family? Yes. No one's arguing that. As someone who is a scientist who studies crows, I am telling you, specifically, in science, no one calls jackdaws crows. If you want to be \"specific\" like you said, then you shouldn't either. They're not the same thing. If you're saying \"crow family\" you're referring to the taxonomic grouping of Corvidae, which includes things from nutcrackers to blue jays to ravens. So your reasoning for calling a jackdaw a crow is because random people \"call the black ones crows?\" Let's get grackles and blackbirds in there, then, too. Also, calling someone a human or an ape ? It's not one or the other, that's not how taxonomy works.They're both. A jackdaw is a jackdaw and a member of the crow family. But that's not what you said.You said a jackdaw is a crow, which is not true unless you're okay with calling all members of the crow family crows, which means you'd call blue jays, ravens, and other birds crows, too.Which you said you don't. It's okay to just admit you're wrong, you know ?"
            };
            // Instantiate a Random class, used in !v6
            Random rand = new Random();

            #region !ping
            cService.CreateCommand("ping")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("pong");
                });
            #endregion
            #region !faction
            cService.CreateCommand("faction")
                .Description("Usage: !faction [vs|tr|nc|remove] Description: Assign yourself a faction role and a colored name.")
                .Parameter("role", ParameterType.Required)
                .Do(async (e) =>
                {
                    switch (e.GetArg("role"))
                    {
                        case "vs":
                            await e.User.RemoveRoles(Program._planetside.GetRole(terranID));
                            await e.User.RemoveRoles(Program._planetside.GetRole(conglomerateID));
                            await e.User.AddRoles(Program._planetside.GetRole(vanuID));
                            break;
                        case "tr":
                            await e.User.RemoveRoles(Program._planetside.GetRole(vanuID));
                            await e.User.RemoveRoles(Program._planetside.GetRole(conglomerateID));
                            await e.User.AddRoles(Program._planetside.GetRole(terranID));
                            break;
                        case "nc":
                            await e.User.RemoveRoles(Program._planetside.GetRole(vanuID));
                            await e.User.RemoveRoles(Program._planetside.GetRole(terranID));
                            await e.User.AddRoles(Program._planetside.GetRole(conglomerateID));
                            break;
                        case "remove":
                            await e.User.RemoveRoles(Program._planetside.GetRole(vanuID));
                            await e.User.RemoveRoles(Program._planetside.GetRole(terranID));
                            await e.User.RemoveRoles(Program._planetside.GetRole(conglomerateID));
                            break;
                        default:
                            await e.Channel.SendMessage($"\"{e.GetArg("role")}\" is an invalid argument. Valid arguments are vs, tr, nc, and remove.");
                            break;
                    }
                });
            #endregion
            #region !v6
            cService.CreateCommand("v6")
                .Description("Taunt the chat.")
                .Do(async (e) =>
                {
                    if (e.User.HasRole(Program._planetside.GetRole(vanuID)))
                    {
                        switch (rand.Next(0, 4))
                        {
                            case 0: await e.Channel.SendMessage("We are your only salvation!"); break;
                            case 1: await e.Channel.SendMessage("You will not stand in my way!"); break;
                            case 2: await e.Channel.SendMessage("Find enlightenment."); break;
                            case 3: await e.Channel.SendMessage("No despair!"); break;
                        }
                    }
                    else if (e.User.HasRole(Program._planetside.GetRole(terranID)))
                    {
                        switch (rand.Next(0, 2)) // TODO find TR taunts
                        {
                            case 0: await e.Channel.SendMessage("You call yourself a soldier?"); break;
                            case 1: await e.Channel.SendMessage("It's almost too easy!"); break;
                        }
                    }
                    else if (e.User.HasRole(Program._planetside.GetRole(conglomerateID)))
                    {
                        switch (rand.Next(0, 1)) // TODO find NC taunts
                        {
                            case 0: await e.Channel.SendMessage("Sometimes, I even impress myself."); break;
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("Your best is my worst.");
                    }
                });
            #endregion
            #region !getIDs
            cService.CreateCommand("getIDs")
                .Description("For debugging purposes.")
                .Do(async (e) =>
                {
                    if (e.User.Id == 72080813948153856) // MarkusGordathian's ID
                    {
                        foreach (Role r in Program._planetside.Roles)
                        {
                            Console.WriteLine($"{r.Name} : {r.Id}");
                        }
                        foreach (Channel c in Program._planetside.TextChannels)
                        {
                            Console.WriteLine($"{c.Name} : {c.Id}");
                        }

                        await e.Channel.SendMessage("IDs written to console.");
                    }
                    else
                    {
                        await e.Channel.SendMessage("You don't have access to that command.");
                    }
                });
            #endregion
            #region !v4
            cService.CreateCommand("v4")
                .Description("Ask for some ammo.")
                .Do(async (e) =>
                {
                    if (e.User.HasRole(Program._planetside.GetRole(vanuID)))
                    {
                        await e.Channel.SendMessage($"{e.User.Nickname} needs some batteries!");
                    }
                });
            #endregion
            #region !dank
            cService.CreateCommand("dank")
                .Description("Get a text emote of your choice.")
                .Parameter("meme", ParameterType.Required)
                .Do(async (e) =>
                {
                    switch (e.GetArg("meme"))
                    {
                        case "disapprove": await e.Channel.SendMessage("ಠ_ಠ"); await e.Message.Delete(); break;
                        case "lenny": await e.Channel.SendMessage("( ͡° ͜ʖ ͡°)"); await e.Message.Delete(); break;
                        case "lennyarmy": await e.Channel.SendMessage("( ͡°( ͡° ͜ʖ( ͡° ͜ʖ ͡°)ʖ ͡°) ͡°)"); await e.Message.Delete(); break;
                        case "creepylen": await e.Channel.SendMessage("┬┴┬┴┤ ͜ʖ ͡°) ├┬┴┬┴"); await e.Message.Delete(); break;
                        case "fuckyeah": await e.Channel.SendMessage("(ง ͠° ͟ل͜ ͡°)ง"); await e.Message.Delete(); break;
                        case "fightme": await e.Channel.SendMessage("(ง'̀-'́)ง"); await e.Message.Delete(); break;
                        case "5dolla": await e.Channel.SendMessage("[̲̅$̲̅(̲̅5̲̅)̲̅$̲̅]"); await e.Message.Delete(); break;
                        case "awyeah": await e.Channel.SendMessage("(•_•) ( •_•)>⌐■-■ (⌐■_■)"); await e.Message.Delete(); break;
                        case "navyseal": await e.Channel.SendMessage(copypastas[0]); await e.Message.Delete(); break;
                        case "jackdaw": await e.Channel.SendMessage(copypastas[1]); await e.Message.Delete(); break;
                        case "wow": await e.Channel.SendMessage("http://i.imgur.com/16fCUzF.gifv"); await e.Message.Delete(); break;
                    }
                });
            #endregion
            #region !prune
            cService.CreateCommand("prune")
                .Description("Remove up to 100 of the bot's messages.")
                .Parameter("NumberOfMessages", ParameterType.Required)
                .Do(async (e) =>
                {
                    if (e.User.HasRole(e.Server.GetRole(166320248348344320)))
                    {
                        var BotMessages =
                        (from message in await e.Channel.DownloadMessages()
                         where message.IsAuthor
                         select message).ToArray();

                        List<Message> MessagesToDelete = new List<Message>();
                        int i = 0;
                        while (i <= int.Parse(e.GetArg("NumberOfMessages")))
                        {
                            MessagesToDelete.Add(BotMessages[i]);
                            i++;
                        }

                        await e.Channel.DeleteMessages(MessagesToDelete.ToArray());
                    }
                    else { await e.Channel.SendMessage("You don't have access to that command."); }
                });
            #endregion
            #region !pp
            //cService.CreateCommand("pp")
            //    .Do(async (e) =>
            //    {
            //        await Program._client.CurrentUser.Edit(username: "BR_1");
            //    });
            #endregion
        }
    }
}
