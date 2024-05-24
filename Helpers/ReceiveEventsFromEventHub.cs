using System.Dynamic;
using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs.Consumer;

namespace EventHubExplorer.Helpers
{
    public class ReceiveEventsFromEventHub()
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private string EventHubConnectionString { set; get; }
        private string EventHubName { set; get; }
        private string ConsumerGroupName { set; get; }
        public void SetConfig(string eventHubConnectionString, string eventHubName,string consumerGroupName)
        {
            EventHubConnectionString = eventHubConnectionString;
            EventHubName = eventHubName;
            ConsumerGroupName = consumerGroupName;
        }


        public async Task StartProcessingAsync(CancellationToken token)
        {
            CancellationToken cancellationToken = _cancellationTokenSource.Token;


            await using EventHubConsumerClient client = new EventHubConsumerClient(ConsumerGroupName.Length > 0 ? ConsumerGroupName : "$Default",EventHubConnectionString,EventHubName);
            EventPosition latestPosition = EventPosition.Latest;
           

            var partitions = await client.GetPartitionIdsAsync();

            var tasks = new List<Task>();
            foreach(var partitionId in partitions)
            {
                tasks.Add(ReceiveEventsFromPartitionAsync(client,partitionId,cancellationToken));
            }

         
            await Task.WhenAll(tasks);

        }

        public async Task StopProcessingAsync(CancellationToken token)
        {
            await _cancellationTokenSource.CancelAsync();
        }


        static async Task ReceiveEventsFromPartitionAsync(EventHubConsumerClient consumerClient,string partitionId,CancellationToken cancellationToken)
        {
            
            await foreach(PartitionEvent @event in consumerClient.ReadEventsFromPartitionAsync(partitionId,EventPosition.Latest,cancellationToken))
            {

                if(cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var data = Encoding.UTF8.GetString(@event.Data.EventBody.ToArray());
                var options = new JsonSerializerOptions
                {
                    Converters = { new ExpandoObjectConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var initialData = JsonSerializer.Deserialize<ExpandoObject>(data,options);
                EventBus.Instance.Publish(initialData);
            }
        }
    }
}
