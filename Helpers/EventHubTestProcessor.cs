using System.Dynamic;
using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Primitives;
using Azure.Messaging.EventHubs.Processor;

namespace EventHubExplorer.Helpers
{
    public class EventHubTestProcessor(BlobCheckpointStore checkpointStore, int maximumBatchSize, int prefetchCount, string consumerGroupName, string eventHubConnectionString, string eventHubName)
        : PluggableCheckpointStoreEventProcessor<EventProcessorPartition>(checkpointStore, maximumBatchSize, consumerGroupName, eventHubConnectionString, eventHubName, new EventProcessorOptions
        {
            PrefetchCount = prefetchCount,
        })
    {
        // generate public event delegate


        // _dataGrid = dataGridView;
        // _eventModel = [eventModel];
        // _dataGrid.DataSource = _eventModel;
        //
        // foreach (var column in eventModel.ToList())
        // {
        //     _dataGrid.Columns.Add(column.Key, column.Key);
        // }


        protected override async Task OnProcessingEventBatchAsync(IEnumerable<EventData> events, EventProcessorPartition partition, CancellationToken cancellationToken)
        {
            var eventDatas = events.ToList();

            if (eventDatas.Count == 0) return;
            EventData lastEvent = null;
            var sortedEvents = new HashSet<EventData>(eventDatas.OrderBy(e => e.SequenceNumber));

            var eventList = sortedEvents.OrderBy(e => e.SequenceNumber);

            foreach (var eventData in eventList)
            {
                lastEvent = eventData;
                var data = Encoding.UTF8.GetString(eventData.EventBody.ToArray());
                var options = new JsonSerializerOptions
                {
                    Converters = { new ExpandoObjectConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var initialData = JsonSerializer.Deserialize<ExpandoObject>(data, options);
                EventBus.Instance.Publish(initialData);


            }

            await UpdateCheckpointAsync(partition.PartitionId, CheckpointPosition.FromEvent(lastEvent), cancellationToken).ConfigureAwait(false);
        }

        protected override  Task OnProcessingErrorAsync(Exception exception, EventProcessorPartition partition, string operationDescription, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Halt(CancellationToken token)
        {
            // _dataGrid.Rows.Clear();
            // _dataGrid.Columns.Clear();
            // _dataGrid.Update();
            // _dataGrid.Refresh();
            await StopProcessingAsync(token);
        }
    }
}