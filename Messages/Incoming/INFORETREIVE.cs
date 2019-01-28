using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
{
    public class INFORETRIEVE : IMessage
    {
        public void Handle(Player player, Request request)
        {
            Response response = Response.Init("USEROBJECT");
            response.AppendKVArgument("name", "Alex");
            response.AppendKVArgument("figure", "sd=001/0&hr=005/223,218,190&hd=002/255,204,153&ey=001/0&fc=001/255,204,153&bd=001/255,204,153&lh=001/255,204,153&rh=001/255,204,153&ch=008/102,102,102&ls=001/102,102,102&rs=001/102,102,102&lg=006/149,120,78&sh=003/47,45,38");
            response.AppendKVArgument("email", "alex.daniel.97@gmail.com");
            response.AppendKVArgument("birthday", "06/07/1997");
            response.AppendKVArgument("phonenumber", "+44");
            response.AppendKVArgument("customData", "Alex lmao!");
            response.AppendKVArgument("has_read_agreement", "1");
            response.AppendKVArgument("sex", "M");
            response.AppendKVArgument("country", "AU");
            response.AppendKVArgument("has_special_rights", "0");
            response.AppendKVArgument("badge_type", "ADM");
            player.Send(response);
        }
    }
}