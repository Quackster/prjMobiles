using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using Squirtle.Storage.Access;
using System;

namespace Squirtle.Messages
{
    public class UPDATE : IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (player.Details == null)
                return;

            //[2019-02-03 13:21:32,984] DEBUG  Squirtle.Messages.MessageHandler - Received: UPDATE 123 you@domain.com 2,1,14 noidea x Male no 45 Alex the best
            Response response = null;

            string password = request.GetArgument(0);
            string email = request.GetArgument(1);

            if (password.Length < 3)
            {
                response = new Response("ERROR");
                response.AppendArgument("Your password is too short!");
                player.Send(response);
            }

            int head = 1;
            int shirt = 1;
            int pants = 1;

            try
            {
                pants = int.Parse(request.GetArgument(2).Split(',')[0]);
                shirt = int.Parse(request.GetArgument(2).Split(',')[1]);
                head = int.Parse(request.GetArgument(2).Split(',')[2]);
            }
            catch { }

            if (pants <= 0 || pants == 9) pants = 1;
            if (shirt <= 0 || shirt == 9) shirt = 1;
            if (head <= 0 || head == 9) head = 1;

            string sex = request.GetArgument(5) == "Male" ? "M" : "F";
            int age = int.Parse(request.GetArgument(7));

            if (age >= 105)
            {
                response = new Response("ERROR");
                response.AppendArgument("You are too old!");
                player.Send(response);
            }

            if (age <= 11)
            {
                response = new Response("ERROR");
                response.AppendArgument("You are too young!");
                player.Send(response);
            }

            string mission = string.Empty;

            for (int i = 8; i < request.CountArguments(); i++)
                mission += request.GetArgument(i) + " ";

            mission = mission.TrimEnd();

            PlayerDao.EditUser(player.Details.Username, password, email, age, pants, shirt, head, sex, mission);
        }
    }
}