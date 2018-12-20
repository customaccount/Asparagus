using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebJobs.Extensions.RabbitMQ.Binding
{
    internal class RabbitQueueValueBinder : IValueBinder
    {
        private readonly RabbitQueueTriggerValue _value;
        private readonly ParameterInfo _parameter;

        public Type Type => _parameter.ParameterType;

        public RabbitQueueValueBinder(ParameterInfo parameter, RabbitQueueTriggerValue value)
        {
            _value = value;
            _parameter = parameter;
        }

        public Task<object> GetValueAsync()
        {
                // if its a string, just return the body of the message as string
                if (Type == typeof(string))
                {
                    return Task.FromResult(Encoding.UTF8.GetString(_value.MessageBytes) as object);
                }

                if (Type == typeof(JObject))
                {
                    return Task.FromResult(JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(_value.MessageBytes)) as object) ;
                }

                if (typeof(MessageContext).IsAssignableFrom(Type))
                {
                    var t = (MessageContext)Activator.CreateInstance(Type);

                    t.MessageId = _value.MessageId;
                    t.ApplicationId = _value.ApplicationId;
                    t.ContentType = _value.ContentType;
                    t.CorrelationId = _value.CorrelationId;
                    t.Headers = _value.Headers;

                    t.Message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(_value.MessageBytes),
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                        });

                    return Task.FromResult(t as object) ;
                }

                // deserialize object based on the settings
                var obj = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(_value.MessageBytes),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                    });

                return Task.FromResult(obj);
        }
        
        public string ToInvokeString()
        {
            return Encoding.UTF8.GetString(_value.MessageBytes);
        }

        //TODO 
        public Task SetValueAsync(object value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}