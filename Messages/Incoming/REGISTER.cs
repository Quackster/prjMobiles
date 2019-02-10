using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using prjMobiles.Storage.Access;
using System;
using System.Threading.Tasks;

namespace prjMobiles.Messages
{
    public class REGISTER : IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (player.Details != null)
                return;

            Response response = null;

            string username = request.GetArgument(0);
            string password = request.GetArgument(1);
            string email = request.GetArgument(2);

            if (password.Length < 3)
            {
                response = new Response("ERROR");
                response.AppendArgument("Your password is too short!");
                player.Send(response);
                return;
            }

            if (username.ToLower() == "Maarit")
            {
                player.Channel.CloseAsync();
                return;
            }

            int head = 1;
            int shirt = 1;
            int pants = 1;

            try
            {
                pants = int.Parse(request.GetArgument(3).Split(',')[0]);
                shirt = int.Parse(request.GetArgument(3).Split(',')[1]);
                head = int.Parse(request.GetArgument(3).Split(',')[2]);
            }
            catch { }

            if (pants <= 0 || pants == 9) pants = 1;
            if (shirt <= 0 || shirt == 9) shirt = 1;
            if (head <= 0 || head == 9) head = 1;

            string sex = request.GetArgument(6) == "Male" ? "M" : "F";
            int age = int.Parse(request.GetArgument(8));

            if (age >= 105)
            {
                response = new Response("ERROR");
                response.AppendArgument("You are too old!");
                player.Send(response);
                return;
            }

            if (age <= 11)
            {
                response = new Response("ERROR");
                response.AppendArgument("You are too young!");
                player.Send(response);
                return;
            }

            string mission = string.Empty;

            for (int i = 9; i < request.CountArguments(); i++)
                mission += request.GetArgument(i) + " ";

            mission = mission.TrimEnd();

            var existingDetails = PlayerDao.CheckExistingUser(username, email);
            //public static void NewUser(string username, string password, string email, int age, int pants, int shirt, int head, string sex, string mission)

            if (existingDetails != null)
            {
                response = new Response("ERROR");
                response.AppendArgument("That username or email is already registered!");
                player.Send(response);
                return;
            }

            PlayerDao.NewUser(username, password, email, age, pants, shirt, head, sex, mission);

            /*var playerData = PlayerDao.TryLogin(username, password);

            if (playerData == null)
            {
                var response = new Response("ERROR");
                response.AppendArgument("login in");
                player.Send(response);
                return;
            }

            player.login(playerData, true);*/

        }
    }
}