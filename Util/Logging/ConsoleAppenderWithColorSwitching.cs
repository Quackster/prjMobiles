using log4net.Appender;
using log4net.Core;
using System;
using System.Text.RegularExpressions;

namespace Squirtle.Util.Logging
{
    public class ConsoleAppenderWithColorSwitching : ConsoleAppender
    {
        private string ReplaceLogLevel(string renderedLayout)
        {
            renderedLayout = renderedLayout.Replace(" INFO ", " |DarkGreen|INFO|Gray| ");
            renderedLayout = renderedLayout.Replace(" DEBUG ", " |Blue|DEBUG|Gray|  ");
            renderedLayout = renderedLayout.Replace(" WARN ", " |DarkYellow|DEBUG|Gray|  ");
            renderedLayout = renderedLayout.Replace(" ERROR ", " |DarkRed|ERROR|Gray|  ");
            return renderedLayout;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var regex = new Regex(@"(\|\w+\|)");
            var renderedLayout = base.RenderLoggingEvent(loggingEvent);
            renderedLayout = ReplaceLogLevel(renderedLayout);

            var chunks = regex.Split(renderedLayout);

            foreach (var chunk in chunks)
            {
                if (chunk.StartsWith("|") && chunk.EndsWith("|"))
                {
                    var consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), chunk.Substring(1, chunk.Length - 2));
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    Console.Write(chunk);
                }
            }
        }
    }
}
