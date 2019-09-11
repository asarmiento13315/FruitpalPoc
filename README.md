# FruitpalPoc
PoC of a tool called fruitpal, which should allows a trader to understand the full cost of buying fruit from various countries of origin.
<br><br>
## Restore instructions
- ...

### **Note:**

In: ./Fruitpal/appsettings.json 

Set propety: marketOverheadJsonFile 

To: path/to/3rd-party/market-overhead/json-file
<br><br>
## Run tests
`dotnet test ./FruitpalTests/FruitpalTests.csproj`

## Run tool
`dotnet run -p ./Fruitpal/Fruitpal.csproj -- <commodity> <price per ton> <trade volume>`

**Ex.** `dotnet run -p ./Fruitpal/Fruitpal.csproj -- mango 53 405`

## Build platform-dependant executable
**Ex. for mac os.** `dotnet publish -c Release -r osx.10.14-x64 --self-contained false`