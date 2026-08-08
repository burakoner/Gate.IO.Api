namespace Gate.IO.Api.Base;

internal sealed class GateMultipartFormData
{
    internal const string ParameterName = "__gate_multipart_form_data";

    private GateMultipartFormData(string body, string contentType)
    {
        Body = body;
        ContentType = contentType;
    }

    public string Body { get; }

    public string ContentType { get; }

    public static ParameterCollection CreateBodyParameters(ParameterCollection formParameters)
    {
        var boundary = "--------------------------" + Guid.NewGuid().ToString("N");
        var body = new StringBuilder();

        foreach (var parameter in formParameters.Where(x => x.Value != null))
        {
            body.Append("--").Append(boundary).Append("\r\n");
            body.Append("Content-Disposition: form-data; name=\"").Append(parameter.Key).Append("\"\r\n\r\n");
            body.Append(Convert.ToString(parameter.Value, CultureInfo.InvariantCulture)).Append("\r\n");
        }

        body.Append("--").Append(boundary).Append("--\r\n");

        return new ParameterCollection
        {
            { ParameterName, new GateMultipartFormData(body.ToString(), $"multipart/form-data; boundary={boundary}") },
        };
    }

    public static GateMultipartFormData Find(IEnumerable<KeyValuePair<string, object>> parameters)
        => parameters?.Select(x => x.Value).OfType<GateMultipartFormData>().SingleOrDefault();
}
