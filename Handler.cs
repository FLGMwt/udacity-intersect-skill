using System;
using Newtonsoft.Json;

namespace UdacityIntersectSkill
{
    public class Handler
    {
        public AlexaResponse Hello(AlexaRequest alexaRequest)
        {
            if (alexaRequest.Request.Type == RequestType.SessionEndedRequest)
            {
                return null;
            }
            var responseModel = GetResponseModel(alexaRequest);
            var response = GetTextAlexaResponse(responseModel);
            Console.WriteLine(JsonConvert.SerializeObject(response));
            return response;
        }

        public static ResponseModel GetResponseModel(AlexaRequest alexaRequest)
        {
            if (alexaRequest.Request.Type == RequestType.LaunchRequest)
            {
                return GetHelpResponse();
            }
            switch (alexaRequest.Request.Intent.Name)
            {
                case IntentType.What:
                    return GetWhatResponse();
                case IntentType.When:
                    return GetWhenResponse();
                case IntentType.DaysUntil:
                    return GetDaysUntilResponse(alexaRequest.Request.Timestamp);
                case IntentType.Where:
                    return GetWhereResponse();
                case IntentType.SkillInfo:
                    return GetSkillInfoResponse();
                default:
                    throw new NotSupportedException();
            }
        }

        public static ResponseModel GetHelpResponse()
        {
            var message = "Find out more information about the yewdacity intersect conference."
                + " You can ask 'What is intersect', 'when is intersect',"
                + " 'how many days until intersect', and 'who built this'";
            return new ResponseModel
            {
                Text = message,
            };
        }

        public static ResponseModel GetWhatResponse()
        {
            var message = "{0} Intersect is a single-day single-track conference"
                + " where students and technologists will meet and hear from leaders of the industry,"
                + " including speakers from Google, Amazon, Stanford, IBM, and more.{1}"
                + "{2}{1}"
                + "You can ask when, where, and how many days until Intersect";

            return new ResponseModel
            {
                // Yewdacity for phoenetics
                Text = string.Format(message, "Yewdacity", "\n", "see the Alexa app for a link to the conference website"),
                CardTitle = "About Udacity Intersect",
                CardText = string.Format("Udacity", "\n", "https://www.udacity.com/intersect"),
            };
        }

        public static ResponseModel GetWhenResponse()
        {
            return new ResponseModel
            {
                Text = "Yewdacity Intersect will be held on March 8, 2017 from 8am to 8 pm",
                CardTitle = "When is Udacity Intersect?",
                CardText = "March 8, 2017 from 8am to 8 pm",
            };
        }

        public static ResponseModel GetWhereResponse()
        {
            return new ResponseModel
            {
                Text = "Yewdacity Intersect will be held at the Computer History Museum in Mountain View, California",
                CardTitle = "Where is Udacity Intersect?",
                CardText = "Computer History Museum in Mountain View, California\nhttp://www.computerhistory.org/",
            };
        }

        public static ResponseModel GetDaysUntilResponse(DateTimeOffset requestTimestamp)
        {
            var conferenceDate = new DateTimeOffset(new DateTime(2017, 3, 8, 8, 0, 0), TimeSpan.FromHours(-8));
            var daysUntil = conferenceDate.Subtract(requestTimestamp).Days;
            return new ResponseModel
            {
                Text = $"There are {daysUntil} days until the Yewdacity Intersect conference",
                CardTitle = "Days Until Intersect",
                CardText = daysUntil.ToString(),
            };
        }

        public static ResponseModel GetSkillInfoResponse()
        {
            var message = "This skill was built by Ryan Stelly"
                + " primarily to learn about developing Alexa skills"
                + " but also to shamelessly pander to Dr. Ashwin Ram,"
                + " the Head of R&D for Amazon Alexa who will be speaking at {0} Intersect.{1}"
                + " The skill is open source on GitHub{2}.{1}"
                + " It was built in C# with AWS Lambda and the Serverless Framework.{1}"
                + " If you see me there, say hi!";

            return new ResponseModel
            {
                Text = string.Format(message, "yewdacity", "", " (see the Alexa app for details)"),
                CardTitle = "About This Skill",
                CardText = string.Format(message, "Udacity", "\n", ": https://github.com/FLGMwt/udacity-intersect-skill"),
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
        public RequestType Type { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Intent Intent { get; set; }
    }

    public enum RequestType
    {
        LaunchRequest = 1,
        IntentRequest = 2,
        SessionEndedRequest = 3,
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
        Where = 4,
        SkillInfo = 5,
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
