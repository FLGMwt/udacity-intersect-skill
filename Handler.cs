using System;
using Newtonsoft.Json;

namespace UdacityIntersectSkill
{
    public class Handler
    {
        public AlexaResponse Hello(AlexaRequest request)
        {
            var response = GetTextResponse($"Your intent was {request.Request.Intent.Name}");

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
        public string Name { get; set; }
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
