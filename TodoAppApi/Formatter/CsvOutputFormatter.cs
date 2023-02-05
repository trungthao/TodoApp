using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using TodoApp.Shared.DataTransferObjects;

namespace TodoAppApi.Formatter
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(ListTaskDto).IsAssignableFrom(type) ||
            typeof(IEnumerable<ListTaskDto>).IsAssignableFrom(type)
            )
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<ListTaskDto>)
            {
                foreach (var listTaskDto in (IEnumerable<ListTaskDto>)context.Object)
                {
                    FormatCsv(buffer, listTaskDto);
                }
            }
            else if (context.Object is ListTaskDto)
            {
                FormatCsv(buffer, (ListTaskDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, ListTaskDto listTaskDto)
        {
            buffer.AppendLine($"{listTaskDto.Id},\"{listTaskDto.Name}\"");
        }
    }
}