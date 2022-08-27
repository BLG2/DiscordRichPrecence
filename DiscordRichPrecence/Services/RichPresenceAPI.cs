using DiscordRPC;
using System;

namespace DiscordRichPrecence.Services
{
    public static class RichPresenceAPI
    {
        public static DiscordRpcClient Client;
        public static RichPresence Presence;

        static RichPresenceAPI()
        {
            Client = new DiscordRpcClient("1012301758229401682");
            Presence = new RichPresence
            {
                Details = "RPC Details",
                State = "RPC State",
                Assets = new Assets
                {
                    LargeImageKey = "https://imgur.com/LTuCjcG.png",
                    LargeImageText = $"LargeImgText",
                    SmallImageKey = "https://imgur.com/LTuCjcG.png",
                    SmallImageText = $"SmallImgText"
                },
                Buttons = new Button[] {}
            };
        }

        public static void Initialize()
        {
            try
            {
                Client.Initialize();
                Update();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Update(bool resetTimer = false)
        {
            try
            {
                if (Client.IsInitialized)
                {
                    if (resetTimer) Presence.Timestamps = Timestamps.Now;
                    Client.SetPresence(Presence);
                }
                else
                {
                    Client.Initialize();
                    if (resetTimer) Presence.Timestamps = Timestamps.Now;
                    Client.SetPresence(Presence);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Disable()
        {
            try
            {
                if (Client.IsInitialized)
                {
                    Client.ClearPresence();
                }
                else
                {
                    Client.Initialize();
                    Client.ClearPresence();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
