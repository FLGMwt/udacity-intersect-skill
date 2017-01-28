using System;
using Newtonsoft.Json;

namespace UdacityIntersectSkill
{
    public class Handler
    {
        public AlexaResponse Hello(AlexaRequest request)
        {
            string text;
            switch (request.Request.Intent.Name)
            {
                case IntentType.What:
                    text = "what do you meant what?";
                    break;
                case IntentType.When:
                    text = "what do you meant when?";
                    break;
                case IntentType.DaysUntil:
                    text = "what do you meant how many days?";
                    break;
                default:
                    throw new NotSupportedException();
            }
            var response = GetTextResponse(text);

            Console.WriteLine(JsonConvert.SerializeObject(response));

            return response;
        }

        public static AlexaResponse GetTextResponse(string text)
        {
            return new AlexaResponse
            {
                Response = new ResponseBody
                {
                    OutputSpeech = new OutputSpeech
                    {
                        Text = text
                    }
                }
            };
        }
    }

    public class AlexaRequest
    {
        public RequestBody Request { get; set; }
    }

    public class RequestBody
    {
        public string Type { get; set; }
        public Intent Intent { get; set; }
    }

    public class Intent
    {

        public IntentType Name { get; set; }
    }

    public enum IntentType
    {
        What = 1,
        When = 2,
        DaysUntil = 3,
    }

    public class AlexaResponse
    {
        public string Version => "1.0";
        public ResponseBody Response { get; set; }
    }

    public class ResponseBody
    {
        public OutputSpeech OutputSpeech { get; set; }
    }

    public class OutputSpeech
    {
        public string Type => "PlainText";
        public string Text { get; set; }
    }
}
