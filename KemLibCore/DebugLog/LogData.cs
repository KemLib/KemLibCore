using System.Text.Json;
using System.Text.Json.Serialization;

namespace KemLibCore.DebugLog
{
    public struct LogData
    {
        #region Properties
        private const string TIME_CULTURE_INFO = "dd/MM/yyyy HH:mm:ss.fff";

        [JsonInclude]
        public string Time;
        [JsonInclude]
        public LogType Type;
        [JsonInclude]
        public string Source;
        [JsonInclude]
        public string Title;
        [JsonInclude]
        public string Message;

        [JsonIgnore]
        public DateTime LogTime
        {
            get
            {
                if(DateTime.TryParse(Time, out DateTime result))
                    return result;
                return DateTime.MinValue;
            }
        }
        #endregion

        #region Construction
        public LogData()
        {
            Time = string.Empty;
            Type = LogType.Message;
            Source = string.Empty;
            Title = string.Empty;
            Message = string.Empty;
        }
        public LogData(LogType type, string? source = null, string? title = null, string? message = null)
        {
            Time = DateTime.Now.ToString(TIME_CULTURE_INFO);
            Type = type;
            Source = source ?? string.Empty;
            Title = title ?? string.Empty;
            Message = message ?? string.Empty;
        }
        #endregion

        #region Method
        public override string ToString()
        {
            return ToJson();
        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static LogData FormJson(string json)
        {
            return JsonSerializer.Deserialize<LogData>(json);
        }
        #endregion
    }
}
