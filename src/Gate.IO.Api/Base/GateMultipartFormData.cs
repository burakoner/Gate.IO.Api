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

    public byte[] Bytes { get; private set; }

    public static ParameterCollection CreateBodyParameters(ParameterCollection formParameters, string fileField, GateOtcFileUpload file)
        => CreateBodyParameters(formParameters, new[] { new KeyValuePair<string, GateOtcFileUpload>(fileField, file) });

    public static ParameterCollection CreateBodyParameters(ParameterCollection formParameters, IReadOnlyCollection<KeyValuePair<string, GateOtcFileUpload>> files)
    {
        if (files.Count == 0) return CreateBodyParameters(formParameters);
        var boundary = "--------------------------" + Guid.NewGuid().ToString("N");
        using var stream = new System.IO.MemoryStream();
        void WriteText(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }
        foreach (var parameter in formParameters.Where(x => x.Value != null))
        {
            WriteText($"--{boundary}\r\nContent-Disposition: form-data; name=\"{parameter.Key}\"\r\n\r\n");
            WriteText(Convert.ToString(parameter.Value, CultureInfo.InvariantCulture) + "\r\n");
        }
        foreach (var item in files)
        {
            var file = item.Value;
            if (file.Content == null || file.Content.Length == 0)
                throw new ArgumentException("A nonempty file is required", nameof(file.Content));
            if (string.IsNullOrWhiteSpace(file.FileName) || file.FileName.IndexOfAny(new[] { '\r', '\n', '\0', '"', '/', '\\' }) >= 0)
                throw new ArgumentException("A safe file name without a path is required", nameof(file.FileName));
            if (file.ContentType?.IndexOfAny(new[] { '\r', '\n', '\0' }) >= 0
                || !System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(file.ContentType ?? "application/octet-stream", out var mediaType))
                throw new ArgumentException("A valid plaintext MIME type is required", nameof(file.ContentType));
            var disposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            {
                Name = $"\"{item.Key}\"", FileName = file.FileName, FileNameStar = file.FileName,
            };
            WriteText($"--{boundary}\r\nContent-Disposition: {disposition}\r\nContent-Type: {mediaType}\r\n\r\n");
            stream.Write(file.Content, 0, file.Content.Length);
            WriteText("\r\n");
        }
        WriteText($"--{boundary}--\r\n");
        return new ParameterCollection
        {
            { ParameterName, new GateMultipartFormData("[multipart/form-data content omitted]", $"multipart/form-data; boundary={boundary}") { Bytes = stream.ToArray() } },
        };
    }

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
