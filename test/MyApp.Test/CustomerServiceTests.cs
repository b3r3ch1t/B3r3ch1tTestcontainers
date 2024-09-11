using Confluent.Kafka;
using MyApp;
using StackExchange.Redis;
using Testcontainers.Kafka;
using Testcontainers.MySql;
using Testcontainers.Redis;

public class CustomerServiceTests : IAsyncLifetime
{
    private MySqlContainer _mysqlContainer;
    private CustomerService _customerService;

    public async Task InitializeAsync()
    {
        _mysqlContainer = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .WithPortBinding(3306)
            .WithUsername("root")
            .WithPassword("password")
            .Build();
        await _mysqlContainer.StartAsync();

        _customerService = new CustomerService(_mysqlContainer.GetConnectionString());
        _customerService.CreateTable();
    }

    public async Task DisposeAsync()
    {
        await _mysqlContainer.DisposeAsync();
    }

    [Fact]
    public void AddCustomer_ShouldAddCustomer()
    {
        _customerService.AddCustomer(1, "John Doe");
        var customers = _customerService.GetCustomers();
        Assert.Contains("John Doe", customers);
    }

    [Fact]
    public void UpdateCustomer_ShouldUpdateCustomer()
    {
        _customerService.AddCustomer(1, "John Doe");
        _customerService.UpdateCustomer(1, "Jane Doe");
        var customers = _customerService.GetCustomers();
        Assert.Contains("Jane Doe", customers);
        Assert.DoesNotContain("John Doe", customers);
    }

    [Fact]
    public void DeleteCustomer_ShouldDeleteCustomer()
    {
        _customerService.AddCustomer(1, "John Doe");
        _customerService.DeleteCustomer(1);
        var customers = _customerService.GetCustomers();
        Assert.DoesNotContain("John Doe", customers);
    }
}


public class KafkaServiceTests : IAsyncLifetime
{
    private KafkaContainer _kafkaContainer;
    private ProducerConfig _producerConfig;

    public async Task InitializeAsync()
    {
        _kafkaContainer = new KafkaBuilder()
            .WithImage("confluentinc/cp-kafka:latest")
            .WithPortBinding(9092) 
            .Build();
        await _kafkaContainer.StartAsync();

        _producerConfig = new ProducerConfig { BootstrapServers = _kafkaContainer.GetBootstrapAddress() };
    }

    public async Task DisposeAsync()
    {
        await _kafkaContainer.DisposeAsync();
    }

    [Fact]
    public void ShouldProduceMessageToKafka()
    {
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        producer.Produce("test-topic", new Message<Null, string> { Value = "Hello, Kafka!" });
        // Further Kafka consumption logic can be implemented.
    }


    public class RedisServiceTests : IAsyncLifetime
    {
        private RedisContainer _redisContainer;
        private ConnectionMultiplexer _redisConnection;

        public async Task InitializeAsync()
        {
            _redisContainer = new RedisBuilder()
                .WithImage("redis:latest")
                .WithPortBinding(6379) 
                .Build();
            await _redisContainer.StartAsync();

            _redisConnection = await ConnectionMultiplexer.ConnectAsync(_redisContainer.GetConnectionString());
        }

        public async Task DisposeAsync()
        {
            await _redisConnection.DisposeAsync();
            await _redisContainer.DisposeAsync();
        }

        [Fact]
        public void ShouldSetAndGetRedisValue()
        {
            var db = _redisConnection.GetDatabase();
            db.StringSet("key", "value");
            var value = db.StringGet("key");

            Assert.Equal("value", value);
        }
    }
}