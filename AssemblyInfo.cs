using Amazon.Lambda.Core;

[assembly:LambdaSerializer(typeof(Stelly.AwsLambda.CamelCaseSerializer.AwsLambdaCamelCaseSerializer))]
