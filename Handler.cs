using System;
using Newtonsoft.Json;

namespace UdacityIntersectSkill
{
    public class Handler
    {
        public AlexaResponse Hello(AlexaRequest alexaRequest)
        {
            var responseModel = GetResponseModel(alexaRequest);
            var response = GetTextAlexaResponse(responseModel);
            Console.WriteLine(JsonConvert.SerializeObject(response));
            return response;
        }

        public static ResponseModel GetResponseModel(AlexaRequest alexaRequest)
        {
            switch (alexaRequest.Request.Intent.Name)
            {
                case IntentType.What:
                    return GetWhatResponse();
                case IntentType.When:
                    return GetWhenResponse();
                case IntentType.DaysUntil:
                    return GetDaysUntilResponse();
                case IntentType.SkillInfo:
                    return GetSkillInfoResponse();
                default:
                    throw new NotSupportedException();
            }
        }

        public static ResponseModel GetWhatResponse()
        {
            return new ResponseModel
            {
                Text = "what do you meant what?"
            };
        }

        public static ResponseModel GetWhenResponse()
        {
            return new ResponseModel
            {
                Text = "what do you meant how many days until?"
            };
        }

        public static ResponseModel GetDaysUntilResponse()
        {
            return new ResponseModel
            {
                Text = "what do you meant what?"
            };
        }

        public static ResponseModel GetSkillInfoResponse()
        {
            var message = "This skill was built by Ryan Stelly"
                + " primarily to learn about developing Alexa skills"
                + " but also to shamelessly pander to Dr. Ashwin Ram,"
                + " the Head of R&D for Amazon Alexa who will be speaking at Udacity Intersect.{0}"
                + " The skill is open source on GitHub ({1}).{0}"
                + " If you see me there, say hi!";

            return new ResponseModel
            {
                Text = string.Format(message, "", "see the Alexa app for details"),
                CardTitle = "About This Skill",
                CardText = string.Format(message, "\n", "https://github.com/FLGMwt/udacity-intersect-skill"),
            };
        }

        public static AlexaResponse GetTextAlexaResponse(ResponseModel responseModel)
        {
            var response = new AlexaResponse
            {
                Response = new ResponseBody
                {
                    OutputSpeech = new OutputSpeech
                    {
                        Text = responseModel.Text,
                    },
                },
            };
            if (responseModel.CardTitle != null && responseModel.CardText != null)
            {
                response.Response.Card = new Card
                {
                    Title = responseModel.CardTitle,
                    Content = responseModel.CardText,
                };
            }
            return response;
        }
    }

    public class ResponseModel
    {
        public string Text { get; set; }
        public string CardTitle { get; set; }
        public string CardText { get; set; }
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
        SkillInfo = 4,
    }

    public class AlexaResponse
    {
        public string Version => "1.0";
        public ResponseBody Response { get; set; }
    }

    public class ResponseBody
    {
        public OutputSpeech OutputSpeech { get; set; }
        public Card Card { get; set; }
    }

    public class OutputSpeech
    {
        public string Type => "PlainText";
        public string Text { get; set; }
    }

    public class Card
    {
        public string Type => "Simple";
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
