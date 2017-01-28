using System;
using Newtonsoft.Json;

namespace UdacityIntersectSkill
{
    public class Handler
    {
        public AlexaResponse Hello()
        {
            var response = GetTextResponse("Go Serverless v1.0! Your function executed successfully!");

            Console.WriteLine(JsonConvert.SerializeObject(response));

            return response;
        }

        public static AlexaResponse GetTextResponse(string text)
        {
            return new AlexaResponse
            {
                Response = new Response
                {
                    Output = new Output
                    {
                        Text = text
                    }
                }
            };
        }
    }

    public class AlexaResponse
    {
        public string Version => "1.0";
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonProperty("outputSpeech")]
        public Output Output { get; set; }
    }

    public class Output
    {
        public string Type => "PlainText";
        public string Text { get; set; }
    }
}
