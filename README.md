# sisense-api-client-csharp
The unofficial .NET/C# client library for the Sisense API.

<h1>How to Use</h1>

````
using SisenseApiClient;
using SisenseApiClient.Authenticators;

// Create an authenticator with your username and password
var authenticator = new LoginAuthenticator("<username>", "<password>");

// Create the Sisense client
var sisenseClient = new SisenseClient("<sisense_server>", authenticator);

// Use one of the services 
var sets = await sisenseCilent.ElatiCubes.GetSetsAsync();
````

<h1>How to use with Global Token Authentication</h1>

For Sisense version 5.8 and earlier, you can use the Global Token located in the Admin page of the Sisense Web Application.
If you use a Sisense version later than the 5.8, I suggest you to use the authentication from the previous section to avoid receive 401 Unauthorized responses in some endpoints.

<i>For more information check https://developer.sisense.com/display/API2/Authenticating+Requests+with+a+Global+Token</i>

````
using SisenseApiClient;
using SisenseApiClient.Authenticators;

// Create an authenticator with your username, password and api key
var authenticator = new GlobalTokenAuthenticator("<username>", "<password>", "<apikey>");

// Create the Sisense client
var sisenseClient = new SisenseClient("<sisense_server>", authenticator);

// Use one of the services 
var sets = await sisenseCilent.ElatiCubes.GetSetsAsync();
````

<h1>What services are provided?</h1>

###### Authentication ######
- LoginAsync(credentials) - Authenticate and receive a token

###### ElastiCubes ######
- v1.0
  - GetSetsAsync() - Get a list of ElastiCube Sets
  - GetSetAsync(setName) - Get an ElastiCube Set
  - GetBuildRevisionAsync(server, cubeName) - Get the ElastiCube build revision
  - GetCustomTablesAsync(server, cubeName) - Get an ElastiCube's custom tables
  - GetCustomTableRelationsAsync(server, cubeName, tableName) - Get an ElastiCube table's relation
  - GetCustomTableAsync(server, cubeName, tableName) - Get an ElastiCube's custom table
  - VerifyConnectivityWithRServerAsync(server, rserver) - Verify connectivity between your ElastiCube server and an R server
  - GetServerSettingsAsync(server) - Returns your ElastiCube server’s settings

- v0.9
  - GetElastiCubesMetadataAsync(query, sortBy) - Returns a list of ElastiCubes with metadata
  - GetElastiCubeMetadataAsync(cubeName) - Returns metadata for an ElastiCube by ElastiCube name
  - GetElastiCubeMetadataFieldsAsync(cubeName, query, offset, count) - Returns fields included in a specific ElastiCube
  - GetServersElastiCubesAsync(query, offset, count, direction, withPermissions) - Returns ElastiCubes with their server and ElastiCube details
  - GetServersMetadataAsync(query, offset, count, direction, withPermissions) - Returns the ElastiCube servers with metadata.
  - GetElastiCubesByServerAsync(server, query, offset, count, orderBy, direction) - Returns all the ElastiCubes by server.
  - GetServerStatusAsync(server, query, offset, count, orderBy, direction) - Returns the status of each ElastiCube in the selected server.

<h1>Bugs or questions?</h1>

Please open an issue for any bugs or questions