using CCPPC.VirtualShop.Domain.Enums;
using Serilog;

namespace CCPPC.VirtualShop.Domain.Models
{
    public class LogModel
    {
        private string MethodName { get; set; }
        private string Message { get; set; }
        private LogType Type { get; set; }

        public LogModel() { }

        public void Record(string methodName, string message, LogType logType)
        {
            MethodName = methodName;
            Message = message;
            Type = logType;
            Record();
        }

        private void Record()
        {
            string template = "{methodname} - {message}";
            string logMessage = $"{MethodName} - {Message}";
            switch (Type)
            {
                case LogType.Error:
                    Log.Error(template, logMessage);
                    break;
                case LogType.Information:
                    Log.Information(template, logMessage);
                    break;
                case LogType.Warning:
                    Log.Warning(template, logMessage);
                    break;
            }
        }
    }
}
