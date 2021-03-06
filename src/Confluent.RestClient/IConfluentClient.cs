﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.RestClient.Model;
using System.Threading;

namespace Confluent.RestClient
{
    /// <summary>
    /// Restful client for Confluent REST API
    /// http://confluent.io/docs/current/kafka-rest/docs/api.html
    /// </summary>
    public interface IConfluentClient : IDisposable
    {
        /// <summary>
        /// Get a list of Kafka topics
        /// </summary>
        /// <returns></returns>
        Task<ConfluentResponse<List<string>>> GetTopicsAsync();

        /// <summary>
        /// Get a list of Kafka topics
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<List<string>>> GetTopicsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get metadata about a specific topic
        /// </summary>
        /// <param name="topicName">Name of the topic to get metadata about</param>
        /// <returns></returns>
        Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topicName);

        /// <summary>
        /// Get metadata about a specific topic
        /// </summary>
        /// <param name="topicName">Name of the topic to get metadata about</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topicName, CancellationToken cancellationToken);

        /// <summary>
        /// The partitions resource provides per-partition metadata, including the current leaders and replicas for each partition
        /// </summary>
        /// <param name="topicName">Name of the topic to get metadata about</param>
        /// <returns></returns>
        Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topicName);

        /// <summary>
        /// The partitions resource provides per-partition metadata, including the current leaders and replicas for each partition
        /// </summary>
        /// <param name="topicName">Name of the topic to get metadata about</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topicName, CancellationToken cancellationToken);

        /// <summary>
        /// Get metadata about a single partition in the topic
        /// </summary>
        /// <param name="topicName">Name of the topic</param>
        /// <param name="partitionId">ID of the partition to inspect</param>
        /// <returns></returns>
        Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(string topicName, int partitionId);

        /// <summary>
        /// Get metadata about a single partition in the topic
        /// </summary>
        /// <param name="topicName">Name of the topic</param>
        /// <param name="partitionId">ID of the partition to inspect</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(string topicName, int partitionId, CancellationToken cancellationToken);

        /// <summary>
        /// Create a new consumer instance in the consumer group
        /// Note that the response includes a URL including the host since the consumer is stateful and tied to a specific REST proxy instance. 
        /// Subsequent examples in this section use a `Host` header for this specific REST proxy instance
        /// </summary>
        /// <param name="consumerGroupName">The name of the consumer group to join</param>
        /// <param name="createConsumerRequest"></param>
        /// <returns></returns>
        Task<ConfluentResponse<ConsumerInstance>> CreateConsumerAsync(
            string consumerGroupName,
            CreateConsumerRequest createConsumerRequest);

        /// <summary>
        /// Create a new consumer instance in the consumer group
        /// Note that the response includes a URL including the host since the consumer is stateful and tied to a specific REST proxy instance. 
        /// Subsequent examples in this section use a `Host` header for this specific REST proxy instance
        /// </summary>
        /// <param name="consumerGroupName">The name of the consumer group to join</param>
        /// <param name="createConsumerRequest"></param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<ConsumerInstance>> CreateConsumerAsync(
            string consumerGroupName,
            CreateConsumerRequest createConsumerRequest,
            CancellationToken cancellationToken);

        /// <summary>
        /// Produce binary messages to a topic, optionally specifying keys or partitions for the messages
        /// </summary>
        /// <param name="topicName">Name of the topic to produce the messages to</param>
        /// <param name="recordSet">Binary record set</param>
        /// <returns></returns>
        Task<ConfluentResponse<PublishResponse>> PublishAsBinaryAsync(
            string topicName,
            BinaryRecordSet recordSet);

        /// <summary>
        /// Produce binary messages to a topic, optionally specifying keys or partitions for the messages
        /// </summary>
        /// <param name="topicName">Name of the topic to produce the messages to</param>
        /// <param name="recordSet">Binary record set</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<PublishResponse>> PublishAsBinaryAsync(
            string topicName,
            BinaryRecordSet recordSet,
            CancellationToken cancellationToken);

        /// <summary>
        /// Produce Avro messages to a topic, optionally specifying keys or partitions for the messages
        /// </summary>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <param name="topicName">Name of the topic to produce the messages to</param>
        /// <param name="recordSet"></param>
        /// <returns></returns>
        Task<ConfluentResponse<PublishResponse>> PublishAsAvroAsync<TKey, TValue>(
            string topicName,
            AvroRecordSet<TKey, TValue> recordSet)
            where TKey : class
            where TValue : class;

        /// <summary>
        /// Produce Avro messages to a topic, optionally specifying keys or partitions for the messages
        /// </summary>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <param name="topicName">Name of the topic to produce the messages to</param>
        /// <param name="recordSet"></param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse<PublishResponse>> PublishAsAvroAsync<TKey, TValue>(
            string topicName,
            AvroRecordSet<TKey, TValue> recordSet,
            CancellationToken cancellationToken)
            where TKey : class
            where TValue : class;

        /// <summary>
        /// Consume messages from a topic as binary (base 64 encoded string). 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <returns>List of Binary messages</returns>
        Task<ConfluentResponse<List<BinaryMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance,
            string topic);

        /// <summary>
        /// Consume messages from a topic as binary (base 64 encoded string). 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>List of Binary messages</returns>
        Task<ConfluentResponse<List<BinaryMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance,
            string topic,
            CancellationToken cancellationToken);

        /// <summary>
        /// Consume messages from a topic as binary (base 64 encoded string). 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <param name="maxBytes">The maximum number of bytes of un-encoded keys and values that should be included in the response</param>
        /// <returns>List of Binary messages</returns>
        Task<ConfluentResponse<List<BinaryMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance,
            string topic,
            int maxBytes);

        /// <summary>
        /// Consume messages from a topic as binary (base 64 encoded string). 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <param name="maxBytes">The maximum number of bytes of un-encoded keys and values that should be included in the response</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>List of Binary messages</returns>
        Task<ConfluentResponse<List<BinaryMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance,
            string topic,
            int maxBytes,
            CancellationToken cancellationToken);


        /// <summary>
        /// Consume messages from a topic as Avro data 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <returns>List of Avro messages</returns>
        Task<ConfluentResponse<List<AvroMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string topic)
            where TKey : class
            where TValue : class;

        /// <summary>
        /// Consume messages from a topic as Avro data 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>List of Avro messages</returns>
        Task<ConfluentResponse<List<AvroMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string topic,
            CancellationToken cancellationToken)
            where TKey : class
            where TValue : class;

        /// <summary>
        /// Consume messages from a topic as Avro data 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <param name="maxBytes">The maximum number of bytes of un-encoded keys and values that should be included in the response</param>
        /// <returns>List of Avro messages</returns>
        Task<ConfluentResponse<List<AvroMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string topic,
            int maxBytes)
            where TKey : class
            where TValue : class;

        /// <summary>
        /// Consume messages from a topic as Avro data 
        /// If the consumer is not yet subscribed to the topic, this adds it as a subscriber, possibly causing a consumer rebalance
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="topic">The topic to consume messages from</param>
        /// <param name="maxBytes">The maximum number of bytes of un-encoded keys and values that should be included in the response</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>List of Avro messages</returns>
        Task<ConfluentResponse<List<AvroMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string topic,
            int maxBytes,
            CancellationToken cancellationToken)
            where TKey : class
            where TValue : class;

        /// <summary>
        /// Commit offsets for the consumer
        /// Note that this request must be made to the specific REST proxy instance holding the consumer instance (using `Host` header)
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <returns>Returns a list of the partitions with the committed offsets</returns>
        Task<ConfluentResponse<List<ConsumerOffset>>> CommitOffsetAsync(ConsumerInstance consumerInstance);

        /// <summary>
        /// Commit offsets for the consumer
        /// Note that this request must be made to the specific REST proxy instance holding the consumer instance (using `Host` header)
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>Returns a list of the partitions with the committed offsets</returns>
        Task<ConfluentResponse<List<ConsumerOffset>>> CommitOffsetAsync(ConsumerInstance consumerInstance, CancellationToken cancellationToken);

        /// <summary>
        /// Destroy the consumer instance
        /// Note that this request must be made to the specific REST proxy instance holding the consumer instance (using `Host` header)
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <returns></returns>
        Task<ConfluentResponse> DeleteConsumerAsync(ConsumerInstance consumerInstance);

        /// <summary>
        /// Destroy the consumer instance
        /// Note that this request must be made to the specific REST proxy instance holding the consumer instance (using `Host` header)
        /// </summary>
        /// <param name="consumerInstance">Consumer instance</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<ConfluentResponse> DeleteConsumerAsync(ConsumerInstance consumerInstance, CancellationToken cancellationToken);
    }
}