using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;
using System.Threading;
using MQTTnet.Client.Options;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Receiving;
using Microsoft.Extensions.Options;
using PTM_middleware.BLL;
using Newtonsoft.Json;
using PTM_middleware.Models.API;

namespace RSCD.MQTT
{
    public class MqttClient : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        [Obsolete]
        private readonly IApplicationLifetime _appLifetime;

        protected IMqttClient Client { get; set; }
        private IMqttClientOptions ClientOptions { get; set; }

        [Obsolete]
        public MqttClient(IApplicationLifetime appLifetime, IServiceProvider serviceProvider)
        {
            _appLifetime = appLifetime;
            _serviceProvider = serviceProvider;
            RegisterMqtt();
        }

        private void RegisterMqtt()
        {
            var factory = new MqttFactory();
            Client = factory.CreateMqttClient();
            ClientOptions = new MqttClientOptionsBuilder()
                .WithClientId("TestClient")
                .WithTcpServer("34.244.190.178")
                .Build();

            var PublishOptions = new MqttApplicationMessageBuilder()
                .WithTopic("MyTopic")
                .WithPayload("Hello World")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            Client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(e => {
                Client.SubscribeAsync(new TopicFilterBuilder().WithTopic("#").Build());
            });

            Client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(async e => {
                var data = e.ApplicationMessage.ConvertPayloadToString();
                Console.WriteLine($"Topic is {e.ApplicationMessage.Topic} \n{data}");

                string topic = e.ApplicationMessage.Topic;

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    // pass it to the handler class
                    var businessLogic = scope.ServiceProvider.GetRequiredService<Traffic_BL>();

                        var topicArray = topic.Split('/');

                    switch(topicArray[1])
                    {
                        case "TrafficDensity":
                            TrafficDensityRequest request = JsonConvert.DeserializeObject<TrafficDensityRequest>(data);
                            var result = businessLogic.CreateTrafficDensityLog(request);
                            break;

                        case "PedestrianRequest":
                            PedestrianCrossingRequest crossingRequest = JsonConvert.DeserializeObject<PedestrianCrossingRequest>(data);
                            var result1 = businessLogic.CreatePedestrianCrossingLog(crossingRequest);
                            break;


                    }


                }
            });

            ConnectMqqt();
        }

        //func to connect with the broker
        public void ConnectMqqt()
        {
            Task.Run(async () => { await Client.ConnectAsync(ClientOptions); });
        }

        //hosted service cancellation token
        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }
        

        private void OnStopped()
        {   
            Client.DisconnectAsync();
        }

        private void OnStopping()
        {
            
        }

        private void OnStarted()
        {
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
