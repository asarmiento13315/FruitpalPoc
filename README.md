# Fruitpal PoC
... Into the algorithmic trading of tropical fruits!

This is a PoC of a tool called fruitpal, which should allows a trader to understand the full cost of buying fruit from various countries of origin.
<br><br>
## Restore instructions
`> dotnet restore`

***For pointing to the 3rd-party market overhead data:***

***In:*** ./Fruitpal/appsettings.json 

***Set propety:*** marketOverheadJsonFile 

***To:*** path/to/3rd-party/market-overhead/json-file
<br><br>
## Run tests
`> dotnet test ./FruitpalTests/FruitpalTests.csproj`
<br><br>
## Run tool
`> dotnet run -p ./Fruitpal/Fruitpal.csproj -- <commodity> <price per ton> <trade volume>`

>**Example:** A trader who wants to know the full cost of buying 405 tons of mangos at $53 a ton would run:<br>

`> dotnet run -p ./Fruitpal/Fruitpal.csproj -- mango 53 405` <br>

>In response to the Example Input, the trader might see:<br>
**<** BR 22060.10 | (54.42\*405)+20<br>
**<** MX 21999.20 | (54.24\*405)+32


## Build platform-dependant executable
**Ex. for mac os.**<br>
`> dotnet publish -c Release -r osx.10.14-x64 --self-contained false`

**Publish output folder:**<br>
`./Fruitpal/bin/Release/netcoreapp2.2/osx.10.14-x64/publish`