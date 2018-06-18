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
If you use a Sisense version later than the 5.8, I suggest you to use the authentication from the previous section.

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
- GetSetsAsync() - Get a list of ElastiCube Sets
- GetSetAsync(setName) - Get an ElastiCube Set
- GetBuildRevisionAsync(server, cubeName) - Get ElastiCube build revision
- GetCustomTablesAsync(server, cubeName) - Get an ElastiCube's custom tables
- GetCustomTableRelationsAsync(server, cubeName) - Get an ElastiCube table's relation
- GetCustomTableAsync(server, cubeName, table) - Get an ElastiCube's custom table
- VerifyConnectivityWithRServerAsync(server, rserver) - Verify connectivity between your ElastiCube server and an R server
- GetServerSettingsAsync(server) - Returns your ElastiCube server’s settings

<h1>Bugs or questions?</h1>

Please open an issue for any bugs or questions